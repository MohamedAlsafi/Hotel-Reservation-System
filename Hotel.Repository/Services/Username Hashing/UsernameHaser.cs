using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.Username_Hashing
{
    public sealed class UserNameHaser : IUsernameHasher
    {
        private readonly int SaltSize = 16;
        private readonly int HashSize = 20;
        private readonly int Iterations = 10000;
        private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA3_512;
        public string HashUserName(string userName)
        {
            byte[] salt = new byte[SaltSize];
            new Random().NextBytes(salt);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(userName, salt, Iterations, hashAlgorithmName, HashSize);
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
            return $"{Convert.ToHexString(hash)}:{Convert.ToHexString(salt)}";

        }

        public bool VerifyUserName(string userName, string hashedUserName)
        {
            string[] parts = hashedUserName.Split(':');
            byte[] salt = Convert.FromHexString(parts[1]);
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] InputHash = Rfc2898DeriveBytes.Pbkdf2(userName, salt, Iterations, hashAlgorithmName, HashSize);
            return CryptographicOperations.FixedTimeEquals(hash, InputHash);
        }
    }
}
