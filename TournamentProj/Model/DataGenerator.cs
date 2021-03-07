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
            // Look for any board games.
            if (context.Tournaments.Any())
            {
                return;   // Data was already seeded
            }

            context.Tournaments.AddRange(
                new Tournament
                {
                    Id = 1,
                    Name = "Squash Grand Prix 1",
                    CreationDate = DateTime.Now,
                    StartDate = DateTime.Today,
                    Draws = new List<Draw>()
                    {
                        new ()
                        {
                            Id = 10,
                            TournamentId = 1,
                            Name = "DU13 Finalerunde",
                            DrawType = DrawType.RR,
                            Matches = new List<Match>()
                            {
                                new Match()
                                {
                                    Id = 50,
                                    P1Id = 15,
                                    P2Id = 16,
                                    Status = Status.CLOSED
                                }
                                ,
                                new Match()
                                {
                                    Id = 51,
                                    P1Id = 15,
                                    P2Id = 17,
                                    Status = Status.CLOSED
                                }
                                ,
                                new Match()
                                {
                                    Id = 52,
                                    P1Id = 16,
                                    P2Id = 17,
                                    Status = Status.CLOSED
                                }
                            }
                        }
                        ,
                        new ()
                        {
                            Id = 11,
                            TournamentId = 1,
                            Name = "DU13 Puljespl",
                            DrawType = DrawType.RR,
                            Matches = new List<Match>()
                        }
                    },
                    Players = new List<Player>()
                    {
                        new Player()
                        {
                            Id = 15,
                            Name = "Peter Revsbech",
                        }
                        ,
                        new Player()
                        {
                            Id = 16,
                            Name = "Simon Steenholdt",
                        }
                        ,
                        new Player()
                        {
                            Id = 17,
                            Name = "Sebastian Brinker",
                        }
                    }
                },
                new Tournament
                {
                    Id = 2,
                    Name = "Squash Grand Prix 2",
                    CreationDate = DateTime.Now,
                    StartDate = DateTime.Today
                },  
                new Tournament
                {
                    Id = 3,
                    Name = "Pool med vennerne",
                    CreationDate = DateTime.Now,
                    StartDate = DateTime.Today
                });
            
            context.Draws.AddRange(
                new Draw()
                {
                    Id = 1,
                    Name = "DU17 Hovedturnering",
                    TournamentId = 1,
                    Matches = new List<Match>()
                },
                new Draw()
                {
                    Id = 2,
                    Name = "DU15 Hovedturnering",
                    TournamentId = 1,
                    Matches = new List<Match>()

                }
                ,
                new Draw()
                {
                    Id = 3,
                    Name = "DU13 Hovedturnering",
                    TournamentId = 1,
                    Matches = new List<Match>()
                }
                ,
                new Draw()
                {
                    Id = 4,
                    Name = "PU17 Puljespil",
                    TournamentId = 2,
                    Matches = new List<Match>()
                }
                ,
                new Draw()
                {
                    Id = 5,
                    Name = "PU17 Hovedturnering",
                    TournamentId = 2,
                    Matches = new List<Match>()
                }
                ,
                new Draw()
                {
                    Id = 6,
                    Name = "Torsdag aften på Bobbys",
                    TournamentId = 3,
                    Matches = new List<Match>()
                }
            );
/*
            context.Players.AddRange(
                new Player()
                {
                    
                }
                ,
                new Player()
                {
                    
                }
                ,
                new Player()
                {
                    
                }
                ,
                new Player()
                {
                    
                }
            );
*/
            
            
            context.SaveChanges();
        }
    }
    }
}