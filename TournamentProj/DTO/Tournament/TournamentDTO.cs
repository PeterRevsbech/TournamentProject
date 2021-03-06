using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentProj.DTO.Tournament
{
    public class TournamentDTO
    {
        [Key]
        public int Id {get;set;}
        public string Name {get;set;}
        
        public DateTime CreationDate { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public ICollection<int> DrawIds { get; set; }
        
        public ICollection<int> PlayerIds { get; set; }
    }
}