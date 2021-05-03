using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentProj.DTO.Draw
{
    public enum DrawTypeDTO
    {
        KO, MONRAD, RR
    }
    public class DrawDTO
    {
        [Key]
        public int Id { get; set; }
        
        public int TournamentId { get; set; }
        
        public ICollection<int> MatchIds { get; set; }
        public string Name { get; set; }
        
        public DrawTypeDTO DrawTypeDTO { get; set; }
        
        public int Games { get; set; }
        
        public int Points { get; set; }
        
        public bool TieBreaks { get; set; }


    }
}