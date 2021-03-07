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
        private readonly DbSet<Tournament> _dbSet;

        public TournamentRepository(ITournamentContext context)
        {
            _dbSet = context.Tournaments;
        }
        
        public IEnumerable<Tournament> FindAll()
        {
            //Include laver et join kald i databasen ==> join tournament med draws
            var x =_dbSet
                .Include(tournament => tournament.Draws)
                .ThenInclude(draw => draw.Matches)
                .Include(tournament => tournament.Players)
                .ToArray();
            return x;
        }

        public Tournament FindById(int id)
        {
            //Det her er LINQ queries
            var result= _dbSet.Where(tournament => id == tournament.Id)
                .Include(tournament => tournament.Draws)
                .ThenInclude(draw => draw.Matches)
                .Include(tournament => tournament.Players)
                .FirstOrDefault();
            
            return result;
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