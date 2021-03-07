﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TournamentProj.Context;
using TournamentProj.DTO.Tournament;
using TournamentProj.Mappers;
using TournamentProj.Model;
using TournamentProj.Services.tournament;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Tournament")]
    public class TournamentController : ControllerBase
    {

        private readonly ILogger<TournamentController> _logger;
        private readonly ITournamentMapper _mapper;
        private readonly ITournamentService _tournamentService;

        public TournamentController(ILogger<TournamentController> logger,ITournamentMapper tournamentMapper, ITournamentService tournamentService)
        {
            _logger = logger;
            _mapper = tournamentMapper;
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_mapper.ToDtoArray(_tournamentService.GetAll()));
        }
        
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _tournamentService.Get(id);
            return Ok(_mapper.ToDTO(result));
        }
        
        [HttpPost]
        public IActionResult Post(JObject payload)
        {
            var tournamentDto = JsonConvert.DeserializeObject<TournamentDTO>(payload.ToString());
            var input = _mapper.FromDTO(tournamentDto);

            var result = _tournamentService.Create(input);

            return Ok(_mapper.ToDTO(result));
        }
        
        [HttpPut]
        public IActionResult Put(JObject payload)
        {
            var tournamentDto = JsonConvert.DeserializeObject<TournamentDTO>(payload.ToString());
            var input = _mapper.FromDTO(tournamentDto);

            var result =_tournamentService.Update(input);
            return Ok(_mapper.ToDTO(result));
        }
        
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _tournamentService.Delete(id); 
            return Ok(_mapper.ToDTO(result));
        }
        
        
    }
}