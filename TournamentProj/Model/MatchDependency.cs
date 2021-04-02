using System.ComponentModel.DataAnnotations;

namespace TournamentProj.Model
{
    public class MatchDependency
    {
        [Key]
        public int Id { get; set; }
        public int dependencyId { get; set; }
        
        /*
         * true: player will be e.g. nr. 3 in a round robin tournament with 4 people
         * false: player will come from a match - nr 1 for winner. Nr 2 for player
         */
        public bool dependsOnDraw { get; set; }
        
        public int position{ get; set; }
    }
}