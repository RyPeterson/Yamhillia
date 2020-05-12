using System;

namespace YamhillaNET.Models.Entities
{
    public abstract class AbstractYamhilliaModel : IYamhilliaModel
    {
        public long Id { get; set; }
        
        public DateTime CreatedAt { set; get; }
        
        public DateTime UpdatedAt { set; get; }
    }
}