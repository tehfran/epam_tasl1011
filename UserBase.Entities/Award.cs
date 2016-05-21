using System.Collections.Generic;

namespace UserBase.Entities
{
    public class Award
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? Image { get; set; }

        public Award(int id, string name)
        {
            Id = id;
            Name = name;
            Image = null;
        }

        public Award(int id, string name, int? image)
        {
            Id = id;
            Name = name;
            Image = image;
        }
    }
}