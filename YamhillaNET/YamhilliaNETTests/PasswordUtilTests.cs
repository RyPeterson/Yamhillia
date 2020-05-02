using Xunit;
using YamhillaNET.Util;

namespace YamhilliaNETTests
{
    public class PasswordUtilTests
    {
        [Fact]
        public void TestHashesAreUnique()
        {
            byte[] hash1, salt1;
            PasswordUtil.Hash("password", out hash1, out salt1);
            
            byte[] hash2, salt2;
            PasswordUtil.Hash("password", out hash2, out salt2);
            
            Assert.NotEqual(hash1, hash2);
            Assert.NotEqual(salt1, salt2);
        }

        [Fact]
        public void TestVerify()
        {
            byte[] hash1, salt1;
            PasswordUtil.Hash("password", out hash1, out salt1);
            Assert.True(PasswordUtil.Verify("password", hash1, salt1));
            Assert.False(PasswordUtil.Verify("password ", hash1, salt1));
            
            byte[] hash2, salt2;
            PasswordUtil.Hash("password", out hash2, out salt2);
            Assert.False(PasswordUtil.Verify("password", hash1, salt2));
            Assert.False(PasswordUtil.Verify("password", hash2, salt1));

        }
    }
}