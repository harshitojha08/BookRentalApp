namespace PresentationLayer.Models
{
    using System;
    using System.Security.Cryptography;

    public class KeyGenerator
    {
        //generate a random key
        public static string GenerateRandomKey(int keySize)
        {
            byte[] keyBytes = new byte[keySize / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }

            return Convert.ToBase64String(keyBytes);
        }
    }

}
