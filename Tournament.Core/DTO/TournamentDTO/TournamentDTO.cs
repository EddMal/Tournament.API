using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Core.DTO.TournamentDTO
{
    public class TournamentDTO
    {
        // Update attribute.
        [Required]
        public string? Title { get; set; }

        // Update attribute.
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate => StartDate.AddMonths(3);

        public IEnumerable<Tournament.Core.DTO.GameDTO.GameDTO> Games { get; init;}

    }
}
