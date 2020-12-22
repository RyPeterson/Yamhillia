using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YamhilliaNET.Models.Entities
{
    public class Farm: AbstractYamhilliaModel
    {
        [Required]
        public string Name { set; get; }
        
        public virtual IEnumerable<FarmMembership> FarmMembers { get; set; }
    }
}