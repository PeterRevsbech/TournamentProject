using System.ComponentModel.DataAnnotations;

namespace TournamentProj.DTO.Player
{
    public class PlayerDTO
    {
        public static int BYE_ID = -1;

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TournamentId { get; set; }
    }
}