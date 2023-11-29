using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Tournament.Core.DTO.GameDTO;
using Tournament.Core.DTO.SeedDTO;
using Tournament.Core.DTO.TournamentDTO;
using Tournament.Core.Entities;
using static Tournament.Core.DTO.SeedDTO.SeedDTO;

namespace Tournament.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async void SeedDataAsync(SeedData seedData, DbContext _context)
        {           
            AddTournamentsToContext(seedData, _context);
            await _context.SaveChangesAsync();
        }

        private static async void AddTournamentsToContext (SeedData seedData, DbContext _context)
        {

            for (var i = 0; i < seedData.Tournaments.Length-1; i++)
            {
                var tournamentToContext = new TournamentDTO()
                {
                    StartDate = DateTime.Now,
                    Title = seedData.Tournaments[i].Name,
                };

                await _context.AddAsync(tournamentToContext);
                await _context.SaveChangesAsync();

                AddGameToContext(seedData, _context, i);

            }
        }

        private static async void AddGameToContext(SeedData seedData, DbContext _context,int i)
        {

                var gameToContext = new GameDTO()
            {
                Time = DateTime.Now,
                Title = seedData.Games[i].Name,
                //TournamentId = seedData.Games[i].Name
            };
            var v = await _context.
            gameToContext.TournamentId = await _context.FindAsync(g.ID.Where(g => g.Title == seedData.Tournaments[i].Name));
            await _context.AddAsync(seedData.Tournaments[i].Name);

        }
    }

}
