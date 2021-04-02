using System.Collections.Generic;
using TournamentProj.Context;
using TournamentProj.DAL.MatchDependencyRepository;
using TournamentProj.Model;

namespace TournamentProj.Services.MatchDependencyService
{
    public class MatchDependencyService : IMatchDependencyService
    {
        private readonly IMatchDependencyRepository _matchDependencyRepository;
        private readonly ITournamentContext _dbContext;
        
        public MatchDependencyService(ITournamentContext dbContext, IMatchDependencyRepository matchDependencyRepository)
        {
            _dbContext = dbContext;
            _matchDependencyRepository = matchDependencyRepository;
        }
        
        
        public MatchDependency Create(MatchDependency matchDependency)
        {
            _matchDependencyRepository.Insert(matchDependency);
            _dbContext.SaveChanges();
            return matchDependency;
        }

        public MatchDependency Get(int id)
        {
            return _matchDependencyRepository.FindById(id);
        }

        public IEnumerable<MatchDependency> GetByMatchId(int matchId)
        {
            return _matchDependencyRepository.FindByMatchId(matchId);
        }

        public IEnumerable<MatchDependency> GetAll()
        {
            return _matchDependencyRepository.FindAll();
        }

        public MatchDependency Delete(int id)
        {
            var matchDependency = _matchDependencyRepository.FindById(id);
            _matchDependencyRepository.Delete(matchDependency);
            _dbContext.SaveChanges();
            return matchDependency;
        }

        public MatchDependency Update(MatchDependency matchDependency)
        {
            _matchDependencyRepository.Update(matchDependency);
            _dbContext.SaveChanges();
            return matchDependency;
        }
    }
}