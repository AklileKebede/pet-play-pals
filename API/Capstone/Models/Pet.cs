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
        public Personality Personality { get; set; }

    }

    public enum PetType
    {
        Dog = 1,
        Cat = 2
    }

    public enum Personality
    {
        Friendly = 1,
        PlaysRough = 2,
        Shy = 3,
        Skittsh = 4,
        HighEnergy = 5,
        Reactive = 6,
        Gentle = 7

    }

}
