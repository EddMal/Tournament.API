using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Tournament.Core.Entities.Game>> GetAllAsync();
        Task<Tournament.Core.Entities.Game> GetAsync(Guid id);
        Task<bool> AnyAsync(Guid id);
        void Add(Tournament.Core.Entities.Game tournament);
        void Update(Tournament.Core.Entities.Game tournament);
        void Remove(Tournament.Core.Entities.Game tournament);
    }
}

