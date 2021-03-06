using System.Collections.Generic;
using TournamentProj.Model;

namespace TournamentProj.DAL
{
    public interface IMatchRepository
    {
        public IEnumerable<Match> FindAll();

        public Match FindById(int id);
        public void Insert(Match match);
        
        public void Delete(int id);

        public void Delete(Match match);

        public void Update(Match match);
    }
}