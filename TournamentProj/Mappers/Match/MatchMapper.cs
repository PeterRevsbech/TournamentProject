using System.Collections.Generic;
using TournamentProj.DTO.Match;
using TournamentProj.Model;

namespace TournamentProj.Mappers
{
    public class MatchMapper
    {
        public Match FromDTO(MatchDTO dto)
        {
            var match = new Match()
            {
               p1Id = dto.p1Id,
               p2Id = dto.p2Id,
               p1Won = dto.p1Won,
               p1Match = dto.p1Match,
               p1Games = dto.p1Games,
               p1Sets = dto.p1Sets,
               p1Points = dto.p2Points,
               p2Match = dto.p2Match,
               p2Games = dto.p2Games,
               p2Sets = dto.p2Sets,
               p2Points = dto.p2Points

            };

            return match;
        }

        public MatchDTO ToDTO(Match match)
        {
            var dto = new MatchDTO()
            {
                p1Id = match.p1Id,
                p2Id = match.p2Id,
                p1Won = match.p1Won,
                p1Match = match.p1Match,
                p1Games = match.p1Games,
                p1Sets = match.p1Sets,
                p1Points = match.p2Points,
                p2Match = match.p2Match,
                p2Games = match.p2Games,
                p2Sets = match.p2Sets,
                p2Points = match.p2Points
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
    }
}