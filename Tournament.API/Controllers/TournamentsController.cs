using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.API.Data.Data;
using Tournament.API.Mappings;
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
        private readonly IMappings _mappings;
        private readonly Tournament.Core.Repositories.ITournamentRepository _tournamentRepository;

        public TournamentsController(TournamentAPIContext context, IMappings mappings, Tournament.Core.Repositories.ITournamentRepository tournamentRepository)
        {
            _context = context;
            _mappings = mappings;
            _tournamentRepository = tournamentRepository;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Core.DTO.TournamentDTO.TournamentDTO>>> GetTournament()
        {

            var tournaments  = await _tournamentRepository.GetAllAsync();

            if (tournaments == null)
            { 
                return NotFound();
            }

            var tournamentDTOs = _mappings.TournamentsToTournamentDTOs(tournaments);

            return Ok(tournamentDTOs);

        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Core.DTO.TournamentDTO.TournamentDTO>> GetTournament(Guid id)
        {
            Core.Entities.Tournament? tournament = await _tournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                //MESSAGE??
                return BadRequest();// $"The tournament with ID: {id} is not found.");
            }

            TournamentDTO tournamentDTO = _mappings.TournamentToTournamentDTO(tournament);

            return Ok(tournamentDTO);
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(Guid id, TournamentDTOUpdate tournamentDTOUpdate)
        {
            if (id != tournamentDTOUpdate.Id)
            {
                return BadRequest();
            }

            var tournamentForModification = await _tournamentRepository.GetAsync(id);

            if (tournamentForModification == null) return NotFound();

            tournamentForModification = _mappings.TournamentDTOUpdateToTornament(tournamentForModification, tournamentDTOUpdate);

            //Alternative?
            _tournamentRepository.Update(tournamentForModification);

            await _context.SaveChangesAsync();

             return NoContent();//$"Tournament {tournamentForModification.Title} Updated"
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Core.DTO.TournamentDTO.TournamentDTO>> PostTournament(Core.DTO.TournamentDTO.TournamentDTO tournamentPost)
        {
            if (tournamentPost == null)
            { 
            return BadRequest();
            }

            var tournament = _mappings.TournamentDTOToTournament(tournamentPost);
            _tournamentRepository.Add(tournament);
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
            var tournament = await _tournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _tournamentRepository.Remove(tournament);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
