using YamhilliaNET.Models;

namespace YamhilliaNET.Constants
{
    public class DefaultFarm
    {
        public static readonly string DefaultFarmKey = "DEFAULT";

        public static readonly Farm DefaultFarmData = new Farm()
        {
            Name = "Default Farm",
            Key = DefaultFarmKey,
        };
    }
}