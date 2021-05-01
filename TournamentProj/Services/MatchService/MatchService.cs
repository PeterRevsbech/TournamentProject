using System;
using System.Collections.Generic;
using System.Linq;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.DAL.MatchDependencyRepository;
using TournamentProj.DTO.Match;
using TournamentProj.Exceptions;
using TournamentProj.Model;

namespace TournamentProj.Services.MatchService
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchDependencyRepository _matchDependencyRepository;
        private readonly IDrawRepository _drawRepository;
        private readonly ITournamentContext _dbContext;

        public MatchService(ITournamentContext dbContext,
            IMatchRepository matchRepository,
            IMatchDependencyRepository matchDependencyRepository,
            IDrawRepository drawRepository)
        {
            _dbContext = dbContext;
            _matchRepository = matchRepository;
            _matchDependencyRepository = matchDependencyRepository;
            _drawRepository = drawRepository;
        }

        public Match Create(Match match)
        {
            _matchRepository.Insert(match);
            _dbContext.SaveChanges();
            return match;
        }

        public Match Get(int id)
        {
            return _matchRepository.FindById(id);
        }

        public IEnumerable<Match> GetByPlayerId (int playerId)
        {
            return _matchRepository.FindByPlayerId(playerId);
        }

        public IEnumerable<Match> GetByDrawId(int drawId)
        {
            return _matchRepository.FindByDrawId(drawId);
        }

        public IEnumerable<Match> GetAll()
        {
            return _matchRepository.FindAll();
        }

        public Match Delete(int id)
        {
            var match = _matchRepository.FindById(id);
            _matchRepository.Delete(match);
            _dbContext.SaveChanges();
            return match;
        }

        public Match Update(Match match, Match oldMatch)
        {
            if (oldMatch == null)
            {
                oldMatch = _matchRepository.FindById(match.Id);
            }

            if (oldMatch.Status != Status.FINISHED && match.Status == Status.FINISHED)
            { //If match was not finished before, but is now
                MatchFinished(match);
            }

            if ((match.P1Id == -1 || match.P2Id == -1)
                && (oldMatch.P1Id == 0 || oldMatch.P2Id == 0)
                && !(match.P1Id == 0 || match.P2Id == 0))
            { //If match has a bye and opponent was not previously known
                ExecuteByeMatch(match);
            }
            
            match.UpdateStatus();
            
            _matchRepository.Update(match);
            _dbContext.SaveChanges();
            return match;
        }

        private void MatchFinished(Match match)
        {
            //Match has been updated and is now Finished.
            //If any other matches are dependent on the result of this match - they should be updated
            var dependentMatches = _matchRepository
                .FindByDrawId(match.DrawId)
                .ToArray()
                .Where(m => (
                    (m.P1DependencyId != 0 && _matchDependencyRepository.FindById(m.P1DependencyId).DependencyId == match.Id)
                    || (m.P2DependencyId != 0 && _matchDependencyRepository.FindById(m.P2DependencyId).DependencyId == match.Id))
                    )
                .ToArray();
            
            //For each dependent match - update the players and the status
            foreach (var dependentMatch in dependentMatches)
            {
                //Find the id's of the matches, that the matchDependencies point to
                var p1DepId = dependentMatch.P1DependencyId != 0 ?_matchDependencyRepository.FindById(dependentMatch.P1DependencyId).DependencyId : 0;
                var p2DepId = dependentMatch.P2DependencyId != 0 ?_matchDependencyRepository.FindById(dependentMatch.P2DependencyId).DependencyId : 0;
                
                MatchDependency matchDependency;
                if (p1DepId == match.Id)
                {
                    matchDependency = _matchDependencyRepository.FindById(dependentMatch.P1DependencyId);
                    dependentMatch.P1Id = FindResultingPlayerFromMatchDependency(matchDependency);
                }
                else if (p2DepId == match.Id)
                {
                    matchDependency = _matchDependencyRepository.FindById(dependentMatch.P2DependencyId);
                    dependentMatch.P2Id = FindResultingPlayerFromMatchDependency(matchDependency);
                }
                
                //Update status
                dependentMatch.UpdateStatus();
                
                //Save to repo
                _matchRepository.Update(dependentMatch);

            }
        }

        private int FindResultingPlayerFromMatchDependency(MatchDependency matchDependency)
        {
            var match = _matchRepository.FindById(matchDependency.DependencyId);
            var wantedPosition = matchDependency.Position;
            if ((match.P1Won && wantedPosition == 1) || (!match.P1Won && wantedPosition == 2) )
            {
                return match.P1Id;
            }
            else
            {
                return match.P2Id;
            }
        }
        
        private void ExecuteByeMatch(Match match)
        {
            //Find the corresponding draw to find the scoring system
            var draw = _drawRepository.FindById(match.DrawId);
            var maxGames = draw.Sets == 0 ? draw.Games : draw.Sets * draw.Games;
            var minGames = (1 + maxGames) / 2;

            //Opponent automatically wins the match and match is finished
            if (match.P1Id == -1)
            {
                //If P1 was the bye
                match.P1Won = false;
                match.P2Games = minGames;
                match.P2Sets = draw.Sets;


                //Fill points array with max points for minimal number of games
                var arr1 = new int[minGames];
                var arr2 = new int[minGames];
                
                for (int i = 0; i < minGames; i++)
                {
                    arr1[i] = draw.Points;
                    arr2[i] = 0;
                }

                match.P1PointsArray = arr1;
                match.P2PointsArray = arr2;
            }
            else
            {
                //If P2 was the bye
                match.P1Won = true;
                match.P1Games = minGames;
                match.P1Sets = draw.Sets;

                //Fill points array with max points for minimal number of games
                var arr1 = new int[minGames];
                var arr2 = new int[minGames];
                
                for (var i = 0; i < minGames; i++)
                {
                    arr1[i] = draw.Points;
                    arr2[i] = 0;
                }

                match.P1PointsArray = arr1;
                match.P2PointsArray = arr2;
            }

            //Update the status of the match
            match.UpdateStatus();

            MatchFinished(match);
            
            _matchRepository.Update(match);
        }

        public Match UpdateScoreReport(MatchReportDTO matchReportDto)
        {
            //Find the corresponding match
            var match  = _matchRepository.FindById(matchReportDto.matchId);
            var oldMatch = Match.Clone(match);
            var draw = _drawRepository.FindById(match.DrawId);
            
            //Format the string and check for errors
            var p1Points = new List<int>();
            var p2Points = new List<int>();
            var setScores = matchReportDto.score.Split(" ");
            int validSetCount = 0;
            
            for (int i = 0; i<setScores.Length;i++)
            {
                try
                {
                    var pointStrings = setScores[i].Split("/");
                    var p1 = Int32.Parse(pointStrings[0]); 
                    var p2 = Int32.Parse(pointStrings[1]); 
                    p1Points.Add(p1);
                    p2Points.Add(p2);
                    validSetCount++;
                }
                catch (Exception e)
                {
                    //When formatting goes wrong - break the for loop - this might be when we encounter "__/__"
                    break;
                }
            }
            
            //Check that number of sets is as expected
            var maxSets = draw.Sets == 0 ? draw.Games : draw.Sets * draw.Games; 
            var minSets = (1 + maxSets) / 2;
            if (validSetCount > maxSets) //Too many sets
            {
                throw new TournamentSoftwareException(
                    $"{validSetCount} valid sets in score-string {1}, but a maximum of {maxSets} " +
                    $"allowed in draw with up to {draw.Sets} sets and {draw.Games} games.");
            }
            //Too few sets
            if (validSetCount < minSets && draw.Sets!=0) //This lower bound only holds, if not using sets (only games)
            {
                throw new TournamentSoftwareException(
                    $"{validSetCount} valid sets in score-string {matchReportDto.score}," +
                    $" but at least {minSets} were required in draw with up to " +
                    $"{draw.Sets} sets and {draw.Games} games.");
            }
            
            //Check that points correspond to expected values
            for (int i = 0; i < validSetCount; i++)
            {
                var overMax = p1Points[i] > draw.Points || p2Points[i] > draw.Points;

                if (draw.TieBreaks) //If tie breaks are allowed
                                    //There can be too many points, but only if there is excactly 2 points between scores
                {
                    if (overMax && Math.Abs(p1Points[i]-p2Points[i])!=2)
                    {
                        throw new TournamentSoftwareException(
                            $"Score in score-string {matchReportDto.score} does not follow tie break rules." +
                            $"A max of {draw.Points} is allowed, unless it is a tie break.");
                    }
                }
                else //If tie breaks are not allowed - no score must be greater than max
                {
                    if (overMax)
                    {
                        throw new TournamentSoftwareException(
                            $"Too high point score in score-string {matchReportDto.score}." +
                            $"A max of {draw.Points} is allowed, since tie breaks are disallowed.");
                    }
                }
            }
            
            
            //Find the winner
            int p1Games=0, p2Games=0, p1PointsTotal=0, p2PointsTotal=0;
            if (draw.Sets == 0)
            {
                for (int i = 0; i < validSetCount; i++)
                {
                    p1PointsTotal += p1Points[i];
                    p2PointsTotal += p2Points[i];
                    if (p1Points[i] > p2Points[i])
                    {
                        p1Games++;
                    } else if (p1Points[i] < p2Points[i])
                    {
                        p2Games++;
                    }
                    else
                    {
                        throw new TournamentSoftwareException(
                            $"Game with equal score in score-string {matchReportDto.score}");
                    }
                }
                
            }
            else
            {
                throw new NotImplementedException("Using sets is not yet supported");
            }

            if (p1Games > p2Games)
            {
                match.P1Won = true;
            }
            else if (p1Games < p2Games)
            {
                match.P1Won = false;
            }
            else
            {
                //Winner is decided by who won the most points
                match.P1Won = p1PointsTotal > p2PointsTotal;
            }
            
            //Set variables
            match.P1PointsArray = p1Points.ToArray();
            match.P2PointsArray = p2Points.ToArray();
            match.P1Games = p1Games;
            match.P2Games = p2Games;
            match.Status = Status.FINISHED;

            //Update the match using existing Update method and return
            return Update(match,oldMatch);

        }
    }
}