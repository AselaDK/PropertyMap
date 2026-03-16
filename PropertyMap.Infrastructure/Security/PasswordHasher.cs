using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PropertyMap.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int IterationCount = 10000;
        private const int NumBytesRequested = 32; // 256 bits

        public string HashPassword(string password, out string salt)
        {
            salt = GenerateSalt();
            return HashPassword(password, salt);
        }

        public string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount,
                numBytesRequested: NumBytesRequested);

            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            string computedHash = HashPassword(password, salt);
            return computedHash == hash;
        }

        public string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8]; // 16 bytes
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}