using System.Collections.Generic;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

namespace TournamentProj.Services.tournament
{
    public interface ITournamentService
    {
        Tournament Create(Tournament tournament);

        Tournament Get(int id);

        IEnumerable<Tournament> GetAll();

        IEnumerable<Tournament> GetFromUserId(int userId);

        Tournament Delete(int id);

        Tournament Update(Tournament tournament);
    }
}