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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<MatchDependency>()
                .HasOne(md => md.Match)
                .WithOne(m => m.p1Dependency)
                //.HasForeignKey(a => a.ApplicationUserId)
                .IsRequired(false);
            
            modelBuilder.Entity<MatchDependency>()
                .HasOne(md => md.Match)
                .WithOne(m => m.p2Dependency)
                //.HasForeignKey(a => a.ApplicationUserId)
                .IsRequired(false);
            
            modelBuilder.Entity<Match>()
                .HasOne<MatchDependency>(m => m.p1Dependency)
                .WithOne(md => md.Match)
                .HasForeignKey<MatchDependency>(md => md.MatchId);
            
            modelBuilder.Entity<Match>()
                .HasOne<MatchDependency>(m => m.p2Dependency)
                .WithOne(md => md.Match)
                .HasForeignKey<MatchDependency>(md => md.MatchId);
            
            */
            
        }
        public TournamentContext()
        {
        }
        public DbSet<Tournament> Tournaments { get; set; }
        
        public DbSet<Player> Players { get; set; }
        
        public DbSet<Draw> Draws { get; set; }

        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchDependency> MatchDependencies { get; set; }

        
    }
}