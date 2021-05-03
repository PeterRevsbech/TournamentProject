using System.Collections.Generic;
using System.Linq;
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
                DrawType = DrawTypeFromDTO(dto.DrawTypeDTO),
                Points = dto.Points,
                Games = dto.Games,
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
                DrawTypeDTO = DrawTypeToDTO(draw.DrawType),
                Points = draw.Points,
                Games = draw.Games,
                TieBreaks = draw.TieBreaks,
                MatchIds = new List<int>()
            };
            if (draw.Matches != null && draw.Matches.ToArray().Length > 0)
            {
                foreach (var match in draw.Matches)
                {
                    dto.MatchIds.Add(match.Id);
                }
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

        public DrawCreation FromCreationDTO(DrawCreationDTO dto)
        {
            DrawCreation drawCreation = new DrawCreation()
            {
                TournamentId = dto.TournamentId,
                playerIds = dto.playerIds,
                DrawType = DrawTypeFromDTO(dto.DrawTypeDTO),
                Name = dto.Name,
                Games = dto.Games,
                Points = dto.Points,
                TieBreaks = dto.TieBreaks,
                playerIdsSeeded = dto.playerIdsSeeded
            };
            return drawCreation;
        }
        
        public DrawCreationDTO ToCreationDTO(DrawCreation drawCreation)
        {
            var dto = new DrawCreationDTO()
            {
                TournamentId = drawCreation.TournamentId,
                playerIds = drawCreation.playerIds,
                DrawTypeDTO = DrawTypeToDTO(drawCreation.DrawType),
                Name = drawCreation.Name,
                Games = drawCreation.Games,
                Points = drawCreation.Points,
                TieBreaks = drawCreation.TieBreaks,
                playerIdsSeeded = drawCreation.playerIdsSeeded
            };
            return dto;
        }

        private DrawTypeDTO DrawTypeToDTO(DrawType drawType)
        {
            switch (drawType)
            {
                case DrawType.KO:
                    return DrawTypeDTO.KO;
                case DrawType.RR:
                    return DrawTypeDTO.RR;
                case DrawType.MONRAD:
                    return DrawTypeDTO.MONRAD;
                default:
                    return 0;
            }
        }
        
        private DrawType DrawTypeFromDTO(DrawTypeDTO dto)
        {
            switch (dto)
            {
                case DrawTypeDTO.KO:
                    return DrawType.KO;
                case DrawTypeDTO.RR:
                    return DrawType.RR;
                case DrawTypeDTO.MONRAD:
                    return DrawType.MONRAD;
                default:
                    return 0;
            }
        }
    }
}
  