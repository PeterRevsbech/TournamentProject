using System.Collections.Generic;

namespace TournamentProj.Model
{
    public enum DrawType
    {
        KnockOut, Monrad, RoundRobin
    }
    public class Draw
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DrawType DrawType { get; set; }


        public int TournamentId { get; set; }
  
        public virtual Tournament Tournament { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}