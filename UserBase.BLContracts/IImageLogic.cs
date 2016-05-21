using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBase.Entities;

namespace UserBase.BLContracts
{
    public interface IImageLogic
    {
        int Add(byte[] content, string type, string source, int sourceId);

        bool Delete(int id);

        Image Get(int id);

        IEnumerable<Image> GetAll();
    }
}
