using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TournamentProj.Context;
using TournamentProj.Model;

namespace TournamentProj.DAL.MatchDependencyRepository
{
    public class MatchDependencyRepository : IMatchDependencyRepository
    {
        private readonly DbSet<MatchDependency> _dbSet;
        
        public MatchDependencyRepository(ITournamentContext context)
        {
            _dbSet = context.MatchDependencies;
        }

        public IEnumerable<MatchDependency> FindAll()
        {
            return _dbSet.ToArray();
        }

        public MatchDependency FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<MatchDependency> FindByMatchId(int matchId)
        {
            return _dbSet.Where(MatchDependency => MatchDependency.MatchId == matchId).ToArray();
        }

        public void Insert(MatchDependency matchDependency)
        {
            _dbSet.Add(matchDependency);
        }

        public void Delete(int id)
        {
            var result = _dbSet.Find(id);
            _dbSet.Remove(result);
        }

        public void Delete(MatchDependency matchDependency)
        {
            _dbSet.Remove(matchDependency);
        }

        public void Update(MatchDependency matchDependency)
        {
            _dbSet.Update(matchDependency);
        }
    }
}