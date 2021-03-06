using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TournamentProj.Context;
using TournamentProj.Model;

namespace TournamentProj.DAL
{
    public class DrawRepository : IDrawRepository
    {
        private readonly ITournamentContext _context;
        private readonly DbSet<Draw> _dbSet;
        public DrawRepository(ITournamentContext context)
        {
            _context = context;
            _dbSet = context.Draws;
        }
        
        public IEnumerable<Draw> FindAll()
        {
            return _dbSet.ToArray();
        }

        public Draw FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(Draw draw)
        {
            _dbSet.Add(draw);
        }

        public void Delete(int id)
        {
            var result = _dbSet.Find(id);
            _dbSet.Remove(result);
        }

        public void Delete(Draw draw)
        {
            _dbSet.Remove(draw);
        }

        public void Update(Draw draw)
        {
            _dbSet.Update(draw);
        }
        
        
    }
    
    
    
    
}