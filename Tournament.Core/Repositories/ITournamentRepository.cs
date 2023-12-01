﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.DTO.TournamentDTO;

namespace Tournament.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament.Core.Entities.Tournament>> GetAllAsync();
        Task<Tournament.Core.Entities.Tournament> GetAsync(Guid id);
        Task<bool> AnyAsync(Guid id);
        void Add(Tournament.Core.Entities.Tournament tournament);
        void Update(Tournament.Core.Entities.Tournament tournament);
        void Remove(Tournament.Core.Entities.Tournament tournament);
    }
}
