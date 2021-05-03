using System.Collections.Generic;

namespace TournamentProj.Model
{
    public class DrawCreation
    {
        public int TournamentId { get; set; }

        public ICollection<int> playerIds { get; set; }
        
        public DrawType DrawType { get; set; }

        public string Name { get; set; }

        public int Games { get; set; }
        
        public int Points { get; set; }
        
        public bool TieBreaks { get; set; }
        
        //Holds prioritized list of players corresponding to their seedings
        //Doesn't have to contain all players - just seeded ones
        public List<int> playerIdsSeeded { get; set; }

        public int[] InitPoints()
        {
            var points = new int[Games];
            return points;
        }
    }
}