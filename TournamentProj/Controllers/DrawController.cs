using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TournamentProj.Context;
using TournamentProj.DTO.Draw;
using TournamentProj.DTO.Player;
using TournamentProj.Mappers;
using TournamentProj.Services;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Draw")]
    public class DrawController : ControllerBase
    {
        //private readonly ITournamentContext _dbContext;
        private readonly ILogger<DrawController> _logger;
        private readonly IDrawMapper _mapper;
        private readonly IDrawService _drawService;

        public DrawController(ILogger<DrawController> logger, ITournamentContext dbContext)
        {
            _logger = logger;
            //_dbContext = dbContext;
            _mapper = new DrawMapper();
                
            //TODO do this with dependency injection instead
            //dbContext is here from DI pattern. Use it to make nessecary service(s)
            _drawService = new DrawService(dbContext);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.ToDtoArray(_drawService.GetAll()));
        }


        [Route("{id:int}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _drawService.Get(id);
            return Ok(_mapper.ToDTO(result));
        }

        
        [HttpPost]
        public IActionResult Post(JObject payload)
        {
            var dto = JsonConvert.DeserializeObject<DrawDTO>(payload.ToString());
            var input = _mapper.FromDTO(dto);

            var result =_drawService.Create(input);
            return Ok(_mapper.ToDTO(result));
        }


        [HttpPut]
        public IActionResult Put(JObject payload)
        {
            var dto = JsonConvert.DeserializeObject<DrawDTO>(payload.ToString());
            var input = _mapper.FromDTO(dto);

            var result = _drawService.Update(input);
            return Ok(result);
        }


        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _drawService.Delete(id);
            return Ok(result);
        }
        
    }
}