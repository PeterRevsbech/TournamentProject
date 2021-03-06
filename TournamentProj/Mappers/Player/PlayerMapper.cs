using System.Collections.Generic;
using TournamentProj.DTO.Player;
using TournamentProj.Model;

namespace TournamentProj.Mappers
{
    public class PlayerMapper : IPlayerMapper
    {
        public Player FromDTO(PlayerDTO dto)
        {
            var player = new Player()
            {
                Name = dto.Name,
                Id = dto.Id
                
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
            foreach (var item in list)
            {
                yield return ToDTO(item);
            }
        }
    }
}