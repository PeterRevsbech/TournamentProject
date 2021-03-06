using System.ComponentModel.DataAnnotations;

namespace TournamentProj.DTO.Tournament
{
    public class TournamentDTO
    {
        [Key]
        public int Id {get;set;}
        
        public string Name {get;set;}
    }
}