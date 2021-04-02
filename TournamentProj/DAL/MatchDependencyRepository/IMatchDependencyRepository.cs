using System.Collections.Generic;
using TournamentProj.Model;

namespace TournamentProj.DAL.MatchDependencyRepository
{
    public interface IMatchDependencyRepository
    {
        public IEnumerable<MatchDependency> FindAll();

        public MatchDependency FindById(int id);
        
        public IEnumerable<MatchDependency> FindByMatchId(int matchId);

        public void Insert(MatchDependency matchDependency);
        
        public void Delete(int id);

        public void Delete(MatchDependency matchDependency);

        public void Update(MatchDependency matchDependency);
    }
}