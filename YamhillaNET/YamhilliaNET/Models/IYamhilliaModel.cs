using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YamhillaNET.Models
{
    public interface IYamhilliaModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }
        
        public DateTime CreatedAt { set; get; }
        
        public DateTime UpdatedAt { set; get; }
    }
}