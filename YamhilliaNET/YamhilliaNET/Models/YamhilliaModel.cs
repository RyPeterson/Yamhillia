using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YamhilliaNET.Models
{
    public interface YamhilliaModel<PKey>
    {
        PKey Id { set; get; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }

    public abstract class AbstractYamhilliaModel : YamhilliaModel<long>
    {
        [Key]
        public long Id { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}