using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.API.Data.Data;
using Tournament.Core.DTO.TournamentDTO;
using Tournament.Core.Entities;
using static Tournament.Core.DTO.SeedDTO.SeedDTO;

namespace Tournament.API.Controllers
{
    [Route("api/Tornaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly TournamentAPIContext _context;

        public TournamentsController(TournamentAPIContext context)
        {
            _context = context;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Core.Entities.Tournament>>> GetTournament()
        {

            var tournaments  = await _context.Tournaments.ToListAsync();

            if (tournaments.Count == 0)
            { 
                return NotFound();
            }

            List<TournamentDTO> TournamentDTOs = new List<TournamentDTO>();

            foreach (var tournament in tournaments) 
            {
                TournamentDTO tournamentDTO = new TournamentDTO()
                {
                    Title = tournament.Title,
                    StartDate = tournament.StartDate,
                };
                TournamentDTOs.Add(tournamentDTO);
            }

            return Ok(TournamentDTOs);

        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Core.Entities.Tournament>> GetTournament(Guid id)
        {
            Core.Entities.Tournament? tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.Id == id);
            if (tournament == null)
            {
                //MESSAGE??
                return BadRequest($"The tournament with ID: {id} is not found.");
            }

            TournamentDTO tournamentDTO = Tournament.API.Mappings.Mappings.TournamentToTournamentDTO(tournament);

            return Ok(tournamentDTO);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(Guid id, TournamentDTOUpdate tournamentDTOUopdate)
        {
            if (id != tournamentDTOUopdate.Id)
            {
                return BadRequest();
            }

            var tournamentForModification = await _context.Tournaments.FirstOrDefaultAsync(t=> t.Id == id);

            if (tournamentForModification == null) return NotFound();

            tournamentForModification = Tournament.API.Mappings.Mappings.TournamentDTOUpdateToTornament(tournamentForModification, tournamentDTOUopdate);

            //Alternative?
            _context.Entry(tournamentForModification).State = EntityState.Modified;

            await _context.SaveChangesAsync();

             return NoContent();//$"Tournament {tournamentForModification.Title} Updated"
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Core.Entities.Tournament>> PostTournament(Core.DTO.TournamentDTO.TournamentDTO tournamentPost)
        {
            var tournament = Tournament.API.Mappings.Mappings.TournamentDTOToTournament(tournamentPost);
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }
        //-----------------------------
        //--N-O-T -O-P-T-I-M-I-Z-E-D --
        //-----------------------------

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(Guid id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TournamentExists(Guid id)
        {
            return _context.Tournaments.Any(e => e.Id == id);
        }
    }
}
