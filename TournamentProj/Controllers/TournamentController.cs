using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TournamentProj.Context;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Tournament")]
    public class TournamentController : ControllerBase
    {

        private readonly ILogger<TournamentController> _logger;
        private readonly IDatabaseContext _dbContext;
        private TournamentMapper _tournamentMapper;

        public TournamentController(ILogger<TournamentController> logger, IDatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _tournamentMapper = new TournamentMapper();
        }

        [HttpGet]
        public IEnumerable<TournamentDTO> Get()
        {
            return _tournamentMapper.ToDtoArray(_dbContext.Tournaments.ToArray());
        }
        
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _dbContext.Tournaments.Find(id);
            return Ok(_tournamentMapper.ToDTO(result));
        }
        
        [HttpPost]
        public IActionResult Post(JObject payload)
        {
            var tournamentDto = JsonConvert.DeserializeObject<TournamentDTO>(payload.ToString());
            var tournament = _tournamentMapper.FromDTO(tournamentDto);
            _dbContext.Tournaments.Add(tournament);
            _dbContext.SaveChanges();
            
            return Ok(_tournamentMapper.ToDTO(tournament));
        }
        
        [HttpPut]
        public IActionResult Put(JObject payload)
        {
            var tournamentDto = JsonConvert.DeserializeObject<TournamentDTO>(payload.ToString());
            var tournament = _tournamentMapper.FromDTO(tournamentDto);

            _dbContext.Tournaments.Update(tournament);
            _dbContext.SaveChanges();
            return Ok(tournament);
        }
        
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _dbContext.Tournaments.Find(id);
            _dbContext.Tournaments.Remove(result);
            _dbContext.SaveChanges();
            return Ok(_tournamentMapper.ToDTO(result));
        }
        
        
    }
}