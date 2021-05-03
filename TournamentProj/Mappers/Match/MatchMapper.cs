using System.Collections.Generic;
using TournamentProj.DTO.Match;
using TournamentProj.Model;

namespace TournamentProj.Mappers
{
    public class MatchMapper : IMatchMapper
    {
        public Match FromDTO(MatchDTO dto)
        {
            var match = new Match()
            {
                Id = dto.Id,
                DrawId = dto.DrawId,
                P1Id = dto.P1Id,
                P2Id = dto.P2Id,
                Status = DTOToModelStatus(dto.statusDTO),
                P1Won = dto.P1Won,
                P1Games = dto.P1Games,
                P1PointsArray = dto.P1Points,
                P2Games = dto.P2Games,
                P2PointsArray = dto.P2Points,
                P1DependencyId = dto.P1DependencyId,
                P2DependencyId = dto.P2DependencyId,
                round = dto.round
            };

            return match;
        }

        public MatchDTO ToDTO(Match match)
        {
            var dto = new MatchDTO()
            {
                Id = match.Id,
                DrawId = match.DrawId,
                P1Id = match.P1Id,
                P2Id = match.P2Id,
                statusDTO = ModelStatusToDTO(match.Status),
                P1Won = match.P1Won,
                P1Games = match.P1Games,
                P1Points = match.P1PointsArray,
                P2Games = match.P2Games,
                P2Points = match.P2PointsArray,
                P1DependencyId = match.P1DependencyId,
                P2DependencyId = match.P2DependencyId,
                round = match.round
            };

            return dto;
        }

        public IEnumerable<MatchDTO> ToDtoArray(IEnumerable<Match> list)
        {
            foreach (var item in list)
            {
                yield return ToDTO(item);
            }
        }

        private StatusDTO ModelStatusToDTO(Status status )
        {
            switch (status)
            {
                case Status.OPEN:
                    return StatusDTO.OPEN;
                case Status.ACTIVE:
                    return StatusDTO.ACTIVE;
                case Status.CLOSED:
                    return StatusDTO.CLOSED;
                case Status.FINISHED:
                    return StatusDTO.FINISHED;
                default:
                    return StatusDTO.CLOSED;
            }
        }
        
        private Status DTOToModelStatus(StatusDTO statusDTO )
        {
            switch (statusDTO)
            {
                case StatusDTO.OPEN:
                    return Status.OPEN;
                case StatusDTO.ACTIVE:
                    return Status.ACTIVE;
                case StatusDTO.CLOSED:
                    return Status.CLOSED;
                case StatusDTO.FINISHED:
                    return Status.FINISHED;
                default:
                    return Status.CLOSED;
            }
        }
    }
    
    
}