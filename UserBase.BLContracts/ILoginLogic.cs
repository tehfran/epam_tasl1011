using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBase.Entities;

namespace UserBase.BLContracts
{
    public interface ILoginLogic
    {
        bool CanLogin(string login, string password);

        bool Register(string login, string password);

        string GetRole(string login);

        IEnumerable<Account> GetAll();
    }
}
