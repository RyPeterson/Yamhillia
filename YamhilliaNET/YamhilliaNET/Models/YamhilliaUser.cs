using Microsoft.AspNetCore.Identity;

namespace YamhilliaNET.Models
{
    public class YamhilliaUser : IdentityUser
    {
        public virtual Farm Farm { set; get; }

        public long? FarmId { set; get;}
    }
}