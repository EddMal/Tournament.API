using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;
using Tournament.API.Controllers;
using Tournament.API.Data.Data;
using Tournament.Core.DTO.GameDTO;
using Tournament.Core.DTO.SeedDTO;
using Tournament.Core.DTO.TournamentDTO;
using Tournament.Core.Entities;
using static Tournament.Core.DTO.SeedDTO.SeedDTO;
using Game = Tournament.Core.Entities.Game;

namespace Tournament.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        

        public static async Task SeedStartupData(this IApplicationBuilder app)
        {
            using (var scpoe = app.ApplicationServices.CreateScope())
            { 
             var serviceProvider = scpoe.ServiceProvider;
             var context = serviceProvider.GetRequiredService<TournamentAPIContext>();
             
                var seedData = serviceProvider.GetRequiredService<SeedData>();
                try
                {
                     await SeedData(seedData, context);
                }
                catch (Exception e) 
                {
                    throw;
                }
            }
        }

        //----------------------------
        //--M-O-V-E- -T-O- -F-I-L-E --
        //----------------------------

        public static async Task SeedData(SeedData seedData, TournamentAPIContext context)
        {
            // Investigate better soulution.
            TournamentAPIContext Tcontext = null!;

            // Assign context if context is not null.
            Tcontext = context ?? throw new ArgumentNullException(nameof(context));

            // If tournaments allready exist return to main.
            if (await context.Tournaments.AnyAsync()) return;

            await AddTournamentsToContext(seedData, Tcontext);
            await context.SaveChangesAsync();
        }

        private static async Task AddTournamentsToContext (SeedData seedData, TournamentAPIContext context)
        {

            for (var i = 0; i < seedData.Tournaments.Length; i++)
            {
                var tournamentToContext = new Tournament.Core.Entities.Tournament()
                {
                    StartDate = DateTime.Now,
                    Title = seedData.Tournaments[i].Name,
                };
                //Alternative: Create list and AddRangeAsync() after the loop. 
                //await context.AddAsync(tournamentToContext);
                await context.SaveChangesAsync();

                var gameToContext = await AddGameToContext(seedData, context, i, tournamentToContext);

                //tournamentToContext.Games.Add(gameToContext); 

                //context.Update(tournamentToContext);

                await context.SaveChangesAsync();
               
            }
        }

        private static async Task<Game> AddGameToContext(SeedData seedData, TournamentAPIContext context,int i, Tournament.Core.Entities.Tournament tournament)
        {

                var gameToContext = new Tournament.Core.Entities.Game()
            {
                Time = DateTime.Now,
                Title = seedData.Games[i].Name,
                TournamentId = tournament.Id,
                Tournament = tournament
                };
            
            //gameToContext.TournamentId = await context.FindAsync(g.ID.Where(g => g.Title == seedData.Tournaments[i].Name));
            await context.AddAsync(gameToContext);

            return gameToContext;

        }
    }

}
