using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;


namespace EduHubWeb.Services
{
    public class PasswordHasher
    {
        public static string HashPassword(string password, string salt)
        {
            // Generate a unique salt for each user
            byte[] saltBytes = Convert.FromBase64String(salt);

            // Generate the hashed password
            string hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000, // Adjust this based on your security requirements
                    numBytesRequested: 256 / 8 // 256-bit hash
                )
            );

            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string salt, string hashedPassword, string storedSalt)
        {
            // Verify the password
            return hashedPassword == HashPassword(password, storedSalt);
        }

        public static string GenerateSalt()
        {
            // Generate a random salt
            byte[] salt = new byte[16]; // 128 bits
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}
