using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YamhillaNET.Models.Entities
{
    public interface IYamhilliaModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }
        
        public DateTime CreatedAt { set; get; }
        
        public DateTime UpdatedAt { set; get; }
        
        /// <summary>
        /// Alternative to ID to track an entity.
        /// Also used to help migrate to different databases
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public string EntityUUID { set; get; }
    }
}