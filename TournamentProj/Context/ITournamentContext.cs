using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentProj.Model;

namespace TournamentProj.Context
{
    public interface ITournamentContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        public DbSet<Tournament> Tournaments { get; set; }
        
        public DbSet<Player> Players { get; set; }
        
        public DbSet<Draw> Draws { get; set; }

        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchDependency> MatchDependencies { get; set; }
        int SaveChanges();
    }
}