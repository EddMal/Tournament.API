using Tournament.Core.DTO.SeedDTO;
using static Tournament.Core.DTO.SeedDTO.SeedDTO;
namespace Tournament.API.Services
{
    public interface ISeedService
    {
        SeedData GetData();
    }
}
