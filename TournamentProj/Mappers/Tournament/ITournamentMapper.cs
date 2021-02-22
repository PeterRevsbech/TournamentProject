using System.Collections.Generic;
using TournamentProj.DTO.Player;
using TournamentProj.Model;
using TournamentProj.DTO.Tournament;

public interface ITournamentMapper
{
    public Tournament FromDTO(TournamentDTO dto);
    public TournamentDTO ToDTO(Tournament tournament);
    
    public IEnumerable<TournamentDTO> ToDtoArray(IEnumerable<Tournament> tournaments);
}