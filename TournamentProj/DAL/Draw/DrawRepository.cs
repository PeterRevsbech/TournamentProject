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
    }
}