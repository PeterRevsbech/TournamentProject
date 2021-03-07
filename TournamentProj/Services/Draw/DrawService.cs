using System.Collections.Generic;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.Model;
using TournamentProj.Services.DrawCreationLogic;

namespace TournamentProj.Services
{
    public class DrawService : IDrawService
    {
        private readonly IDrawRepository _drawRepository;
        private readonly ITournamentContext _dbContext;

        public DrawService(ITournamentContext dbContext, IDrawRepository drawRepository)
        {
            _dbContext = dbContext;
            _drawRepository = drawRepository;
        }

        
        public Draw Create(Draw draw)
        {
            _drawRepository.Insert(draw);
            _dbContext.SaveChanges();
            return draw;
        }

        public Draw CreateAutomatic(DrawCreation drawCreation)
        {
            var generatedDraw = DrawCreator.GenerateDraw(drawCreation);
            return Create(generatedDraw);
        }

        public Draw Get(int id)
        {
            return _drawRepository.FindById(id);
        }

        public IEnumerable<Draw> GetAll()
        {
            return _drawRepository.FindAll();
        }

        public Draw Delete(int id)
        {
            var draw = _drawRepository.FindById(id);
            _drawRepository.Delete(draw);
            _dbContext.SaveChanges();
            return draw;
        }

        public Draw Update(Draw draw)
        {
            _drawRepository.Update(draw);
            _dbContext.SaveChanges();
            return draw;
        }
        
    }
}