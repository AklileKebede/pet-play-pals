using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public DateTime Birthday { get; set; }
        public char Sex { get; set; }
        public PetType PetType { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        public string Bio { get; set; }

    }

    public enum PetType
    {
        Dog = 1,
        Cat = 2
    }
}
