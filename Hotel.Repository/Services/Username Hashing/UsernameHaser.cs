using System;
using System.Security.Cryptography;

namespace Hotel.Repository.Services.Username_Hashing
{
    public sealed class UserNameHaser : IUsernameHasher
    {
        private readonly int SaltSize = 16;
        private readonly int HashSize = 20;
        private readonly int Iterations = 10000;
        // Change from SHA3_512 to SHA256 for platform compatibility
        private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;

        public string HashUserName(string userName)
        {
            // Use a cryptographically secure random number generator
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Use SHA256 which is widely supported
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(userName, salt, Iterations, hashAlgorithmName, HashSize);

            // Format as hex strings with colon separator
            return $"{Convert.ToHexString(hash)}:{Convert.ToHexString(salt)}";
        }

        public bool VerifyUserName(string userName, string hashedUserName)
        {
            string[] parts = hashedUserName.Split(':');
            byte[] salt = Convert.FromHexString(parts[1]);
            byte[] hash = Convert.FromHexString(parts[0]);

            byte[] testHash = Rfc2898DeriveBytes.Pbkdf2(userName, salt, Iterations, hashAlgorithmName, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, testHash);
        }
    }
}