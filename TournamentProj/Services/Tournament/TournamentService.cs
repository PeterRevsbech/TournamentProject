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

        public TournamentService(ITournamentContext dbContext)
        {
            //TODO maybe not so good to use constructor here
            _tournamentRepository = new TournamentRepository(dbContext);
        }

        
        public Tournament Create(Tournament tournament)
        {
            _tournamentRepository.Insert(tournament);
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

        public void Delete(int id)
        {
            _tournamentRepository.Delete(id);
        }
    }
}