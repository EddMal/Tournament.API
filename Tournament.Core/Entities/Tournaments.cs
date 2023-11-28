using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Entities
{
    public class Tournaments
    {

        // KEY
        public Guid Id { get; set; }

        // Add attribute.
        public string? Title { get; set; }

        // Add attribute.
        public DateTime StartDate { get; set; }

        //Navigation Property
        ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
