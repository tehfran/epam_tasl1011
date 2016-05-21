using System;
using System.Collections.Generic;
using System.Linq;
using UserBase.DALContracts;
using UserBase.Entities;

namespace UserBase.MemoryDAL
{
    public class MemoryUserDao : IUserDao
    {
        private List<User> _users;

        public MemoryUserDao()
        {
            _users = new List<User>
            {
                new User(0, "Stepan Ivanov", new DateTime(1980, 5, 8)),
                new User(1, "Ivan Petrov", new DateTime(1955, 2, 1)),
                new User(2, "Petr Sidorov", new DateTime(1970, 12, 30)),
            };
        }

        public bool Add(User user)
        {
            _users.Add(user);
            return true;
        }

        public bool Delete(int id)
        {
            _users.RemoveAt(id);
            return true;
        }

        public bool Grant(int userId, int awardId)
        {
            var user = _users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new ArgumentOutOfRangeException($"User ID {userId} not found");
            }
            _users.Find(x => x.Id == userId).Awards.Add(awardId);
            return true;
        }

        public bool Rescind(int userId, int awardId)
        {
            _users.Find(x => x.Id == userId).Awards.Remove(awardId);
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.OrderBy(x => x.Id).ToList();
        }

        public User Get(int id)
        {
            return _users.ElementAtOrDefault(id);
        }

        public bool Edit(User user)
        {
            var id = _users.FindIndex(x => x.Id == user.Id);
            _users[id] = user;
            return true;
        }

        public bool AddImage(int userId, int? imageId)
        {
            throw new NotImplementedException();
        }
    }
}