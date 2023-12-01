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

        public async Task<IEnumerable<TournamentDTO>> GetAllAsync()
        {
            var tournaments = await _context.Tournaments.ToListAsync();

            if (tournaments.Count == 0)
            {
                return new List<TournamentDTO>();
            }

            List<TournamentDTO> TournamentDTOs = new List<TournamentDTO>();

            foreach (var tournament in tournaments)
            {
                // change when made to a service :Tournament.API.Mappings.Mappings.TournamentToTournamentDTO(tournament);
                TournamentDTO tournamentDTO = new TournamentDTO()
                {
                    Title = tournament.Title,
                    StartDate = tournament.StartDate,
                };
                TournamentDTOs.Add(tournamentDTO);
            }

            return TournamentDTOs;
        }

        public async Task<TournamentDTO> GetAsync(Guid id)
        {
            Core.Entities.Tournament? tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.Id == id);
            if (tournament == null)
            {
                //MESSAGE??
                return new TournamentDTO();
            }


            //TournamentDTO tournamentDTO = Tournament.API.Mappings.Mappings.TournamentToTournamentDTO(tournament);
            //return Tournament.API.Mappings.Mappings.TournamentToTournamentDTO(tournament);
            return new TournamentDTO();

        }

        public void Remove(Core.Entities.Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public void Update(Core.Entities.Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }
}
