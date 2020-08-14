using System;

namespace YamhilliaNET.Models
{
    public class UpdateUser
    {
        public long Id { set; get; }
        
        public string Username { set; get; }
        
        public string Password { set; get; }
    }
}