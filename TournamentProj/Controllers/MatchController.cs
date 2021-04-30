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
using TournamentProj.Services.MatchService;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Match")]
    public class MatchController : ControllerBase
    {
        private readonly ILogger<MatchController> _logger;
        private readonly IMatchMapper _mapper;
        private readonly IMatchService _matchService;

        public MatchController(ILogger<MatchController> logger, IMatchMapper matchMapper, IMatchService matchService)
        {
            _logger = logger;
            _mapper = matchMapper;
            _matchService = matchService;
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
        
        [Route("Player/{playerId:int}")]
        [HttpGet]
        public IActionResult GetByPlayerId(int playerId)
        {
            var result = _matchService.GetByPlayerId(playerId);
            return Ok(_mapper.ToDtoArray(result));
        }
        
        
        [Route("Draw/{drawId:int}")]
        [HttpGet]
        public IActionResult GetByDrawId(int drawId)
        {
            var result = _matchService.GetByDrawId(drawId);
            return Ok(_mapper.ToDtoArray(result));
        }
        
        
        [HttpPost]
        public IActionResult Post(MatchDTO dto)
        {
            var input = _mapper.FromDTO(dto);

            var result =_matchService.Create(input);
            return Ok(_mapper.ToDTO(result));
        }
        
        
        [HttpPut]
        public IActionResult Put(MatchDTO dto)
        {
            var input = _mapper.FromDTO(dto);

            var result = _matchService.Update(input);
            return Ok(_mapper.ToDTO(result));
        }
        
        
        [Route("Report")]
        [HttpPut]
        public IActionResult Put(MatchReportDTO matchReportDTO)
        {
            var result = _matchService.UpdateScoreReport(matchReportDTO);
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