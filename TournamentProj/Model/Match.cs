using System.ComponentModel.DataAnnotations;

namespace TournamentProj.Model
{
    public class Match
    {
        [Key]
        public int matchId { get; set; }

        public int DrawId { get; set; }
        
        public Draw Draw { get; set; }
        
        [Required]
        public int p1Id { get; set; }
        
        [Required]
        public int p2Id { get; set; }
        
        public bool p1Won { get; set; }
        
        public int p1Match { get; set; }
        public int p1Games { get; set; }
        public int p1Sets { get; set; }
        public int p1Points { get; set; }
        public int p2Match { get; set; }
        public int p2Games { get; set; }
        public int p2Sets { get; set; }
        public int p2Points { get; set; }
    }
}