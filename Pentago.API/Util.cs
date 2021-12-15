using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pentago.API
{
    public static class Util
    {
        public static string Sha256Hash(string value)
        {
            using var hash = SHA256.Create();

            return string.Concat(hash
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                .Select(item => item.ToString("x2")));
        }
        
        public static string GenerateApiKey()
        {
            var key = new byte[32];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(key);

            return Convert.ToBase64String(key);
        }

        public static string ToStandard(this string input) => input.Trim().Normalize().ToLower();
    }
}