using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YamhillaNET.Exceptions;
using YamhillaNET.Util;

namespace YamhillaNET.Constants
{
    public class Species: StringEnumeration
    {
        private Species(string value) : base(value)
        {
        }
        
        public static readonly Species Alpaca = new Species("Alpaca");
        public static readonly Species Buffalo = new Species("Buffalo");
        public static readonly Species Cow = new Species("Cow");
        public static readonly Species Cat = new Species("Cat");
        public static readonly Species Chicken = new Species("Chicken");
        public static readonly Species Donkey = new Species("Donkey");
        public static readonly Species Dog = new Species("Dog");
        public static readonly Species Duck = new Species("Duck");
        public static readonly Species Emu = new Species("Emu");
        public static readonly Species Goat = new Species("Goat");
        public static readonly Species GuineaPig = new Species("Guinea Pig");
        public static readonly Species Goose = new Species("Goose");
        public static readonly Species Horse = new Species("Horse");
        public static readonly Species HoneyBee = new Species("Honey Bee");
        public static readonly Species Llama = new Species("Llama");
        public static readonly Species Pig = new Species("Pig");
        public static readonly Species Pigeon = new Species("Pigeon");
        public static readonly Species Rabbit = new Species("Rabbit");
        public static readonly Species Sheep = new Species("Sheep");
        public static readonly Species Silkworm = new Species("Silkworm");
        public static readonly Species Turkey = new Species("Turkey");

        
        public static readonly IList<Species> SpeciesList = new List<Species>()
        {
            Alpaca,
            Buffalo,
            Cow,
            Cat,
            Chicken,
            Donkey,
            Dog,
            Duck,
            Emu,
            Goat,
            GuineaPig,
            Goose,
            Horse,
            HoneyBee,
            Llama,
            Pig,
            Pigeon,
            Rabbit,
            Sheep,
            Silkworm,
            Turkey,
        }.AsReadOnly();

        public static Species ValueOf(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var species = SpeciesList.FirstOrDefault(v => v.Value.ToLower() == value.ToLower());
                if (species != null)
                {
                    return species;
                }
            }
            throw new YamhilliaNotFoundError(@$"{value} is not mapped to a species");
        }
    }
}