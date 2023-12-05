using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.API.Data.Data;
using Tournament.API.Mappings;
using Tournament.Core.DTO.GameDTO;
using Tournament.Core.DTO.TournamentDTO;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Repositories;

namespace Tournament.API.Controllers
{
    [Route("api/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMappings _mappings;
        private readonly IUOW _UOW;
        public GamesController(IMappings mappings, IUOW UOW)
        {
            _mappings = mappings;
            _UOW = UOW;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Core.DTO.GameDTO.GameDTO>>> GetGame()
        {

            var game = await _UOW.GameRepository.GetAllAsync();

            if (game == null)
            {
                return NotFound();
            }

            var gameDTOs = _mappings.GamesToGameDTOs(game);

            return Ok(gameDTOs);
        }

        // GET: api/Games/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Core.DTO.GameDTO.GameDTO>> GetGame(Guid id)
        //{
        //    Core.Entities.Game? game = await _UOW.GameRepository.GetAsync(id);
        //    if (game == null)
        //    {
        //        return BadRequest();//$"The tournament with ID: {id} is not found.");
        //    }

        //    GameDTO gameDTO = _mappings.GameToGameDTO(game);

        //    return Ok(gameDTO);
        //}

        // GET: api/Games/5
        [HttpGet("{title}")]
        public async Task<ActionResult<Core.DTO.GameDTO.GameDTO>> GetGame(string title)
        {
            Core.Entities.Game? game = await _UOW.GameRepository.GetAsync(title);

            if (game == null)
            {
                return BadRequest();//$"The tournament with ID: {id} is not found.");
            }

            GameDTO gameDTO = _mappings.GameToGameDTO(game);

            return Ok(gameDTO);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(Guid id, GameDTOUpdate gameDTOUpdate)
        {
           //Optmizations? (PutGame)  
            if (id != gameDTOUpdate.Id)
            {
                return BadRequest();
            }
            //----------------------------------------
            //--C-O-N-T-R-O-L- -M-O-D-E-L-S-T-A-T-E --
            //----------------------------------------

            var gameForModification = await _UOW.GameRepository.GetAsync(id);

            if (gameForModification == null) return NotFound();

            gameForModification = _mappings.GameDTOUpdateToGame(gameForModification, gameDTOUpdate);

            //Alternative?
            _UOW.GameRepository.Update(gameForModification);

            await _UOW.CompleteAsync();

            return NoContent();//$"Game {gameForModification.Title} Updated"
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Core.DTO.GameDTO.GameDTO>> PostGame(Core.DTO.GameDTO.GameDTO gamePost)
        {
            //------------------------------------------------------------------
            //--A-D-D -L-O-G-I-C- -H-A-N-D-L-I-N-G- -T-O-U-R-N-a_M-E-N-T-I-D- --
            //------------------------------------------------------------------
            //Description:
            //Empty TournamentID should be ok.
            //:Additional changes:
            //Tournament should display games and game should display tournament name.
            //--E-N-D-----------------------------------------------------------
            if (gamePost == null)
            {
                return BadRequest();
            }

            var game = _mappings.GameDTOToGame(gamePost);
            _UOW.GameRepository.Add(game);
            await _UOW.CompleteAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        //PATCH
        [HttpPatch("{gameId}")]
        public async Task<ActionResult<GameDTOUpdate>> PatchGame(Guid gameId, JsonPatchDocument<GameDTOUpdate> patchDocument)
        {
            var GamePatch = await _UOW.GameRepository.GetAsync(gameId);

            if (GamePatch is null) return NotFound($"Game with id {gameId} was not found.");


            //if (company.Id != empToPatch.CompanyId) return BadRequest();

            var updatedGame = _mappings.GameToGameDTOUpdate(GamePatch);

            patchDocument.ApplyTo(updatedGame, ModelState);

            await TryUpdateModelAsync(updatedGame);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            GamePatch = _mappings.GameDTOUpdateToGame(GamePatch, updatedGame);
            _UOW.GameRepository.Update(GamePatch);
            await _UOW.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var game = await _UOW.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _UOW.GameRepository.Remove(game);
            await _UOW.CompleteAsync();

            return NoContent();
        }

    }
}
