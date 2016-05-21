using System.Collections.Generic;
using UserBase.Entities;

namespace UserBase.DALContracts
{
    public interface IAwardDao
    {
        Award Get(int id);

        bool Add(Award award);

        bool Delete(int id);

        bool Edit(Award award);        

        IEnumerable<Award> GetAll();

        bool AddImage(int userId, int? imageId);
    }
}