using System.Collections.Generic;
using TournamentProj.Model;

namespace TournamentProj.Services.MatchDependencyService
{
    public interface IMatchDependencyService
    {
        MatchDependency Create(MatchDependency matchDependency);

        MatchDependency Get(int id);
        
        IEnumerable<MatchDependency> GetByMatchId(int matchId);

        IEnumerable<MatchDependency> GetAll();

        MatchDependency Delete(int id);

        MatchDependency Update(MatchDependency matchDependency);
    }
}