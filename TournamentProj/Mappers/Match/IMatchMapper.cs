using System.Collections.Generic;
using TournamentProj.DTO.Match;
using TournamentProj.Model;

namespace TournamentProj.Mappers
{
    public interface IMatchMapper
    {
        public Match FromDTO(MatchDTO dto);
        public MatchDTO ToDTO(Match draw);

        public IEnumerable<MatchDTO> ToDtoArray(IEnumerable<Match> draws);
    }
}