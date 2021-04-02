using System.ComponentModel.DataAnnotations;

namespace TournamentProj.Model
{
    public class MatchDependency
    {
        [Key]
        public int Id { get; set; }
        
        /*
         * Id of the match, the dependency belongs to
         
        public int MatchId { get; set; }
        
        public Match Match { get; set; }
        */
        
        /*
         * Id of the match or draw, that the dependency points to. E.g. "player will be winner of match with ID x"
         */ public int DependencyId { get; set; }
        
        /*
         * true: player will be e.g. nr. 3 in a round robin tournament with 4 people
         * false: player will come from a match - nr 1 for winner. Nr 2 for player
         */
        public bool DependsOnDraw { get; set; }
        
        public int Position{ get; set; }
    }
}