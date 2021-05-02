using System.Collections.Generic;
using System.Linq;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.Exceptions;
using TournamentProj.Model;

namespace TournamentProj.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IDrawRepository _drawRepository;
        private readonly ITournamentContext _dbContext;
        

        public PlayerService(ITournamentContext dbContext,
            IPlayerRepository playerRepository,
            IMatchRepository matchRepository,
            IDrawRepository drawRepository)
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
            
            foreach (var playerId in playerIds)
            {
                players.Add(_playerRepository.FindById(playerId));
            }

            return players;
        }

        public Player Delete(int id)
        {
            var player = _playerRepository.FindById(id);
            //Check that no draw exists with matches, with this player
            
            //1) Player belongs to a tournament - find all draws that this tournament has
            //2) For each draw - get all matches and check if player is in one of the matches
            var draws = _drawRepository.FindByTournamentId(player.TournamentId);
            foreach (var draw in draws)
            {
                foreach (var match in draw.Matches)
                {
                    if (id == match.P1Id || id == match.P2Id)
                    {
                        throw new TournamentSoftwareException("Cannot delete a player, that is in a match.");
                    }
                }
            }
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