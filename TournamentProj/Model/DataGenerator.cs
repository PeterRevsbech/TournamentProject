using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TournamentProj.Context;

namespace TournamentProj.Model
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DatabaseContext(
            serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>()))
        {
            // Look for any board games.
            if (context.Tournaments.Any())
            {
                return;   // Data was already seeded
            }

            context.Tournaments.AddRange(
                new Tournament
                {
                    Id = 1,
                    Name = "Squash Grand Prix 1"
                },
                new Tournament
                {
                    Id = 2,
                    Name = "Squash Grand Prix 2"
                },  
                new Tournament
                {
                    Id = 3,
                    Name = "Pool med vennerne"
                });
            
            context.Draws.AddRange(
                new Draw()
                {
                    Id = 1,
                    Name = "DU17 Hovedturnering",
                    TournamentId = 1
                },
                new Draw()
                {
                    Id = 2,
                    Name = "DU15 Hovedturnering",
                    TournamentId = 1
                }
                ,
                new Draw()
                {
                    Id = 3,
                    Name = "DU13 Hovedturnering",
                    TournamentId = 1
                }
                ,
                new Draw()
                {
                    Id = 4,
                    Name = "PU17 Puljespil",
                    TournamentId = 2
                }
                ,
                new Draw()
                {
                    Id = 5,
                    Name = "PU17 Hovedturnering",
                    TournamentId = 2
                }
                ,
                new Draw()
                {
                    Id = 6,
                    Name = "Torsdag aften på Bobbys",
                    TournamentId = 3
                    
                }
            );

            
            
            context.SaveChanges();
        }
    }
    }
}