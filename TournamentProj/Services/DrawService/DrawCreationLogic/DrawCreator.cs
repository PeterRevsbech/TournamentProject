using System;
using System.Collections.Generic;
using System.Linq;
using TournamentProj.Exceptions;
using TournamentProj.Model;

namespace TournamentProj.Services.DrawService
{
    public static class DrawCreator
    {
        public static Draw GenerateDraw(DrawCreation drawCreation)
        {
            //TODO add logic here for recognizing specific score types, e.g. tennis, squash so on 
            Draw draw = new Draw();

            switch (drawCreation.DrawType)
            {
                case DrawType.KO:
                    ConfigureKO(draw,drawCreation);
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
                    Match match = new Match()
                    {
                        P1Id = playerIds[i],
                        P2Id = playerIds[j],
                        Status = Status.OPEN
                    };
                    matches.Add(match);
                }
            }

            draw.Matches = matches;
        }

        private static void ConfigureKO(Draw draw, DrawCreation drawCreation)
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
            //Configure first round manually - no match dependencies
            var opponents = seededPlayerIds.Skip(seededPlayerIds.Count / 2).ToList();
            opponents = Randomize(opponents);

            for (int i = 0; i < roundSize; i++)
            {
                var match = new Match()
                {
                    Draw = draw,
                    DrawId = draw.Id,
                    P1Id = seededPlayerIds[i],
                    P2Id = opponents[0]
                };
                //Remove that opponent
                opponents.Remove(opponents[0]);
                matches.Add(match);
            }
            
            
            

            //Configure next rounds with match dependencies
            for (var round = 2; round <= lastRound; round++)
            {
                
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
        
    }
}