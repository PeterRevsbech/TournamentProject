using System.Collections.Generic;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.Model;

namespace TournamentProj.Services
{
    public class DrawService
    {
        private readonly IDrawRepository _drawRepository;
        private readonly ITournamentContext _dbContext;

        public DrawService(ITournamentContext dbContext)
        {
            _dbContext = dbContext;
            //TODO maybe not so good to use constructor here
            _drawRepository = new DrawRepository(dbContext);
        }

        /*
        public Tournament Create(Tournament tournament)
        {
            _tournamentRepository.Insert(tournament);
            _dbContext.SaveChanges();
            return tournament;
        }

        public Tournament Get(int id)
        {
            return _tournamentRepository.FindById(id);
        }

        public IEnumerable<Tournament> GetAll()
        {
            return _tournamentRepository.FindAll();
        }

        public IEnumerable<Tournament> GetFromUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Tournament Delete(int id)
        {
            var tournament = _tournamentRepository.FindById(id);
            _tournamentRepository.Delete(tournament);
            _dbContext.SaveChanges();
            return tournament;
        }

        public Tournament Update(Tournament tournament)
        {
            _tournamentRepository.Update(tournament);
            _dbContext.SaveChanges();
            return tournament;
        }
        */
    }
}