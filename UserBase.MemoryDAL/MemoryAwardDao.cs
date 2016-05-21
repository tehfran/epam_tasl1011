using System;
using System.Collections.Generic;
using System.Linq;
using UserBase.DALContracts;
using UserBase.Entities;

namespace UserBase.MemoryDAL
{
    public class MemoryAwardDao : IAwardDao
    {
        private List<Award> _awards;

        public MemoryAwardDao()
        {
            _awards = new List<Award>
            {
                new Award(0, "Purple heart"),
                new Award(1, "Orange heart"),
            };
        }

        public bool Add(Award award)
        {
            _awards.Add(award);
            return true;
        }

        public bool AddImage(int userId, int? imageId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            _awards.RemoveAt(id);
            return true;
        }

        public bool Edit(Award award)
        {
            var id = _awards.FindIndex(x => x.Id == award.Id);
            _awards[id] = award;
            return true;
        }

        public Award Get(int id)
        {
            return _awards.ElementAtOrDefault(id);
        }

        public IEnumerable<Award> GetAll()
        {
            return _awards.OrderBy(x => x.Id).ToList();
        }
    }
}