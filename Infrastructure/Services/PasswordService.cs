using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        public (byte[] hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            string salt = Convert.ToBase64String(saltBytes);

            byte[] hash = Hash(password, salt);

            return (hash, salt);
        }
        public bool VerifyPassword(string password, string salt, byte[] hash)
        {
            byte[] newHash = Hash(password, salt);

            return newHash.SequenceEqual(hash);
        }

        private byte[] Hash(string password, string salt)
        {
            string passwordSalt = salt + password;

            using SHA256 sha256 = SHA256.Create();

            return sha256.ComputeHash(
                Encoding.UTF8.GetBytes(passwordSalt));
        }

    }
}
