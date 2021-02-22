using System.Collections.Generic;
using TournamentProj.DTO.Player;
using TournamentProj.Model;

public interface IPlayerMapper
{
    public Player FromDTO(PlayerDTO dto);
    public PlayerDTO ToDTO(Player player);

    public IEnumerable<PlayerDTO> ToDtoArray(IEnumerable<Player> players);
}