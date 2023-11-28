using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Entities
{
    public class Game
    {
        // KEY
        public Guid Id { get; set; }
    
        // Add attribute.
        public string Title { get; set; } = string.Empty;

        // Add attribute.
        public DateTime Time { get; set; }

        //Foreign key
        public Guid TournamentId;

        //Navigation property
        public Tournaments Tournament { get; set; } = new Tournaments();

    }
}
