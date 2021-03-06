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
using TournamentProj.Services.tournament;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Tournament")]
    public class TournamentController : ControllerBase
    {

        private readonly ILogger<TournamentController> _logger;
        private readonly ITournamentContext _dbContext;
        private ITournamentMapper _mapper;
        private ITournamentService _tournamentService;

        public TournamentController(ILogger<TournamentController> logger, ITournamentContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = new TournamentMapper();
            
            //TODO do this with dependency injection instead
            //dbContext is here from DI pattern. Use it to make nessecary service(s)
            _tournamentService = new TournamentService(dbContext);
        }

        [HttpGet]
        public IEnumerable<TournamentDTO> Get()
        {
            return _mapper.ToDtoArray(_tournamentService.GetAll());
        }
        
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _dbContext.Tournaments.Find(id);
            return Ok(_mapper.ToDTO(result));
        }
        
        [HttpPost]
        public IActionResult Post(JObject payload)
        {
            var tournamentDto = JsonConvert.DeserializeObject<TournamentDTO>(payload.ToString());
            var tournament = _mapper.FromDTO(tournamentDto);
            _dbContext.Tournaments.Add(tournament);
            _dbContext.SaveChanges();
            
            return Ok(_mapper.ToDTO(tournament));
        }
        
        [HttpPut]
        public IActionResult Put(JObject payload)
        {
            var tournamentDto = JsonConvert.DeserializeObject<TournamentDTO>(payload.ToString());
            var tournament = _mapper.FromDTO(tournamentDto);

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
            return Ok(_mapper.ToDTO(result));
        }
        
        
    }
}