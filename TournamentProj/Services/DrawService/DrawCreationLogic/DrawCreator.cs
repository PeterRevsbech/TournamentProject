using System;
using System.Collections.Generic;
using System.Linq;
using TournamentProj.DAL;
using TournamentProj.DAL.MatchDependencyRepository;
using TournamentProj.Exceptions;
using TournamentProj.Model;

namespace TournamentProj.Services.DrawService
{
    public static class DrawCreator
    {
        public static Draw GenerateDraw(DrawCreation drawCreation,
            IDrawRepository drawRepository,
            IMatchRepository matchRepository,
            IMatchDependencyRepository matchDependencyRepository)
        {
            //TODO add logic here for recognizing specific score types, e.g. tennis, squash so on 
            var draw = new Draw();
            drawRepository.Insert(draw);

            switch (drawCreation.DrawType)
            {
                case DrawType.KO:
                    ConfigureKO(draw,drawCreation,matchRepository,matchDependencyRepository);
                    break;
                case DrawType.RR:
                    ConfigureRR(draw, drawCreation);
                    break;
                case DrawType.MONRAD:
                    break;
                default:
                    throw new TournamentSoftwareException("Tried to create a draw with no DrawType specified.");
            }
            
            draw.Name = drawCreation.Name;
            draw.Sets = drawCreation.Sets;
            draw.Games = drawCreation.Games;
            draw.Points = drawCreation.Points;
            draw.TournamentId = drawCreation.TournamentId;
            draw.DrawType = drawCreation.DrawType;
            draw.TieBreaks = drawCreation.TieBreaks;
            
            drawRepository.Update(draw);
            return draw;
        }

        private static void ConfigureRR(Draw draw, DrawCreation drawCreation)
        {
            //Everybody plays against everybody
            
            //All matches are open from the beginning
            //Highest seeded players should play each other in the last round, but this is up
            //to the user, since the matches are open anyway

            int[] playerIds = drawCreation.playerIds.ToArray();
            
            var matches = new List<Match>();
            
            for (int i = 0; i< playerIds.Length; i++)
            {
                for (int j = i+1; j < playerIds.Length; j++)
                {
                    var match = initMatch(draw, null, null, playerIds[i], playerIds[j], Status.OPEN, 1, drawCreation);
                    matches.Add(match);
                }
            }

            draw.Matches = matches;
        }

        private static void ConfigureKO(Draw draw, DrawCreation drawCreation, IMatchRepository matchRepository, IMatchDependencyRepository matchDependencyRepository)
        {
            var matches = new List<Match>();
            
            //Configure seeding
            var seededPlayerIds = GetSeededPlayerIds(drawCreation.playerIds.ToList(), drawCreation.playerIdsSeeded);

            while (!IsPowerOf2(seededPlayerIds.Count))
            {
                //Add byes so playerIds is power of 2
                seededPlayerIds.Add(-1);
            }

            int lastRound = Math.ILogB(seededPlayerIds.Count);
            int roundSize = seededPlayerIds.Count / 2;
            
            //Configure first round manually - no match dependencies - matches are open
            //Byes should be paired with top-seeded players in first round
            var opponents = seededPlayerIds.Skip(seededPlayerIds.Count / 2).ToList();
            opponents = BringByesToFront(Randomize(opponents));
            

            for (int i = 0; i < roundSize; i++)
            {
                //Init first round match
                var match = initMatch(draw, null, null,seededPlayerIds[i],opponents[0], Status.OPEN, 1, drawCreation);
                
                //Remove that opponent
                opponents.Remove(opponents[0]);
                matchRepository.Insert(match); //Insert in database to get an id
                matches.Add(match);
            }
            
            
            //Configure next rounds with match dependencies
            for (var round = 2; round <= lastRound; round++)
            {
                //Update roundSize: In round i, there are half as many mathces as in i-1
                var oldRoundSize = roundSize;
                roundSize = oldRoundSize / 2;
      
                
                //Matches from last round - this is where the dependencies will come from
                var lastRoundMatchIds = matches.Skip(matches.Count - oldRoundSize).Select(m => m.Id).ToList();
                
                //Last half of the lastRoundMatches ==> the ones that are "unseeded" in this round
                var opponentMatchIds = lastRoundMatchIds.Skip(oldRoundSize / 2).ToList();
                opponentMatchIds = Randomize(opponentMatchIds);

                //Add roundsize new matches for this round
                for (int i = 0; i < roundSize; i++)
                {
                    //Create the matchDependencies
                    var p1Dependency = new MatchDependency()
                    {
                        DependencyId = lastRoundMatchIds[i], //Player 1 will come from match with higher seeded player
                        DependsOnDraw = false,
                        Position = 1
                    };
                    matchDependencyRepository.Insert(p1Dependency);
                    var p2Dependency = new MatchDependency()
                    {
                        DependencyId = opponentMatchIds[0], //Player 2 will be selected at random from opponents
                        DependsOnDraw = false,
                        Position = 1
                    };
                    matchDependencyRepository.Insert(p2Dependency);


                    var match = initMatch(draw, p1Dependency, p2Dependency, 0, 0,  Status.CLOSED, round, drawCreation);
                    
                    //Remove the selected opponent-match-id
                    opponentMatchIds.Remove(opponentMatchIds[0]);
                    matchRepository.Insert(match); //Insert in database to get an id
                    matches.Add(match);
                }
            }
            
            draw.Matches = matches;
        }

