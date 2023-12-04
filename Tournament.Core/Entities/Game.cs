using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        // Update attribute.
        [Required]
        public string Title { get; set; } = string.Empty;

        // Add attribute.
        // Update attribute.
        [Required]
        public DateTime Time { get; set; }

        //Foreign key
        public Guid TournamentId { get; set; }

        //Navigation property
        public Tournament? Tournament { get; set; } //= new Tournament();

    }
 
}
