using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TournamentProj.Context;
using TournamentProj.DTO.Draw;
using TournamentProj.DTO.Player;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Draw")]
    public class DrawController : ControllerBase
    {
        private readonly ITournamentContext _dbContext;
        private readonly ILogger<DrawController> _logger;
        private readonly IDrawMapper _mapper;

        public DrawController(ILogger<DrawController> logger, ITournamentContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = new DrawMapper();
        }

        [HttpGet]
        public IEnumerable<DrawDTO> Get()
        {
            return _mapper.ToDtoArray(_dbContext.Draws.ToArray());
        }


        [Route("{id:int}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _dbContext.Draws.Find(id);
            return Ok(_mapper.ToDTO(result));
        }

        
        [HttpPost]
        public IActionResult Post(JObject payload)
        {
            var dto = JsonConvert.DeserializeObject<DrawDTO>(payload.ToString());
            var result = _mapper.FromDTO(dto);
            _dbContext.Draws.Add(result);
            _dbContext.SaveChanges();

            return Ok(_mapper.ToDTO(result));
        }


        [HttpPut]
        public IActionResult Put(JObject payload)
        {
            var dto = JsonConvert.DeserializeObject<DrawDTO>(payload.ToString());
            var result = _mapper.FromDTO(dto);

            _dbContext.Draws.Update(result);
            _dbContext.SaveChanges();
            return Ok(result);
        }


        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _dbContext.Draws.Find(id);
            _dbContext.Draws.Remove(result);
            _dbContext.SaveChanges();
            return Ok(result);
        }
        
    }
}