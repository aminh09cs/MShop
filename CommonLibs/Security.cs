using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibs
{
    public static class Sercurity
    {
        public static string EncryptPassword(string password)
        {
            var salt = "3a9IbkKNr2RjwFwGnPudHbLfA4zugj6TVcoBtlWpJl0m";
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }
        public static bool HasXssFilterChars(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            s = s.ToLower();
            return s.Contains("<") || s.Contains(">") ||
                   s.Contains("%3c") || s.Contains("%3e") ||
                   s.Contains("＜") || s.Contains("＞") ||
                   s.Contains("%ef%bc%9c") || s.Contains("%ef%bc%9e");
        }
    }
}