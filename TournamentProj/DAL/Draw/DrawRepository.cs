using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TournamentProj.Context;
using TournamentProj.Model;

namespace TournamentProj.DAL
{
    public class DrawRepository : IDrawRepository
    {
        private readonly DbSet<Draw> _dbSet;
        public DrawRepository(ITournamentContext context)
        {
            _dbSet = context.Draws;
        }
        
        public IEnumerable<Draw> FindAll()
        {
            var result =_dbSet
                .Include(draw => draw.Matches)
                .ToArray();
            return result;
        }

        public Draw FindById(int id)
        {
            var result= _dbSet.Where(draw => id == draw.Id)
                .Include(draw => draw.Matches)
                .FirstOrDefault();

            return result;
        }

        public void Insert(Draw draw)
        {
            //TODO få den til at genkende den tournament, den hører til
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