using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TournamentProj.Context;
using TournamentProj.Model;

namespace TournamentProj.DAL
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ITournamentContext _context;
        private readonly DbSet<Match> _dbSet;
        public MatchRepository(ITournamentContext context)
        {
            _context = context;
            _dbSet = context.Matches;
        }
        
        public IEnumerable<Match> FindAll()
        {
            return _dbSet.ToArray();
        }

        public Match FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(Match match)
        {
            _dbSet.Add(match);
        }

        public void Delete(int id)
        {
            var result = _dbSet.Find(id);
            _dbSet.Remove(result);
        }

        public void Delete(Match match)
        {
            _dbSet.Remove(match);
        }

        public void Update(Match match)
        {
            _dbSet.Update(match);
        }
    }
}