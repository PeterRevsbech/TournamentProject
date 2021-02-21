using Microsoft.EntityFrameworkCore;
using TournamentProj.Model;

namespace TournamentProj.Context
{
    public interface IDatabaseContext 
    {
        DbSet<Tournament> Tournaments { get; set; }
        
        int SaveChanges();
    }
}