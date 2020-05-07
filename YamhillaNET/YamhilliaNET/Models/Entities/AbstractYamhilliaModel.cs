using System;

namespace YamhillaNET.Models
{
    public abstract class AbstractYamhilliaModel : IYamhilliaModel
    {
        public long Id { get; set; }
        
        public DateTime CreatedAt { set; get; }
        
        public DateTime UpdatedAt { set; get; }
    }
}