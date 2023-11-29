using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Core.DTO.TournamentDTO
{
    public class TournamentDTO
    {
        // Add attribute.
        public string? Title { get; set; }

        // Add attribute.
        public DateTime StartDate { get; set; }

        //// Add attribute
        //public Game Game { get; set; }
    }
}
