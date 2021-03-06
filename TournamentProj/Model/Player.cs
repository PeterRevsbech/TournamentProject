using System.ComponentModel.DataAnnotations;

namespace TournamentProj.Model
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public int DrawId { get; set; }
        
        public virtual Draw Draw { get; set; }
    }
}