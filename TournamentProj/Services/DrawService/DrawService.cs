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
            var generatedDraw = DrawCreator.GenerateDraw(drawCreation,_drawRepository,_matchRepository,_matchDependencyRepository);
            Create(generatedDraw);
            UpdateAllByeMatches(generatedDraw);
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
                //If match contains a bye
                if (match.P1Id == -1 || match.P2Id == -1)
                {
                    match.Status = Status.FINISHED;
                    match.P1Won = (match.P2Id == -1); // Player that is not a bye has now won
                    //Update players in matches that depend on this one
                    var dependentMatches = draw.Matches
                        .Where(m => (m.P1DependencyId == match.Id || m.P2DependencyId == match.Id));
                    
                    //Go through matches that depend on this one to update their dependencies
                    foreach (var dependentMatch in dependentMatches)
                    {
                        MatchDependency matchDependency;
                        if (dependentMatch.P1DependencyId == match.Id)
                        {
                            matchDependency = _matchDependencyRepository.FindById(dependentMatch.P1DependencyId);
                            dependentMatch.P1Id = FindResultingPlayerFromMatchDepdendency(matchDependency);
                        }
                        else
                        {
                            matchDependency = _matchDependencyRepository.FindById(dependentMatch.P2DependencyId);
                            dependentMatch.P2Id = FindResultingPlayerFromMatchDepdendency(matchDependency);
                        }
                    }
                } 
            }
        }

        private int FindResultingPlayerFromMatchDepdendency(MatchDependency matchDependency)
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