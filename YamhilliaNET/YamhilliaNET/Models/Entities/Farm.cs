using System.ComponentModel.DataAnnotations;

namespace YamhilliaNET.Models.Entities
{
    public class Farm: AbstractYamhilliaModel
    {
        [Required]
        public string Name { set; get; }
        
        [Required]
        public long OwnerId { set; get; }
        public virtual User Owner { set; get; }
    }
}