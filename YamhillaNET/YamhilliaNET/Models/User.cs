using System.ComponentModel.DataAnnotations;

namespace YamhillaNET.Models
{
    public class User: AbstractYamhilliaModel
    {
        [Required]
        public string Username { set; get; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}