using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserBase.DALContracts;
using UserBase.Entities;
using UserBase.BLContracts;

namespace UserBase.UserLogic
{
    public class LoginLogic : ILoginLogic
    {
        private SHA1 _sha;

        private ILoginDao _loginDao;

        public LoginLogic()
        {
            _sha = SHA1.Create();
            _loginDao = DaoLinker.loginDao;
        }

        public bool CanLogin(string login, string password)
        {
            if (!LoginExists(login))
            {
                return false;
            }
            else
            {
                var account = _loginDao.Get(login);

                var hash = TakeHash(password);

                var flag = (hash.Trim() == account.PassHash.Trim());

                return (hash.Trim() == account.PassHash.Trim());
            }
        }

        public bool Register(string login, string password)
        {
            if (LoginExists(login))
            {
                throw new Exception("Duplicate login");
            }
            else
            {
                var account = new Account(login, TakeHash(password));

                _loginDao.Add(account);
            }

            return true;
        }

        public string GetRole(string login)
        {
            if (LoginExists(login))
            {
                return _loginDao.Get(login).Role;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public string[] GetAllRoles()
        {
            return _loginDao.GetAllRoles();
        }

        public IEnumerable<Account> GetAll()
        {
            return _loginDao.GetAll();
        }

        public bool ChangeRole(string login, string role)
        {
            if (LoginExists(login))
            {
                return _loginDao.ChangeRole(login, role);
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        private bool LoginExists(string login) => (_loginDao.Get(login) != null);

        public string TakeHash(string password) => Convert.ToBase64String(_sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }
}
