using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTO.SeedDTO
{
    public class SeedDTO
    {

        public class Rootobject
        {
            public SeedData SeedData { get; set; }
        }

        public class SeedData
        {
            public Game[] Games { get; set; }
            public Tournament[] Tournaments { get; set; }
        }

        public class Game
        {
            public string Name { get; set; }
        }

        public class Tournament
        {
            public string Name { get; set; }
        }

    }
}
