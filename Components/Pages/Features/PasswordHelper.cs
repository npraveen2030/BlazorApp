using System.Security.Cryptography;
using System.Text;
namespace BlazorApp.Components.Pages.Features
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            // Derive a key (hash the password with the salt)
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32); // 256-bit hash

            // Combine salt + hash into one byte array
            byte[] hashBytes = new byte[48];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
            Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);

            // Convert to base64 for storage
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != hash[i]) return false;
            }

            return true;
        }
    }
}