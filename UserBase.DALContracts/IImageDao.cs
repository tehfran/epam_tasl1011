using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBase.Entities;

namespace UserBase.DALContracts
{
    public interface IImageDao
    {
        bool Add(Image image);

        Image Get(int id);

        bool Delete(int id);

        List<Image> GetAll();
    }
}
