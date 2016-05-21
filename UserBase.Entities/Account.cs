using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBase.Entities
{
    public class Account
    {
        public string Login { get; set; }

        public string PassHash { get; set; }

        public string Role { get; set; }

        public Account(string login, string hash)
        {
            Login = login;

            PassHash = hash;

            Role = "user";
        }

        public Account(string login, string hash, string role)
        {
            Login = login;

            PassHash = hash;

            Role = role;
        }
    }
}
