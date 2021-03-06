using System.Collections.Generic;
using TournamentProj.DTO.Draw;
using TournamentProj.Model;

namespace TournamentProj.Mappers
{
    public class DrawMapper : IDrawMapper
    {
        public Draw FromDTO(DrawDTO dto)
        {
            var draw = new Draw()
            {
                Name = dto.Name,
                Id = dto.Id,
                TournamentId = dto.TournamentId,
                Points = dto.Points,
                Games = dto.Games,
                Sets = dto.Sets,
                TieBreaks = dto.TieBreaks
            };

            return draw;
        }

        public DrawDTO ToDTO(Draw draw)
        {
            var dto = new DrawDTO()
            {
                Name = draw.Name,
                Id = draw.Id,
                TournamentId = draw.TournamentId,
                Points = draw.Points,
                Games = draw.Games,
                Sets = draw.Sets,
                TieBreaks = draw.TieBreaks,
                MatchIds = new List<int>()
            };
            foreach (var match in draw.Matches)
            {
                dto.MatchIds.Add(match.Id);
            }

            return dto;
        }

        public IEnumerable<DrawDTO> ToDtoArray(IEnumerable<Draw> list)
        {
            foreach (var item in list)
            {
                yield return ToDTO(item);
            }
        }
    }
}
