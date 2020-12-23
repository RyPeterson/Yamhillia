using YamhilliaNET.Constants;

namespace YamhilliaNET.Models.Farms
{
    public class AddUserToFarmParams
    {
        public long FarmId { set; get; }
        public long RequesterId { set; get; }
        public long UserId { set; get; }
        public MemberType MemberType { set; get; }
    }
}