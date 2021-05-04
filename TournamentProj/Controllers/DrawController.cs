using Microsoft.AspNetCore.Mvc;
using TournamentProj.DTO.Draw;
using TournamentProj.Mappers;
using TournamentProj.Services.DrawService;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("api/Draw")]
    public class DrawController : ControllerBase
    {
        private readonly IDrawMapper _mapper;
        private readonly IDrawService _drawService;

        public DrawController(IDrawService drawService, IDrawMapper drawMapper)
        {
            _mapper = drawMapper;
            _drawService = drawService;
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
        public IActionResult Post(DrawDTO dto)
        {
            var input = _mapper.FromDTO(dto);

            var result =_drawService.Create(input);
            return Ok(_mapper.ToDTO(result));
        }
            
        [Route("Generate/")]
        [HttpPost]
        //Automatically sets a draw up according to specifications is DrawCreationDTO.
        public IActionResult PostAutomaticCreation(DrawCreationDTO dto)
        {
            var drawCreation = _mapper.FromCreationDTO(dto);
            var result =_drawService.CreateAutomatic(drawCreation);
            return Ok(_mapper.ToDTO(result));
        }

        [HttpPut]
        public IActionResult Put(DrawDTO dto)
        {
            var input = _mapper.FromDTO(dto);

            var result = _drawService.Update(input);
            return Ok(result);
        }


        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _drawService.Delete(id);   
            return Ok(_mapper.ToDTO(result));
        }
        
    }
}