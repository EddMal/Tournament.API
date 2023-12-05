using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.API.Data.Data;
using Tournament.API.Mappings;
using Tournament.Core.DTO.TournamentDTO;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using static Tournament.Core.DTO.SeedDTO.SeedDTO;

namespace Tournament.API.Controllers
{
    [Route("api/Tornaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IMappings _mappings;
        private readonly IUOW _UOW;

        public TournamentsController(IMappings mappings, IUOW OUW)
        {
            _mappings = mappings;
            _UOW = OUW;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Core.DTO.TournamentDTO.TournamentDTO>>> GetTournament()
        {

            var tournaments = await _UOW.TournamentRepository.GetAllAsync();

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
            Core.Entities.Tournament? tournament = await _UOW.TournamentRepository.GetAsync(id);
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

            var tournamentForModification = await _UOW.TournamentRepository.GetAsync(id);

            if (tournamentForModification == null) return NotFound();

            tournamentForModification = _mappings.TournamentDTOUpdateToTornament(tournamentForModification, tournamentDTOUpdate);

            //Alternative?
            _UOW.TournamentRepository.Update(tournamentForModification);

            await _UOW.CompleteAsync();

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
            _UOW.TournamentRepository.Add(tournament);
            await _UOW.CompleteAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        //PATCH
        [HttpPatch("{tournamentId}")]
        public async Task<ActionResult<TournamentDTOUpdate>> PatchTournament(Guid tournamentId, JsonPatchDocument<TournamentDTOUpdate> patchDocument) 
        {
            var tournamentPatch = await _UOW.TournamentRepository.GetAsync(tournamentId);

            if (tournamentPatch is null) return NotFound($"Tournament with id {tournamentId} was not found.");


            //if (company.Id != empToPatch.CompanyId) return BadRequest();
            
            var updatedTournament = _mappings.TournamentToTournamentDTOUpdate(tournamentPatch);

            patchDocument.ApplyTo(updatedTournament, ModelState);

            await TryUpdateModelAsync(updatedTournament);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            tournamentPatch = _mappings.TournamentDTOUpdateToTornament(tournamentPatch, updatedTournament);
            _UOW.TournamentRepository.Update(tournamentPatch);
            await _UOW.CompleteAsync();

            return NoContent();
        }

        //-----------------------------
        //--N-O-T -O-P-T-I-M-I-Z-E-D --
        //-----------------------------

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(Guid id)
        {
            var tournament = await _UOW.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _UOW.TournamentRepository.Remove(tournament);
            await _UOW.CompleteAsync();

            return NoContent();
        }
    }
}
