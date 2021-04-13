using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers.searchfilters
{
    public class PlaydateSearchFilter
    {
        public int userId { get; set; } = -1;

        public Dictionary<int, bool> personalitiesPermitted { get; set; } = new Dictionary<int, bool>();

        public List<int> allowedPersonalities
        {
            get
            {
                List<int> personalites = new List<int>();
                foreach (KeyValuePair<int, bool> kvp in this.personalitiesPermitted) { if (kvp.Value) { personalites.Add(kvp.Key); } }
                if (personalites.Count == 0) { personalites.Add(-1); };
                return personalites;
            }

        }

        public List<int> disallowedPersonalities
        {
            get
            {
                List<int> personalites = new List<int>();
                foreach (KeyValuePair<int, bool> kvp in this.personalitiesPermitted) { if (!kvp.Value) { personalites.Add(kvp.Key); } }
                if (personalites.Count == 0) { personalites.Add(-1); };
                return personalites;
            }
        }

        public Dictionary<int, bool> petTypesPermitted { get; set; } = new Dictionary<int, bool>();

        public List<int> allowedPetTypes
        {
            get
            {
                List<int> petTypes = new List<int>();
                foreach (KeyValuePair<int, bool> kvp in this.petTypesPermitted) { if (kvp.Value) { petTypes.Add(kvp.Key); } }
                if (petTypes.Count == 0) { petTypes.Add(-1); };
                return petTypes;
            }

        }

        public List<int> disallowedPetTypes
        {
            get
            {
                List<int> petTypes = new List<int>();
                foreach (KeyValuePair<int, bool> kvp in this.petTypesPermitted) { if (!kvp.Value) { petTypes.Add(kvp.Key); } }
                if (petTypes.Count == 0) { petTypes.Add(-1); };
                return petTypes;
            }
        }

        // Search Radius 

        public float searchRadius { set; get; } = -1;

        // Search Center (zip=> lat., lng.)
        public Location searchCenter { get; set; }

    }
}
