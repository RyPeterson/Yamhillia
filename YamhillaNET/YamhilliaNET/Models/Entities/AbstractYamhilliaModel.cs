using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YamhillaNET.Models.Entities
{
    public abstract class AbstractYamhilliaModel : IYamhilliaModel
    {
        public long Id { get; set; }
        
        public DateTime CreatedAt { set; get; }
        
        public DateTime UpdatedAt { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string EntityUUID { get; set; }
    }
}