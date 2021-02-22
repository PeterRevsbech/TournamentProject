using System.Collections.Generic;
using TournamentProj.DTO;
using TournamentProj.DTO.Player;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

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
        var dtos = new List<TournamentDTO>();
        foreach (var item in list)
        {
            dtos.Add(ToDTO(item));
        }

        return dtos;
    }
}