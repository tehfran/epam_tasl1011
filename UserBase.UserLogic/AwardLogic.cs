using System;
using System.Collections.Generic;
using System.Linq;
using UserBase.BLContracts;
using UserBase.DALContracts;
using UserBase.Entities;

namespace UserBase.UserLogic
{
    public class AwardLogic : IAwardLogic
    {
        private IAwardDao _awardDao;
        private IUserDao _userDao;

        public int MaxID { get; private set; }

        public AwardLogic()
        {
            _userDao = DaoLinker.userDao;
            _awardDao = DaoLinker.awardDao;

            MaxID = _awardDao.GetAll().Select(x => x.Id).DefaultIfEmpty().Max();
        }

        public int Add(string name)
        {
            int id = MaxID + 1;
            var award = new Award(id, name);
            _awardDao.Add(award);
            MaxID++;
            return id;
        }

        public bool Delete(int id)
        {
            var ids = Users(id).Select(x => x.Id);
            foreach (int item in ids)
            {
                Rescind(item, id);
            }
            _awardDao.Delete(id);
            return true;
        }

        public bool Grant(int userId, int awardId)
        {
            if (_userDao.Get(userId) == null)
            {
                throw new ArgumentOutOfRangeException($"User with ID {userId} not found");
            }

            if (_awardDao.Get(awardId) == null)
            {
                throw new ArgumentOutOfRangeException($"Award with ID {awardId} not found");
            }

            _userDao.Grant(userId, awardId);

            return true;
        }

        public bool Rescind(int userId, int awardId)
        {
            if (_userDao.Get(userId) == null)
            {
                throw new ArgumentOutOfRangeException($"User with ID {userId} not found");
            }

            if (_awardDao.Get(awardId) == null)
            {
                throw new ArgumentOutOfRangeException($"Award with ID {awardId} not found");
            }

            _userDao.Rescind(userId, awardId);

            return true;
        }

        public IEnumerable<Award> GetAll()
        {
            return _awardDao.GetAll();
        }

        public Award Get(int id)
        {
            var award = _awardDao.Get(id);

            if (award == null)
            {
                throw new ArgumentOutOfRangeException($"Award with ID {id} not found");
            }
            else return award;
        }

        public bool Edit(Award award)
        {
            _awardDao.Edit(award);
            return true;
        }

        public List<User> Users(int id)
        {
            return _userDao.GetAll().Where(u => u.Awards.Contains(id)).ToList();
        }
    }
}