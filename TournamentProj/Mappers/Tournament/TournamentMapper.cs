using System.Collections.Generic;
using TournamentProj.DTO;
using TournamentProj.DTO.Player;
using TournamentProj.DTO.Tournament;
using TournamentProj.Model;

namespace TournamentProj.Mappers
{
    public class TournamentMapper : ITournamentMapper
    {
        public Tournament FromDTO(TournamentDTO dto)
        {
            var tournament = new Tournament()
            {
                Id = dto.Id,
                Name = dto.Name,
                CreationDate = dto.CreationDate,
                StartDate = dto.StartDate

            };
            return tournament;
        }
    
        public TournamentDTO ToDTO(Tournament tournament)
        {
            var dto = new TournamentDTO()
            {
                Id = tournament.Id,
                Name = tournament.Name,
                CreationDate = tournament.CreationDate,
                StartDate = tournament.StartDate,
                PlayerIds = new List<int>(),
                DrawIds = new List<int>()
            };

            if (tournament.Players != null && tournament.Players.Count > 0)
            {
                foreach (var player in tournament.Players)
                {
                    dto.PlayerIds.Add(player.Id);
                }
            }

            if (tournament.Draws != null && tournament.Draws.Count > 0)
            {
                foreach (var draw in tournament.Draws)
                {
                    dto.DrawIds.Add(draw.Id);
                }
            }
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