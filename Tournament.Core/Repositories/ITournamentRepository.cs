using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Repositories
{
    internal interface ITournamentRepository
    {
        Task<IEnumerable<Tournament.Core.Entities.Tournament>> GetAllAsync();
        Task<Tournament.Core.Entities.Tournament> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Tournament.Core.Entities.Tournament tournament);
        void Update(Tournament.Core.Entities.Tournament tournament);
        void Remove(Tournament.Core.Entities.Tournament tournament);
    }
}
