using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Entities
{
    public class Tournament
    {

        // KEY
        public Guid Id { get; set; }

        // Update attribute.
        [Required]
        public string? Title { get; set; }

        // Update attribute.
        [Required]
        public DateTime StartDate { get; set; }

        //Navigation Property
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
