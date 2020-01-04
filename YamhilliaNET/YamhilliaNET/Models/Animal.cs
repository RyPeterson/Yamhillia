using System;
using YamhilliaNET.Constants;

namespace YamhilliaNET.Models
{
    public class Animal : AbstractYamhilliaModel
    {
        public string Name { set; get; }

        public Genders Gender { set; get; }

        public Species Species { set; get; }

        public DateTime? DateOfBirth { set; get;}

        public DateTime? DateOfDeath { set; get;}
 
        public virtual Farm Farm { set; get; }
    }
}