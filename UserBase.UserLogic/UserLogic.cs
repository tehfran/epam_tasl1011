using System;
using System.Collections.Generic;
using System.Linq;
using UserBase.BLContracts;
using UserBase.DALContracts;
using UserBase.Entities;

namespace UserBase.UserLogic
{
    public class UserLogic : IUserLogic
    {
        private IUserDao _userDao;
        private IAwardDao _awardDao;

        public UserLogic()
        {
            _userDao = DaoLinker.userDao;
            _awardDao = DaoLinker.awardDao;

            MaxID = _userDao.GetAll().Select(x => x.Id).DefaultIfEmpty().Max();
        }

        public int Add(string username, DateTime date)
        {
            int roughAge = DateTime.Now.Year - date.Year;
            if (roughAge > 150 || roughAge < 10)
            {
                throw new ArgumentOutOfRangeException("Invalid age");
            }
            int id = MaxID + 1;
            var user = new User(id, username, date.Date);
            _userDao.Add(user);
            MaxID++;
            return id;
        }

        public IEnumerable<User> GetAll()
        {
            return _userDao.GetAll();
        }

        public bool Delete(int id)
        {
            _userDao.Delete(id);
            return true;
        }

        public User Get(int id)
        {
            var user = _userDao.Get(id);

            if (user == null)
            {
                throw new ArgumentOutOfRangeException($"User with ID {id} not found");
            }
            else return user;
        }

        public bool Edit(User user)
        {
            if (user.Age > 150 || user.Age < 10)
            {
                throw new ArgumentOutOfRangeException("Invalid age");
            }
            _userDao.Edit(user);
            return true;
        }

        private int MaxID { get; set; }
    }
}