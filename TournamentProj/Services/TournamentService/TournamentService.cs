﻿using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

namespace TournamentProj.Services.TournamentService
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ITournamentContext _dbContext;

        public TournamentService(ITournamentContext dbContext, ITournamentRepository tournamentRepository)
        {
            _dbContext = dbContext;
            _tournamentRepository = tournamentRepository;
        }
        
        public Tournament Create(Tournament tournament)
        {
            _tournamentRepository.Insert(tournament);
            _dbContext.SaveChanges();
            return tournament;
        }

        public Tournament Get(int id)
        {
            return _tournamentRepository.FindById(id);
        }

        public IEnumerable<Tournament> GetAll()
        {
            var result = _tournamentRepository.FindAll();
            return result;
        }

        public Tournament Delete(int id)
        {
            var tournament = _tournamentRepository.FindById(id);
            _tournamentRepository.Delete(tournament);
            _dbContext.SaveChanges();
            return tournament;
        }

        public Tournament Update(Tournament tournament)
        {
            _tournamentRepository.Update(tournament);
            _dbContext.SaveChanges();
            return tournament;
        }
    }
}