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

        public DbSet<Match> Matches { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Draw>()
                .HasOne(t => t.Tournament)
                .WithMany(d => d.Draws)
                .HasForeignKey("TournamentFK");

            modelBuilder.Entity<Player>()
                .HasOne(t => t.Tournament)
                .WithMany(p => p.Players)
                .HasForeignKey("TournamentFK");
            
            modelBuilder.Entity<Match>()
                .HasOne(d => d.Draw)
                .WithMany(m => m.Matches)
                .HasForeignKey("DrawFK");
        }
        
    }
}