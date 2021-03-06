using System.Collections;
using System.Collections.Generic;
using TournamentProj.Model;
namespace TournamentProj.DAL
{
    public interface ITournamentRepository
    {

        public IEnumerable<Tournament> FindAll();

        public Tournament FindById(int id);
        public void Insert(Tournament entity);
        
        public void Delete(int id);

        public void Delete(Tournament tournament);

        public void Update(Tournament tournament);
    }
    
}