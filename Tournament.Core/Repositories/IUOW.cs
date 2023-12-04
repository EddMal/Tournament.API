using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Repositories
{
    public interface IUOW
    {
        IGameRepository GameRepository { get; }
        ITournamentRepository TournamentRepository { get; }

        public Task CompleteAsync();

    }
}
