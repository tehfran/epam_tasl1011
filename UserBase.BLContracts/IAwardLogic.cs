using System.Collections.Generic;
using UserBase.Entities;

namespace UserBase.BLContracts
{
    public interface IAwardLogic
    {
        int Add(string name);

        bool Grant(int userId, int awardId);

        bool Rescind(int userId, int awardId);

        bool Delete(int id);

        IEnumerable<Award> GetAll();

        Award Get(int id);

        List<User> Users(int id);
    }
}