using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseProject
{
    public class PasswordConverter
    {
        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (Regex.IsMatch(value, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$") && value.Length == 8)
                {
                    _password = GetHash(value);
                }

            }
        }


        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Convert.FromBase64String(input));

            return Convert.ToBase64String(hash);
        }
    }
}
