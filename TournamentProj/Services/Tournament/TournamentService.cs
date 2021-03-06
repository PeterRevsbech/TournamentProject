using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

namespace TournamentProj.Services.tournament
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentContext _dbContext;

        public TournamentService(ITournamentContext dbContext)
        {
            _dbContext = dbContext;
            //TODO maybe not so good to use constructor here
            _tournamentRepository = new TournamentRepository(dbContext);
        }

        
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
    }
}