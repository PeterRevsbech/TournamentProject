
namespace TournamentProj.DTO.Match
{
    public enum StatusDTO
    {
        CLOSED,
        OPEN,
        ACTIVE,
        FINISHED
    }
    public class MatchDTO
    {
        public int Id { get; set; }
        
        public int DrawId { get; set; }
        public int P1Id { get; set; }
        public int P2Id { get; set; }
        
        public StatusDTO statusDTO { get; set; }
        public bool P1Won { get; set; }
        public int P1Match { get; set; }
        public int P1Sets { get; set; }
        public int P1Games { get; set; }
        public int P1Points { get; set; }
        public int P2Match { get; set; }
        public int P2Sets { get; set; }
        public int P2Games { get; set; }
        public int P2Points { get; set; }
    }
}