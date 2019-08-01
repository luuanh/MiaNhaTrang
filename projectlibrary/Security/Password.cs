using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.Config;

namespace ProjectLibrary.Security
{
    public class Password
    {
        public static string HashPassword(string strword)
        {
            if (string.IsNullOrEmpty(strword))
            {
               throw new Exception("Strword not null");
            }
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(strword + ConfigPassword.AddStringPass);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public static bool CheckPassword(string password, string storedHash)
        {
            return HashPassword(password + ConfigPassword.AddStringPass) == storedHash;
        }
    }
}
