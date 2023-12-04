using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.API.Data.Data;
using Tournament.Core.Repositories;

namespace Tournament.Core.Repositories
{
    public class UOW: IUOW
    {
        private readonly TournamentAPIContext _context;

        private readonly IGameRepository _gameRepository;

        private readonly ITournamentRepository _tournamentRepository;

        //Investigate/research Lazy..
        //private readonly Lazy<IGameRepository> _gameRepository;

        //private readonly Lazy<ITournamentRepository> _tournamentRepository;

        //public IGameRepository GameRepository => _gameRepository.Value;

        //public ITournamentRepository TournamentRepository => _tournamentRepository.Value;


        public IGameRepository GameRepository => _gameRepository;

        public ITournamentRepository TournamentRepository => _tournamentRepository;


        public UOW(TournamentAPIContext context, IGameRepository gameRepo, ITournamentRepository tournamentRepo) 
        {
            _context = context;
            _gameRepository = gameRepo;
            _tournamentRepository = tournamentRepo;
        }


        public async Task CompleteAsync() 
        {

            await _context.SaveChangesAsync();
        }

    }
}
