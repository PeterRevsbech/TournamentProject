using System.Collections.Generic;
using TournamentProj.DTO.MatchDependencyDTO;
using TournamentProj.Model;

namespace TournamentProj.Mappers.MatchDependencyMapper
{
    public interface IMatchDependencyMapper
    {
        public MatchDependency FromDTO(MatchDependencyDTO dto);
        
        public MatchDependencyDTO ToDTO(MatchDependency matchDependency);

        public IEnumerable<MatchDependencyDTO> ToDtoArray(IEnumerable<MatchDependency> matchDependencies);
        
    }
}