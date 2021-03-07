using System.Collections.Generic;
using System.Linq;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.Model;

namespace TournamentProj.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IDrawRepository _drawRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly ITournamentContext _dbContext;

        public PlayerService(ITournamentContext dbContext,
            IPlayerRepository playerRepository,
            IDrawRepository drawRepository,
            IMatchRepository matchRepository)
        {
            _dbContext = dbContext;
            _playerRepository = playerRepository;
            _matchRepository = matchRepository;
            _drawRepository = drawRepository;
        }

        
        public Player Create(Player player)
        {
            _playerRepository.Insert(player);
            _dbContext.SaveChanges();
            return player;
        }

        public Player Get(int id)
        {
            return _playerRepository.FindById(id);
        }

        public IEnumerable<Player> GetAll()
        {
            return _playerRepository.FindAll();
        }

        public IEnumerable<Player> GetFromDrawId(int drawId)
        {
            //Get all matches of the draw
            var matches = _matchRepository.FindByDrawId(drawId);
            
            //Get the playerIds from all the matches with no duplicates
            var playerIds = new HashSet<int>();
            foreach (var match in matches)
            {
                playerIds.Add(match.P1Id);
                playerIds.Add(match.P2Id);
            }
            
            
            //Get all those players
            var players = new List<Player>();
            
            foreach (var playerId in playerIds)//TODO do this with LINQ
            {
                players.Add(_playerRepository.FindById(playerId));
            }

            return players;
        }

        public Player Delete(int id)
        {
            var player = _playerRepository.FindById(id);
            _playerRepository.Delete(player);
            _dbContext.SaveChanges();
            return player;
        }

        public Player Update(Player player)
        {
            _playerRepository.Update(player);
            _dbContext.SaveChanges();
            return player;
        }
    }
}