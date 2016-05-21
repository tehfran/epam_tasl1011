using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserBase.Entities;

namespace UserBase.DALContracts
{
    public interface ILoginDao
    {
        Account Get(string login);

        bool Add(Account acc);

        bool Edit(Account acc);

        IEnumerable<Account> GetAll();

        bool AddRole(int id, string role);

        string[] GetAllRoles();

        bool ChangeRole(string login, string role);
    }
}
