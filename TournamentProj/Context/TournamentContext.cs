using Microsoft.EntityFrameworkCore;
using TournamentProj.Model;
namespace TournamentProj.Context

{
    public class TournamentContext : DbContext, ITournamentContext
    {
        
        public TournamentContext(DbContextOptions<TournamentContext> options)
            : base(options)
        {
        }
        public TournamentContext()
        {
        }
        public DbSet<Tournament> Tournaments { get; set; }
        
        public DbSet<Player> Players { get; set; }
        
        public DbSet<Draw> Draws { get; set; }

       


        //public DbSet<Match> Matches { get; set; }
        
    }
}