using System.Collections.Generic;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

namespace TournamentProj.Services.TournamentService
{
    public interface ITournamentService
    {
        Tournament Create(Tournament tournament);

        Tournament Get(int id);

        IEnumerable<Tournament> GetAll();

        Tournament Delete(int id);

        Tournament Update(Tournament tournament);
    }
}