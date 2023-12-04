using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.API.Data.Data;
using Tournament.Core.Repositories;

namespace Tournament.Data.Repositories
{
    public class GameRepository: IGameRepository
    {
        private readonly TournamentAPIContext _context;
        public GameRepository(TournamentAPIContext context)
        {
            _context = context;
        }
        //INSERT VERIFICATION AND RETURN BOOL IF SUCESS.
        public void Add(Core.Entities.Game game)
        {
            _context.Games.Add(game);
            //await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await _context.Games.AnyAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Core.Entities.Game>> GetAllAsync()
        {
            var games = await _context.Games.ToListAsync();

            //if (game.Count == 0)
            //{
            //    return new List<game>();
            //}

            return games;
        }

        public async Task<Core.Entities.Game> GetAsync(Guid id)
        {
            Core.Entities.Game? game = await _context.Games.FirstOrDefaultAsync(t => t.Id == id);
            //if (tournament == null)
            //{
            //    //MESSAGE??
            //    return new tournament;
            //}
            return game!;

        }

        public void Remove(Core.Entities.Game game)
        {
            _context.Games.Remove(game);
        }

        public void Update(Core.Entities.Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
        }
    }
}
