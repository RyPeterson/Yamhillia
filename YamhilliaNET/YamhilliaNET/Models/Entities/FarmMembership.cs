using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using YamhilliaNET.Constants;

namespace YamhilliaNET.Models.Entities
{
    public class FarmMembership : AbstractYamhilliaModel
    {
        [Required]
        public long UserId { set; get; }
        
        [JsonIgnore]
        public virtual User User { set; get; }
        
        [Required]
        public long FarmId { set; get; }
        
        [JsonIgnore]
        public virtual Farm Farm { set; get; }
        
        [Required]
        public MemberType MemberType { set; get; }
    }
}