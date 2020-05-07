using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Internal;

namespace YamhillaNET.Util
{
    public class PasswordUtil
    {
        /// <summary>
        /// Requires:
        /// at least 1 uppercase
        /// at least 1 lowercase
        /// at least 1 digit
        /// at least 1 "special" character
        /// at least 8 characters
        /// </summary>
        private static readonly HashSet<char> SpecialCharacters = new HashSet<char>() { '%', '$', '#', '*', '@' };

        private static readonly int MinLength = 8;
        private static readonly int MinConditionsMet = 5;
        
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

        public static bool IsStrongEnough(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var conditionsMet = 0;
            if (password.Length >= MinLength)
            {
                conditionsMet++;
            }
            if (password.Any(char.IsLower))
            {
                conditionsMet++;
            }
            if (password.Any(char.IsUpper))
            {
                conditionsMet++;
            }
            if (password.Any(char.IsDigit))
            {
                conditionsMet++;
            }
            if (password.Any(SpecialCharacters.Contains))
            {
                conditionsMet++;
            }

            return conditionsMet >= MinConditionsMet;
        }
    }
}