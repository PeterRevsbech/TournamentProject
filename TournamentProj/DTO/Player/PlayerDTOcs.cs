using System.ComponentModel.DataAnnotations;

namespace TournamentProj.DTO.Player
{
    public class PlayerDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int DrawId { get; set; }
    }
}