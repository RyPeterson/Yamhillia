using System;
using System.Security.Cryptography;
using System.Text;

namespace YamhillaNET.Util
{
    public class PasswordUtil
    {
        public static void Hash(string password, out byte[] hash, out byte[] salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty or blank.", "password");
            }

            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool Verify(string password, byte[] hash, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (hash.Length !=  64)
            {
                throw new ArgumentException("Invalid hash. Must be 64 bytes", "hash");
            }

            if (salt.Length != 128)
            {
                throw new ArgumentException("Invalid salt. Must be 128 bytes", "salt");
            }

            using (var hmac = new HMACSHA512(salt))
            {
                var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computed.Length; i++)
                {
                    if (computed[i] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}