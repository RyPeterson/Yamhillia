using System;
using Xunit;
using YamhilliaNET.Util;

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

        [Fact]
        public void TestStrength()
        {
            Assert.False(PasswordUtil.IsStrongEnough(null));
            Assert.False(PasswordUtil.IsStrongEnough(""));
            Assert.False(PasswordUtil.IsStrongEnough("        "));
            Assert.False(PasswordUtil.IsStrongEnough("1234567"));
            Assert.False(PasswordUtil.IsStrongEnough("12345678"));
            // Only 2 requirement
            Assert.False(PasswordUtil.IsStrongEnough("1234567m"));
            // 3 requirements
            Assert.False(PasswordUtil.IsStrongEnough("123456Mm"));
            
            Assert.True(PasswordUtil.IsStrongEnough("12345*Mm"));
            
            Assert.True(PasswordUtil.IsStrongEnough("Password1@"));
        }

        [Fact]
        public void Test_Hash_Fails_NullPassword()
        {
            byte[] hash;
            byte[] salt;
            Assert.Throws<ArgumentNullException>(() => PasswordUtil.Hash(null, out hash, out salt));
        }

        [Fact]
        public void Test_Hash_EmptyPassword()
        {
            byte[] hash;
            byte[] salt;
            Assert.Throws<ArgumentException>(() => PasswordUtil.Hash("", out hash, out salt));

        }

        [Fact]
        public void Test_Verify_EmptyPassword()
        {
            Assert.False(PasswordUtil.Verify("", new byte[]{}, new byte[]{}));
            Assert.False(PasswordUtil.Verify(null, new byte[]{}, new byte[]{}));

        }

        [Fact]
        public void Test_Verify_InvalidHashOrSalt()
        {
            // Verify we're actually testing correctly
            PasswordUtil.Verify("nope", new byte[64], new byte[128]);
            Assert.Throws<ArgumentException>(() => PasswordUtil.Verify("nope", new byte[63], new byte[128]));
            Assert.Throws<ArgumentException>(() => PasswordUtil.Verify("nope", new byte[64], new byte[127]));

        }
    }
}