﻿using System;
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
using TournamentProj.Mappers;
using TournamentProj.Model;
using TournamentProj.Services;
using TournamentProj.Services.PlayerService;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Player")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerMapper _mapper;
        private readonly IPlayerService _playerService;


        public PlayerController(IPlayerMapper playerMapper, IPlayerService playerService)
        {
            _mapper = playerMapper;
            _playerService = playerService;
        }

        [HttpGet]
        public IEnumerable<PlayerDTO> Get()
        {
            return _mapper.ToDtoArray(_playerService.GetAll());
        }
        
        
        
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _playerService.Get(id);
            return Ok(_mapper.ToDTO(result));
        }
        
        [Route("Draw/{drawId:int}")]
        [HttpGet]
        public IEnumerable<PlayerDTO> GetFromDrawId(int drawId)
        {
            var result = _playerService.GetFromDrawId(drawId);
            return _mapper.ToDtoArray(result);
        }
        
        
        [HttpPost]
        public IActionResult Post(PlayerDTO dto)
        {
            var input = _mapper.FromDTO(dto);

            var result =_playerService.Create(input);
            return Ok(_mapper.ToDTO(result));
        }
        
        
        [HttpPut]
        public IActionResult Put(PlayerDTO dto)
        {
            var input = _mapper.FromDTO(dto);

            var result = _playerService.Update(input);
            return Ok(_mapper.ToDTO(result));
        }
        
        
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _playerService.Delete(id);
            return Ok(_mapper.ToDTO(result));
        }
        
        
    }
    
}