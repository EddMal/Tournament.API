using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tournament.API.Data.Data
{
    public class TournamentAPIContext : DbContext
    {
        //Sets tracking to off as default.
        public TournamentAPIContext(DbContextOptions<TournamentAPIContext> options)
           : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //Set entities names for use of db/_context in backend.
        public DbSet<Core.Entities.Tournament> Tournaments => Set<Core.Entities.Tournament>();
        public DbSet<Game> Games => Set<Game>();

        //Override tables name.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Core.Entities.Tournament>().ToTable($"{nameof(Core.Entities.Tournament)}s");

            modelBuilder.Entity<Game>().ToTable($"{nameof(Game)}s");
        }



        public DbSet<Tournament.Core.Entities.Tournament> Tournament { get; set; } = default!;
        public DbSet<Tournament.Core.Entities.Game> Game { get; set; } = default!;
    }
}
