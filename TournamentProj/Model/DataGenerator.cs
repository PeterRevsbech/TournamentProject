using System;
using System.Collections.Generic;
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
        using (var context = new TournamentContext(
            serviceProvider.GetRequiredService<DbContextOptions<TournamentContext>>()))
        {
            // Look for any tournaments
            if (context.Tournaments.Any())
            {
                return;// Data was already seeded
            }

            context.Tournaments.AddRange(
                new Tournament
                {
                    Id = 1,
                    Name = "Squash Grand Prix 1",
                    CreationDate = DateTime.Now,
                    StartDate = DateTime.Today,
                    Draws = new List<Draw>(),
                    Players = new List<Player>()
                    {
                        new ()
                        {
                            Id = 15,
                            Name = "Peter Revsbech",
                        }
                        ,
                        new ()
                        {
                            Id = 16,
                            Name = "Simon Steenholdt",
                        }
                        ,
                        new ()
                        {
                            Id = 17,
                            Name = "Sebastian Brinker",
                        }
                    }
                }
            );
            context.SaveChanges();
        }
    }
    }
}