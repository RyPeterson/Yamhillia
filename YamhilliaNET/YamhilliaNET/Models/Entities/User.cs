using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YamhilliaNET.Models.Entities
{
    public class User: AbstractYamhilliaModel
    {
        [Required]
        public string Username { set; get; }
        
        [Required]
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        
        [Required]
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
    }
}