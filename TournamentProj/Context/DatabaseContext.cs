using Microsoft.EntityFrameworkCore;
using TournamentProj.Model;
namespace TournamentProj.Context

{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<Tournament> Tournaments { get; set; }
        
        public DbSet<Player> Players { get; set; }
        
        public DbSet<Draw> Draws { get; set; }
        
        //public DbSet<Match> Matches { get; set; }
        
    }
}