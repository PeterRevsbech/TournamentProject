using System.Collections.Generic;
using TournamentProj.Model;

namespace TournamentProj.DAL
{
    public interface IPlayerRepository
    {
        public IEnumerable<Player> FindAll();

        public Player FindById(int id);

        public void Insert(Player player);
        
        public void Delete(int id);

        public void Delete(Player player);

        public void Update(Player player);
    }
}