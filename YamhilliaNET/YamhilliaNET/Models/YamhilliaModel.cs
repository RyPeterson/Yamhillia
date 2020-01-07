using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YamhilliaNET.Models
{
    public interface YamhilliaModel
    {
        long Id { set; get; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }

    public abstract class AbstractYamhilliaModel : YamhilliaModel
    {
        [Key]
        public long Id { set; get; }

        
        public DateTime CreatedAt { get; set; }

        
        public DateTime UpdatedAt { get; set; }
    }
}