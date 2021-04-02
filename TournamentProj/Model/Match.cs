using System.ComponentModel.DataAnnotations;

namespace TournamentProj.Model
{
    public enum Status
    {
        CLOSED,
        OPEN,
        ACTIVE,
        FINISHED
    }
    public class Match
    {

        [Key]
        public int Id { get; set; }

        public int DrawId { get; set; }
        
        public Draw Draw { get; set; }
        
        [Required]
        public int P1Id { get; set; }
        
        [Required]
        public int P2Id { get; set; }
        
        public Status Status { get; set; }
        
        public bool P1Won { get; set; }
        
        public int P1Match { get; set; }
        public int P1Games { get; set; }
        public int P1Sets { get; set; }
        
        public int P1Points { get; set; }
        public int P2Match { get; set; }
        public int P2Games { get; set; }
        public int P2Sets { get; set; }
        public int P2Points { get; set; }

        public MatchDependency p1Dependency { get; set; }
        
        public MatchDependency p2Dependency { get; set; }
        
    }
}