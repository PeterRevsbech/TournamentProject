using TournamentProj.Model;
using TournamentProj.DTO;

public interface ITournamentMapper
{
    public Tournament FromDTO(TournamentDTO dto);
    public TournamentDTO ToDTO(Tournament tournament);
}