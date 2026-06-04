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
            // 1. Calculamos el hash de la contraseña que metió el usuario en el Login
            byte[] newHash = Hash(password, salt);

            // 2. IMPRIMIMOS EN CONSOLA PARA AUDITAR EL ERROR
            Console.WriteLine("=================== AUDITORÍA DE LOGIN ===================");
            Console.WriteLine($"Contraseña recibida del formulario: '{password}'");
            Console.WriteLine($"Salt recuperado de la BD: '{salt}'");
            Console.WriteLine($"Hash recuperado de la BD (Base64): {Convert.ToBase64String(hash)}");
            Console.WriteLine($"Nuevo Hash generado ahora (Base64): {Convert.ToBase64String(newHash)}");

            // 3. Comparamos los tamaños
            Console.WriteLine($"¿Mismo tamaño de bytes?: {hash.Length == newHash.Length} ({hash.Length} vs {newHash.Length})");

            bool result = newHash.SequenceEqual(hash);
            Console.WriteLine($"¿Resultado final de la comparación?: {result}");
            Console.WriteLine("==========================================================");

            return result;
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
