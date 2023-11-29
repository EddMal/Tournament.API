using NuGet.Packaging;
using System.Collections.Generic;
using Tournament.API.Controllers;
using Tournament.Core.DTO.SeedDTO;
using static Tournament.Core.DTO.SeedDTO.SeedDTO;

namespace Tournament.API.Services
{
    public class SeedService : ISeedService
    {
        private readonly IConfiguration _configuration;

        public SeedService(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public SeedData GetData()
        {
            var seedData = _configuration.GetSection("SeedData");

            return ((SeedData)seedData);

        }
    }
}
