using System;
using System.Collections.Generic;
using UserBase.Entities;

namespace UserBase.BLContracts
{
    public interface IUserLogic
    {
        int Add(string username, DateTime date);

        bool Delete(int id);

        IEnumerable<User> GetAll();

        User Get(int id);
    }
}