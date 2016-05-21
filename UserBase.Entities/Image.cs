using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBase.Entities
{
    public class Image
    {
        public byte[] Content { get; set; }

        public int Id { get; set; }

        public string ContentType { get; set; }

        public Image()
        {

        }
        public Image(int id, byte[] content, string type)
        {
            Id = id;
            Content = content;
            ContentType = type;
        }
    }
}
