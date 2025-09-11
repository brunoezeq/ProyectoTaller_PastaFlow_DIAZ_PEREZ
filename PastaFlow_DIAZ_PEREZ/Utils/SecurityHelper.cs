using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PastaFlow_DIAZ_PEREZ.Utils
{
    public static class SeguridadHelper
    {
        public static byte[] ComputeSha256Hash(string plainText)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.Unicode.GetBytes(plainText));
            }
        }
    }
}
