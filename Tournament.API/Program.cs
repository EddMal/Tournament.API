using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;
using Tournament.API.Data.Data;
using Tournament.API.Extensions;
using Tournament.API.Mappings;
using Tournament.API.Services;
using Tournament.Core.DTO.SeedDTO;
using Tournament.Core.Repositories;
using static Tournament.Core.DTO.SeedDTO.SeedDTO;

namespace Tournament.API
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
            IConfiguration seedConfig = new ConfigurationBuilder()
                        .SetBasePath(Environment.CurrentDirectory)
                        .AddJsonFile("Properties/seedData.json", optional: false, reloadOnChange: true)
                        .Build();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TournamentAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentAPIContext") ?? throw new InvalidOperationException("Connection string 'TournamentAPIContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();

            builder.Services.AddSingleton<SeedData>(seedConfig.GetSection("SeedData").Get<SeedData>()!);
            builder.Services.Configure<SeedData>(seedConfig.GetSection("SeedData").Bind);

            builder.Services.AddScoped<IMappings,Mappings.Mappings>();
            builder.Services.AddScoped<Tournament.Core.Repositories.ITournamentRepository, Tournament.Data.Repositories.TournamentRepository>();
            builder.Services.AddScoped<Tournament.Core.Repositories.IGameRepository, Tournament.Data.Repositories.GameRepository>();
            builder.Services.AddScoped<IUOW, UOW>();

            builder.Services.AddScoped<ISeedService, SeedService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                await app.SeedStartupData();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