        private static void ConfigureMonrad(Draw draw, DrawCreation drawCreation)
        {
            var playerIds = drawCreation.playerIds.ToArray();
            
            var matches = new List<Match>();
            
            

            draw.Matches = matches;
        }

        private static List<int> GetSeededPlayerIds(List<int> playerIds, List<int> seededPlayerIds)
        {
            //If no seedings were made - just randomize and return input
            if (seededPlayerIds == null || seededPlayerIds.Count==0)
            {
                return Randomize(playerIds);
            }
            
            //Otherwise change order in playerIds to correspond to seedings
            //1) Remove seeded players from playerIds 
            playerIds = playerIds.Except(seededPlayerIds).ToList();
            
            //2) Scramble unseeded players for fairness
            playerIds = Randomize(playerIds);
            
            //3) append unseeded players to back of list behind seeded players
            seededPlayerIds.AddRange(playerIds);
            
            //result is a pseudo-seeded list of players
            return seededPlayerIds;
        }
        
        private static List<int> Randomize(List<int> input)
        {
            Random rnd = new Random();
            return input.OrderBy((item) => rnd.Next()).ToList();
        }

        private static List<int> BringByesToFront(List<int> input)
        {
            var newList = new List<int>();
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == -1)
                {
                    newList.Add(-1);
                }
            }
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] != -1)
                {
                    newList.Add(input[i]);
                }
            }

            return newList;
        }

        private static bool IsPowerOf2(int n)
        {
            while (n!=1)
            {
                if (n % 2 != 0)
                {
                    return false;
                }

                n /= 2;
            }

            return true;
        }
        
        private static Match initMatch(Draw draw, MatchDependency p1Dependency, MatchDependency p2Dependency,int p1Id, int p2Id, Status status, int round, DrawCreation drawCreation){
            var match = new Match()
            {
                Draw = draw,
                DrawId = draw.Id,
                P1DependencyId = p1Dependency == null ? 0: p1Dependency.Id,
                P2DependencyId = p2Dependency == null ? 0: p2Dependency.Id,
                P1Id = p1Id,
                P2Id = p2Id,
                Status = status,
                round = round,
                P1Match = 0,
                P2Match = 0,
                P1Games = 0,
                P2Games = 0,
                P1Sets = 0,
                P2Sets = 0,
                P1PointsArray = drawCreation.InitPoints(),
                P2PointsArray = drawCreation.InitPoints()
            };
            return match;
        }
        
    }
    
    
}