using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Playdate
    {
        public int PlaydateId { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int UserId { get; set; }
        public Location location { get; set; }
        public List<Pet> Participants { get; set; }



    }
}
