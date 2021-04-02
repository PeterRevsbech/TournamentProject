using System.Collections.Generic;
using TournamentProj.DTO.MatchDependencyDTO;
using TournamentProj.Model;

namespace TournamentProj.Mappers.MatchDependencyMapper
{
    public class MatchDependencyMapper : IMatchDependencyMapper
    {
        public MatchDependency FromDTO(MatchDependencyDTO dto)
        {
            var matchDependency = new MatchDependency()
            {
                Id = dto.Id,
                dependencyId = dto.dependencyId,
                dependsOnDraw = dto.dependsOnDraw,
                position = dto.position
            };

            return matchDependency;
        }

        public MatchDependencyDTO ToDTO(MatchDependency matchDependency)
        {
            var dto = new MatchDependencyDTO()
            {
                Id = matchDependency.Id,
                dependencyId = matchDependency.dependencyId,
                dependsOnDraw = matchDependency.dependsOnDraw,
                position = matchDependency.position
            };

            return dto;
        }

        public IEnumerable<MatchDependencyDTO> ToDtoArray(IEnumerable<MatchDependency> list)
        {
            foreach (var item in list)
            {
                yield return ToDTO(item);
            }
        }
    }
}