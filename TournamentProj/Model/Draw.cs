using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentProj.Model
{
    public enum DrawType
    {
        KO, MONRAD, RR
    }
    public class Draw
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        
        public Tournament Tournament { get; set; }
        
        public ICollection<Match> Matches { get; set; }
        public string Name { get; set; }
        
        public DrawType DrawType { get; set; }
        
        public int Sets { get; set; }
        
        public int Games { get; set; }
        
        public int Points { get; set; }
        
        public bool TieBreaks { get; set; }


    }
}