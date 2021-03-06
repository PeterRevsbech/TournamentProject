using System.Collections.Generic;
using TournamentProj.DAL;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

namespace TournamentProj.Services.tournament
{
    public class TournamentService : ITournamentService
    {
        private readonly IRepository<Tournament> tournamentRepository;

        public TournamentService(IRepository<Tournament> tournamentRepository)
        {
            this.tournamentRepository = tournamentRepository;
        }

        
        public Tournament Create(TournamentDTO tournamentDTO)
        {
            throw new System.NotImplementedException();
        }

        public Tournament Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TournamentDTO> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TournamentDTO> GetFromUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}