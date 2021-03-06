using System.Collections.Generic;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.Model;

namespace TournamentProj.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _matchRepository;
        private readonly ITournamentContext _dbContext;

        public PlayerService(ITournamentContext dbContext)
        {
            _dbContext = dbContext;
            //TODO maybe not so good to use constructor here
            _matchRepository = new PlayerRepository(dbContext);
        }

        
        public Player Create(Player player)
        {
            _matchRepository.Insert(player);
            _dbContext.SaveChanges();
            return player;
        }

        public Player Get(int id)
        {
            return _matchRepository.FindById(id);
        }

        public IEnumerable<Player> GetAll()
        {
            return _matchRepository.FindAll();
        }

        public Player Delete(int id)
        {
            var player = _matchRepository.FindById(id);
            _matchRepository.Delete(player);
            _dbContext.SaveChanges();
            return player;
        }

        public Player Update(Player player)
        {
            _matchRepository.Update(player);
            _dbContext.SaveChanges();
            return player;
        }
    }
}