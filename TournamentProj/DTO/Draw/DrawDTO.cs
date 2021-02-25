namespace TournamentProj.DTO.Draw
{
    public class DrawDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int games { get; set; }
        public int sets { get; set; }
        public int points { get; set; }
        public bool useSets { get; set; }
        public bool tieBreaks { get; set; }
    }
}