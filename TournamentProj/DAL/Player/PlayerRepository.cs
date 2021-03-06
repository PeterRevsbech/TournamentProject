using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TournamentProj.Context;
using TournamentProj.Model;

namespace TournamentProj.DAL
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ITournamentContext _context;
        private readonly DbSet<Player> _dbSet;
        public PlayerRepository(ITournamentContext context)
        {
            _context = context;
            _dbSet = context.Players;
        }
        
        public IEnumerable<Player> FindAll()
        {
            return _dbSet.ToArray();
        }

        public Player FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(Player player)
        {
            _dbSet.Add(player);
        }

        public void Delete(int id)
        {
            var result = _dbSet.Find(id);
            _dbSet.Remove(result);
        }

        public void Delete(Player player)
        {
            _dbSet.Remove(player);
        }

        public void Update(Player player)
        {
            _dbSet.Update(player);
        }
    }
}