using System.Text.Json.Serialization;
using YamhilliaNET.Constants;

namespace YamhilliaNET.Models.Entities
{
    public class FarmMembership : AbstractYamhilliaModel
    {
        public long UserId { set; get; }
        
        [JsonIgnore]
        public virtual User User { set; get; }
        
        public long FarmId { set; get; }
        
        [JsonIgnore]
        public virtual Farm Farm { set; get; }
        
        public MemberType MemberType { set; get; }
    }
}