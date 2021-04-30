using System.Collections.Generic;
using TournamentProj.DAL;
using TournamentProj.DAL.MatchDependencyRepository;
using TournamentProj.DTO.Match;
using TournamentProj.Model;

namespace TournamentProj.Services.MatchService
{
    public interface IMatchService
    {
        Match Create(Match match);

        Match Get(int id);

        IEnumerable<Match> GetByPlayerId(int playerId);
        IEnumerable<Match> GetByDrawId(int drawId);

        IEnumerable<Match> GetAll();
        
        Match Delete(int id);

        Match Update(Match match);
        Match UpdateScoreReport(MatchReportDTO matchReportDto);
        
    }
}