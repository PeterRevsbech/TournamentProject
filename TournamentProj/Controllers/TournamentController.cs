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
        
        [Route("{tournamentId:int}")]
        [HttpGet]
        public IActionResult Get(int tournamentId)
        {
            var tournament = _dbContext.Tournaments.Find(tournamentId);
            return Ok(_tournamentMapper.ToDTO(tournament));
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