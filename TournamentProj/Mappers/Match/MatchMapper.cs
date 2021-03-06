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
                P1Match = dto.P1Match,
                P1Games = dto.P1Games,
                P1Sets = dto.P1Sets,
                P1Points = dto.P2Points,
                P2Match = dto.P2Match,
                P2Games = dto.P2Games,
                P2Sets = dto.P2Sets,
                P2Points = dto.P2Points
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
                P1Match = match.P1Match,
                P1Games = match.P1Games,
                P1Sets = match.P1Sets,
                P1Points = match.P2Points,
                P2Match = match.P2Match,
                P2Games = match.P2Games,
                P2Sets = match.P2Sets,
                P2Points = match.P2Points
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