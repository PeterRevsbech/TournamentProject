﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TournamentProj.DTO.MatchDependencyDTO;
using TournamentProj.Mappers.MatchDependencyMapper;
using TournamentProj.Services.MatchDependencyService;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/MatchDependency")]
    public class MatchDependencyController : ControllerBase
    {
        private readonly ILogger<MatchDependencyController> _logger;
        private readonly IMatchDependencyMapper _mapper;
        private readonly IMatchDependencyService _matchDependencyService;

        public MatchDependencyController(ILogger<MatchDependencyController> logger, IMatchDependencyMapper matchMapper, IMatchDependencyService matchDependencyService)
        {
            _logger = logger;
            _mapper = matchMapper;
            _matchDependencyService = matchDependencyService;
        }

        [HttpGet]
        public IEnumerable<MatchDependencyDTO> Get()
        {
            return _mapper.ToDtoArray(_matchDependencyService.GetAll());
        }
        
        [HttpGet]
        public IEnumerable<MatchDependencyDTO> GetByMatchId(int matchId)
        {
            return _mapper.ToDtoArray(_matchDependencyService.GetByMatchId(matchId));
        }


        [Route("{id:int}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _matchDependencyService.Get(id);
            return Ok(_mapper.ToDTO(result));
        }

        [HttpPost]
        public IActionResult Post(MatchDependencyDTO dto)
        {
            var input = _mapper.FromDTO(dto);

            var result =_matchDependencyService.Create(input);
            return Ok(_mapper.ToDTO(result));
        }
        
        
        [HttpPut]
        public IActionResult Put(MatchDependencyDTO dto)
        {
            var input = _mapper.FromDTO(dto);

            var result = _matchDependencyService.Update(input);
            return Ok(_mapper.ToDTO(result));
        }
        
        
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _matchDependencyService.Delete(id);
            return Ok(_mapper.ToDTO(result));
        }
    }
}