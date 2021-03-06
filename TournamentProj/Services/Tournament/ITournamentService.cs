using System.Collections.Generic;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

namespace TournamentProj.Services.tournament
{
    public interface ITournamentService
    {
        Tournament Create(TournamentDTO tournamentDTO);

        Tournament Get(int id);

        IEnumerable<TournamentDTO> GetAll();

        IEnumerable<TournamentDTO> GetFromUserId(int userId);

        void Delete(int id);
    }
}