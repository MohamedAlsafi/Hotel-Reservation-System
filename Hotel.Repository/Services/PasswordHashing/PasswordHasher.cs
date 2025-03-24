using System;
using System.Security.Cryptography;

namespace Hotel.Repository.Services.PasswordHashing
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private readonly int SaltSize = 16;
        private readonly int HashSize = 20;
        private readonly int Iterations = 10000;
        // Use SHA256 which is widely supported across platforms
        private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;

        public string HashPassword(string password)
        {
            // Use a cryptographically secure random number generator
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Use the SHA256 algorithm which is widely supported
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithmName, HashSize);

            // Format the hash and salt as hex strings separated by a colon
            return $"{Convert.ToHexString(hash)}:{Convert.ToHexString(salt)}";
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            string[] parts = hashedPassword.Split(':');
            byte[] salt = Convert.FromHexString(parts[1]);
            byte[] hash = Convert.FromHexString(parts[0]);

            byte[] testHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithmName, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, testHash);
        }
    }
}