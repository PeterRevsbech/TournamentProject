namespace TournamentProj.Model
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DrawId { get; set; }
        public virtual Draw Draw { get; set; }
    }
}