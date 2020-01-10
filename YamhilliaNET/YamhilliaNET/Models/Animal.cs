using System;
using System.ComponentModel.DataAnnotations;
using YamhilliaNET.Constants;

namespace YamhilliaNET.Models
{
    public class Animal : AbstractYamhilliaModel
    {
        [Required]
        public string Name { set; get; }

        [Required]
        public Genders Gender { set; get; }

        [Required]
        public Species Species { set; get; }

        public DateTime? DateOfBirth { set; get;}

        public DateTime? DateOfDeath { set; get;}
 
        public virtual Farm Farm { set; get; }

        public long FarmId { set; get;}

        public string CustomIdentifier { set; get;}
    }
}