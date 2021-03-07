using System;
using System.Collections.Generic;

namespace TournamentProj.Model
{
    public class Tournament
    {
        public int Id {get;set;}
        public string Name {get;set;}
        
        public DateTime CreationDate { get; set; }
        
        public DateTime StartDate { get; set; }

        public List<Draw> Draws { get; set; }
        
        public List<Player> Players { get; set; }
        
    }
}