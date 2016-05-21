using System.Collections.Generic;
using UserBase.Entities;

namespace UserBase.DALContracts
{
    public interface IUserDao
    {
        bool Add(User user);

        bool Delete(int id);

        User Get(int id);

        IEnumerable<User> GetAll();

        bool Rescind(int userId, int awardId);

        bool Grant(int userId, int awardId);

        bool Edit(User user);

        bool AddImage(int userId, int? imageId);
    }
}