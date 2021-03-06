﻿using System.Collections.Generic;
using TournamentProj.DTO;
using TournamentProj.DTO.Player;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

namespace TournamentProj.Mappers
{
    public class TournamentMapper : ITournamentMapper
    {
        public Tournament FromDTO(TournamentDTO tournamentDto)
        {
            Tournament tournament = new Tournament();
            tournament.Id = tournamentDto.Id;
            tournament.Name = tournamentDto.Name;
            return tournament;
        }
    
        public TournamentDTO ToDTO(Tournament tournament)
        {
            TournamentDTO dto = new TournamentDTO();
            dto.Id = tournament.Id;
            dto.Name = tournament.Name;
            return dto;
        }
        
        
        public IEnumerable<TournamentDTO> ToDtoArray(IEnumerable<Tournament> list)
        {
            foreach (var item in list)
            {
                yield return ToDTO(item);
            }
        }
    }
}