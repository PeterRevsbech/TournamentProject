using System.Collections.Generic;
using TournamentProj.Model;

namespace TournamentProj.Services
{
    public interface IDrawService
    {
        Draw Create(Draw draw);

        Draw Get(int id);

        IEnumerable<Draw> GetAll();

        IEnumerable<Draw> GetFromUserId(int userId);

        Draw Delete(int id);

        Draw Update(Draw tournament);
    }
}