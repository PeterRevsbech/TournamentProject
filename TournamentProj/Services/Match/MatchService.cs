using System.Collections.Generic;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.Model;

namespace TournamentProj.Services
{
    public class MatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITournamentContext _dbContext;

        public MatchService(ITournamentContext dbContext)
        {
            _dbContext = dbContext;
            //TODO maybe not so good to use constructor here
            _matchRepository = new MatchRepository(dbContext);
        }

        
        public Match Create(Match match)
        {
            _matchRepository.Insert(match);
            _dbContext.SaveChanges();
            return match;
        }

        public Match Get(int id)
        {
            return _matchRepository.FindById(id);
        }

        public IEnumerable<Match> GetAll()
        {
            return _matchRepository.FindAll();
        }

        public Match Delete(int id)
        {
            var match = _matchRepository.FindById(id);
            _matchRepository.Delete(match);
            _dbContext.SaveChanges();
            return match;
        }

        public Match Update(Match match)
        {
            _matchRepository.Update(match);
            _dbContext.SaveChanges();
            return match;
        }
    }
}