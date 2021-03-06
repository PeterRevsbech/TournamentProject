using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TournamentProj.Context;
using TournamentProj.DTO.Match;
using TournamentProj.DTO.Player;
using TournamentProj.Mappers;
using TournamentProj.Model;
using TournamentProj.Services;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Match")]
    public class MatchController : ControllerBase
    {
        private readonly ILogger<MatchController> _logger;
        //private readonly ITournamentContext _dbContext;
        private readonly IMatchMapper _mapper;
        private readonly IMatchService _matchService;


        public MatchController(ILogger<MatchController> logger, ITournamentContext dbContext)
        {
            _logger = logger;
            //_dbContext = dbContext;
            _mapper = new MatchMapper();
            _matchService = new MatchService(dbContext);
        }

        [HttpGet]
        public IEnumerable<MatchDTO> Get()
        {
            return _mapper.ToDtoArray(_matchService.GetAll());
        }


        [Route("{id:int}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _matchService.Get(id);
            return Ok(_mapper.ToDTO(result));
        }
        
        [HttpPost]
        public IActionResult Post(JObject payload)
        {
            var dto = JsonConvert.DeserializeObject<MatchDTO>(payload.ToString());
            var input = _mapper.FromDTO(dto);

            var result =_matchService.Create(input);
            return Ok(_mapper.ToDTO(result));
        }
        
        
        [HttpPut]
        public IActionResult Put(JObject payload)
        {
            var dto = JsonConvert.DeserializeObject<MatchDTO>(payload.ToString());
            var input = _mapper.FromDTO(dto);

            var result = _matchService.Update(input);
            return Ok(_mapper.ToDTO(result));
        }
        
        
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _matchService.Delete(id);
            return Ok(_mapper.ToDTO(result));
        }
    }
}