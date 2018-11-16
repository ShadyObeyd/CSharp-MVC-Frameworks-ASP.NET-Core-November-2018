using PANDA.Services.Contracts;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PANDA.Services
{
    public class HashService : IHashService
    {
        public string Hash(string stringToHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

                string hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
