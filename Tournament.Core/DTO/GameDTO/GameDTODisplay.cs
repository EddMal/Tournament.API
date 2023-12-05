using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTO.GameDTO
{
    internal class GameDTODisplay
    {
        // Update attribute.
        [Required]
        public string? Title { get; set; }

        // Update attribute.
        [Required]
        public DateTime Time { get; set; }

        // Update attribute.
        public string? TournamentTitle { get; set; }
    }
}
