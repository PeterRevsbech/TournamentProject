using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TournamentProj.Context;
using TournamentProj.DTO;
using TournamentProj.DTO.Player;
using TournamentProj.Model;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Player")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IDatabaseContext _dbContext;
        private PlayerMapper _playerMapper;

        public PlayerController(ILogger<PlayerController> logger, IDatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _playerMapper = new PlayerMapper();
        }

        [HttpGet]
        public IEnumerable<PlayerDTO> Get()
        {
            return _playerMapper.ToDtoArray(_dbContext.Players.ToArray());
        }
        
        [Route("{tournamentId:int}")]
        [HttpGet]
        public IActionResult Get(int tournamentId)
        {
            var tournament = _dbContext.Tournaments.Find(tournamentId);
            return Ok(_playerMapper.ToDTO(tournament));
        }
        
        [HttpPost]
        public IActionResult Post(JObject payload)
        {
            var tournamentDto = JsonConvert.DeserializeObject<PlayerDTO>(payload.ToString());
            var tournament = _playerMapper.FromDTO(tournamentDto);
            _dbContext.Tournaments.Add(tournament);
            _dbContext.SaveChanges();
            
            return Ok(_playerMapper.ToDTO(tournament));
        }
        
        [HttpPut]
        public IActionResult Put(JObject payload)
        {
            var tournamentDto = JsonConvert.DeserializeObject<PlayerDTO>(payload.ToString());
            var tournament = _tournamentMapper.FromDTO(tournamentDto);

            _dbContext.Tournaments.Update(tournament);
            _dbContext.SaveChanges();
            return Ok(tournament);
        }
        
        [Route("{tournamentId:int}")]
        [HttpDelete]
        public Tournament Delete(int tournamentId)
        {
            var tournament = _dbContext.Tournaments.Find(tournamentId);
            _dbContext.Tournaments.Remove(tournament);
            _dbContext.SaveChanges();
            return tournament;
        }
        
        
    }
}