using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentProj.Model
{
    public class Player
    {
        public static int BYE_ID = -1;

        [Key]
        public int Id { get; set; }
        
        public Tournament Tournament { get; set; }
        
        public int TournamentId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
    }
}