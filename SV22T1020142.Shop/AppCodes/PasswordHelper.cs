using System.Security.Cryptography;
using System.Text;

namespace SV22T1020142.Shop.AppCodes
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using var md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password ?? ""));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
