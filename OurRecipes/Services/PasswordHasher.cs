using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace OurRecipes.Services
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
           
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

          
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }
        public static string HashPasswordWithoutSalt(string password)
        {

            byte[] salt = Array.Empty<byte>();

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));


            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }
        public static bool VerifyPassword(string storedHash, string providedPassword)
        {
        
            string[] parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                throw new FormatException("Unexpected hash format. Should be formatted as 'salt:hash'.");
            }

          
            byte[] salt = Convert.FromBase64String(parts[0]);
            string hashedPassword = parts[1];

         
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: providedPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            
            return hashedPassword == hashed;
        }
    }

}
