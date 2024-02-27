using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Services.Helpers
{
    public class PasswordGenerator
    {
        private static readonly Random random = new Random();

        public string GenerateRandomPassword(int userId)
        {
            string randomPart = GenerateRandomString(4);

            string password = $"{userId}_{randomPart}";

            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Iterations = 64;
                argon2.MemorySize = 4096;
                argon2.DegreeOfParallelism = 4;

                byte[] hashBytes = argon2.GetBytes(10);
                return Convert.ToBase64String(hashBytes);
            }
        }
        public string GenerateRandomSimplePassword(int userId, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();

            // Generate a random part
            string randomPart = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            // Combine the user ID and the random part
            string password = $"{userId}_{randomPart}";

            return password;
        }
        private string GenerateRandomString(int length)
        {
            const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            return new string(Enumerable.Repeat(Characters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
