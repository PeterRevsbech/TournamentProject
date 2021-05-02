using System.Collections.Generic;
using TournamentProj.Model;

namespace TournamentProj.DAL
{
    public interface IDrawRepository
    {
        public IEnumerable<Draw> FindAll();

        public Draw FindById(int id);

        public IEnumerable<Draw> FindByTournamentId(int tournamentId);
        
        public void Insert(Draw draw);
        
        public void Delete(int id);

        public void Delete(Draw draw);

        public void Update(Draw draw);
    }
}