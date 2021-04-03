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
                DependencyId = dto.DependencyId,
                DependsOnDraw = dto.DependsOnDraw,
                Position = dto.Position
            };

            return matchDependency;
        }

        public MatchDependencyDTO ToDTO(MatchDependency matchDependency)
        {
            var dto = new MatchDependencyDTO()
            {
                Id = matchDependency.Id,
                DependencyId = matchDependency.DependencyId,
                DependsOnDraw = matchDependency.DependsOnDraw,
                Position = matchDependency.Position
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