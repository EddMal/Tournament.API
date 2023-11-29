using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tournament.API.Data.Data;
using Tournament.API.Services;
using Tournament.Core.DTO.SeedDTO;
using static Tournament.Core.DTO.SeedDTO.SeedDTO;

namespace Tournament.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration seedConfig = new ConfigurationBuilder()
                        .SetBasePath(Environment.CurrentDirectory)
                        .AddJsonFile("seedData.json", optional: false, reloadOnChange: true)
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

            builder.Services.AddSingleton<IConfiguration>(seedConfig);

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
                
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
