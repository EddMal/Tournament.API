using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTO.GameDTO
{
    public class GameDTO
    {
        // Add attribute.
        public string Title { get; set; } = string.Empty;

        // Add attribute.
        public DateTime Time { get; set; }

        //Foreign key
        public Guid TournamentId;

    }
}
