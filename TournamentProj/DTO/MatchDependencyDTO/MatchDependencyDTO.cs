namespace TournamentProj.DTO.MatchDependencyDTO
{
    public class MatchDependencyDTO
    {
        public int Id { get; set; }
        
        public int MatchId { get; set; }

        public int DependencyId { get; set; }
        
        /*
         * true: player will be e.g. nr. 3 in a round robin tournament with 4 people
         * false: player will come from a match - nr 1 for winner. Nr 2 for player
         */
        public bool DependsOnDraw { get; set; }
        
        public int Position{ get; set; }
    }
}