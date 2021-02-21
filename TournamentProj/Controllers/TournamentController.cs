using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TournamentProj.Context;
using TournamentProj.Model;

namespace TournamentProj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TournamentController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDatabaseContext _dbContext;

        public TournamentController(ILogger<WeatherForecastController> logger, IDatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Tournament> Get()
        {
            return _dbContext.Tournaments.ToArray();
        }
        
        [HttpPost]
        public Tournament Post()
        {
            var t = new Tournament()
            {
                Name = "Peter"
            };
            _dbContext.Tournaments.Add(t);
            _dbContext.SaveChanges();
            return t;
        }
        
        [HttpPut]
        public Tournament Put()
        {
            var t = new Tournament()
            {
                Name = "Peter"
            };
            _dbContext.Tournaments.Add(t);
            _dbContext.SaveChanges();
            return t;
        }
        
        [HttpDelete]
        public Tournament Delete()
        {
            var t = new Tournament()
            {
                Name = "Peter"
            };
            _dbContext.Tournaments.Add(t);
            _dbContext.SaveChanges();
            return t;
        }
        
        
    }
}