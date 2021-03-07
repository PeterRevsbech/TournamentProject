using System.Collections.Generic;
using TournamentProj.Model;

namespace TournamentProj.Services
{
    public interface IMatchService
    {
        Match Create(Match match);

        Match Get(int id);

        IEnumerable<Match> GetByPlayerId(int playerId);

        IEnumerable<Match> GetAll();
        
        Match Delete(int id);

        Match Update(Match match);
    }
}