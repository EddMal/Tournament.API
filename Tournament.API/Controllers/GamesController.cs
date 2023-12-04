using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private readonly TournamentAPIContext _context;
        private readonly IMappings _mappings;
        private readonly Tournament.Core.Repositories.IGameRepository _gameRepository;

        public GamesController(TournamentAPIContext context, IMappings mappings, Tournament.Core.Repositories.IGameRepository gameRepository)
        {
            _context = context;
            _mappings = mappings;
            _gameRepository = gameRepository;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Core.DTO.GameDTO.GameDTO>>> GetGame()
        {

            var game = await _gameRepository.GetAllAsync();

            if (game == null)
            {
                return NotFound();
            }

            var gameDTOs = _mappings.GamesToGameDTOs(game);

            return Ok(gameDTOs);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Core.DTO.GameDTO.GameDTO>> GetGame(Guid id)
        {
            Core.Entities.Game? game = await _gameRepository.GetAsync(id);
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

            var gameForModification = await _gameRepository.GetAsync(id);

            if (gameForModification == null) return NotFound();

            gameForModification = _mappings.GameDTOUpdateToGame(gameForModification, gameDTOUpdate);

            //Alternative?
            _gameRepository.Update(gameForModification);

            await _context.SaveChangesAsync();

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
            _gameRepository.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var game = await _gameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _gameRepository.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
