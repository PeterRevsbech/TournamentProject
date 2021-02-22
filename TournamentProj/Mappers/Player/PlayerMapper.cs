using System.Collections.Generic;
using TournamentProj.DTO.Player;
using TournamentProj.Model;

public class PlayerMapper : IPlayerMapper
{
    public Player FromDTO(PlayerDTO playerDto)
    {
        var player = new Player()
        {
            Name = playerDto.Name,
            Id = playerDto.Id
                
        };
       
        return player;
    }
    
    public PlayerDTO ToDTO(Player player)
    {
        var dto = new PlayerDTO()
        {
            Id = player.Id,
            Name = player.Name
        };
       
        return dto;
    }

    public IEnumerable<PlayerDTO> ToDtoArray(IEnumerable<Player> list)
    {
        var dtos = new List<PlayerDTO>();
        foreach (var item in list)
        {
            dtos.Add(ToDTO(item));
        }

        return dtos;
    }
        
        
}