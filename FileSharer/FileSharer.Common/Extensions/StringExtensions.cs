using System;
using System.Security.Cryptography;
using System.Text;

namespace FileSharer.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetHashSHA256(this string message)
        {
            var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
            var hash = Convert.ToBase64String(bytes);

            return hash;
        }
    }
}
