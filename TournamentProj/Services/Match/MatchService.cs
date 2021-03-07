using System.Collections.Generic;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.Model;

namespace TournamentProj.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITournamentContext _dbContext;

        public MatchService(ITournamentContext dbContext, IMatchRepository matchRepository)
        {
            _dbContext = dbContext;
            _matchRepository = matchRepository;
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

        public IEnumerable<Match> GetByPlayerId (int playerId)
        {
            return _matchRepository.FindByPlayerId(playerId);
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