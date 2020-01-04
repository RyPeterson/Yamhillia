using System.Collections.Generic;

namespace YamhilliaNET.Models
{
    /// <summary>
    /// Basic organizational unit, a Farm can belong to one person or multiple people.
    /// </summary>
    public class Farm : AbstractYamhilliaModel
    {
        public string Name { set; get; }

        public virtual IEnumerable<YamhilliaUser> Users { set; get; }

        public virtual IEnumerable<Animal> Animals { set; get;}
    }
}