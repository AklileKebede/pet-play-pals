using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Playdate
    {
        public int PlaydateId { get; set; }

        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public Location location { get; set; }



    }
}
