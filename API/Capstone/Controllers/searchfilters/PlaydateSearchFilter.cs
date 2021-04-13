using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers.searchfilters
{
    public class PlaydateSearchFilter
    {
        public int userId { get; set; } = -1;
        public int[] requiredPetTypes { get; set; } = new int[] { -1 };
        public int[] disallowedPetTypes { get; set; } = new int[] { -1 };
        public int[] allowedPersonalities { get; set; } = new int[] { -1 };
        public int[] disallowedPersonalities { get; set; } = new int[] { -1 };


    }
}
