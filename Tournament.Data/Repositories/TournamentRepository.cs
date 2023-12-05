using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.API.Data.Data;
using Tournament.Core.DTO.TournamentDTO;
using Tournament.Core.Repositories;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentAPIContext _context;
        public TournamentRepository(TournamentAPIContext context)
        {
            _context = context;
        }
        //INSERT VERIFICATION AND RETURN BOOL IF SUCESS.
        public void Add(Core.Entities.Tournament tournament)
        {
            _context.Tournaments.Add(tournament);
            //await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await _context.Tournaments.AnyAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Core.Entities.Tournament>> GetAllAsync(bool includeGames = false)
        {
            return includeGames ? await _context.Tournaments.Include(t=>t.Games).ToListAsync()
                                : await _context.Tournaments.ToListAsync();

        }

        public async Task<Core.Entities.Tournament> GetAsync(Guid id)
        {
            Core.Entities.Tournament? tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.Id == id);
            //if (tournament == null)
            //{
            //    //MESSAGE??
            //    return new tournament;
            //}
            return tournament!;

        }

        public void Remove(Core.Entities.Tournament tournament)
        {
            _context.Tournaments.Remove(tournament);
        }

        public void Update(Core.Entities.Tournament tournament)
        {
            _context.Entry(tournament).State = EntityState.Modified;
        }
    }
}
