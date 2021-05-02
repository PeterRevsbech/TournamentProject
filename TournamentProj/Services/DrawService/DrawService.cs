using System.Collections.Generic;
using System.Linq;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.DAL.MatchDependencyRepository;
using TournamentProj.Model;

namespace TournamentProj.Services.DrawService
{
    public class DrawService : IDrawService
    {
        private readonly IDrawRepository _drawRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchDependencyRepository _matchDependencyRepository;
        private readonly ITournamentContext _dbContext;

        public DrawService(ITournamentContext dbContext,
            IDrawRepository drawRepository,
            IMatchRepository matchRepository,
            IMatchDependencyRepository matchDependencyRepository)
        {
            _dbContext = dbContext;
            _drawRepository = drawRepository;
            _matchRepository = matchRepository;
            _matchDependencyRepository = matchDependencyRepository;
        }

        
        public Draw Create(Draw draw)
        {
            _drawRepository.Insert(draw);
            _dbContext.SaveChanges();
            return draw;
        }

        public Draw CreateAutomatic(DrawCreation drawCreation)
        {
            //IMPORTANT: Here it is important to first create the draw using SaveChanges() method ==> THen update the draw using it again
            var generatedDraw = DrawCreator.GenerateDraw(drawCreation,_drawRepository,_matchRepository,_matchDependencyRepository, _dbContext);
            Create(generatedDraw);
            UpdateAllByeMatches(generatedDraw);
            Update(generatedDraw);
            return generatedDraw;
        }

        public Draw Get(int id)
        {
            return _drawRepository.FindById(id);
        }

        public IEnumerable<Draw> GetAll()
        {
            return _drawRepository.FindAll();
        }

        public Draw Delete(int id)
        {
            var draw = _drawRepository.FindById(id);
            _drawRepository.Delete(draw);
            _dbContext.SaveChanges();
            return draw;
        }

        public Draw Update(Draw draw)
        {
            _drawRepository.Update(draw);
            _dbContext.SaveChanges();
            return draw;
        }


        private void UpdateAllByeMatches(Draw draw)
        {
            //Go through all mathces
            foreach (var match in draw.Matches)
            {
                if (match.P1Id == Player.BYE_ID || match.P2Id == Player.BYE_ID)
                {
                    ExecuteByeMatch(draw,match);
                    _matchRepository.Update(match);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------
        //The following methods are copied from MatchService to avoid bad dependencies
        private void ExecuteByeMatch(Draw draw, Match match)
        {
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
                
                for (int i = 0; i < minGames; i++)
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
            
            
            
            //For each dependent match - update the players
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


    }
}