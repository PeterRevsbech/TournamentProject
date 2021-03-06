using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TournamentProj.Context;
using TournamentProj.Model;

namespace TournamentProj.DAL
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly ITournamentContext _context;
        private readonly DbSet<Tournament> _dbSet;

        public TournamentRepository(ITournamentContext context)
        {
            _context = context;
            _dbSet = context.Tournaments;
        }
        
        public IEnumerable<Tournament> FindAll()
        {
            var x =_dbSet.ToArray();
            return _dbSet.ToArray();
        }

        public Tournament FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(Tournament entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var result = _dbSet.Find(id);
            _dbSet.Remove(result);
        }

        public void Delete(Tournament tournament)
        {
            _dbSet.Remove(tournament);
        }

        public void Update(Tournament tournament)
        {
            _dbSet.Update(tournament);
        }
    }
}