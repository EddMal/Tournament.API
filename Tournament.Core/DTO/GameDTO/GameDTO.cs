using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTO.GameDTO
{
    public class GameDTO
    {
        // Update attribute.
        [Required]
        public string Title { get; set; } = string.Empty;

        // Update attribute.
        [Required]
        public DateTime Time { get; set; }

        //Foreign key
        public Guid TournamentId;

    }
}
