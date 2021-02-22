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
        
        
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _dbContext.Players.Find(id);
            return Ok(_playerMapper.ToDTO(result));
        }
        
        [HttpPost]
        public IActionResult Post(JObject payload)
        {
            var dto = JsonConvert.DeserializeObject<PlayerDTO>(payload.ToString());
            var result = _playerMapper.FromDTO(dto);
            _dbContext.Players.Add(result);
            _dbContext.SaveChanges();
            
            return Ok(_playerMapper.ToDTO(result));
        }
        
        [HttpPut]
        public IActionResult Put(JObject payload)
        {
            var dto = JsonConvert.DeserializeObject<PlayerDTO>(payload.ToString());
            var result = _playerMapper.FromDTO(dto);

            _dbContext.Players.Update(result);
            _dbContext.SaveChanges();
            return Ok(result);
        }
        
        [Route("{id:int}")]
        [HttpDelete]
        public Tournament Delete(int id)
        {
            var result = _dbContext.Tournaments.Find(id);
            _dbContext.Tournaments.Remove(result);
            _dbContext.SaveChanges();
            return result;
        }
        
        
    }
}