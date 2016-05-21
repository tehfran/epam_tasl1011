using System;
using System.Collections.Generic;

namespace UserBase.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime BirthDate { get; set; }

        public HashSet<int> Awards { get; set; }

        public int? Image { get; set; }

        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - BirthDate.Year;
                if (BirthDate.Date.AddYears(age) > now.Date)
                {
                    --age;
                }

                return age;
            }
        }

        public User(int id, string username, DateTime birthdate)
        {
            Id = id;
            Username = username;
            BirthDate = birthdate;
            Awards = new HashSet<int>();
            Image = null;
        }

        public User(int id, string username, DateTime birthdate, IEnumerable<int> awards)
        {
            Id = id;
            Username = username;
            BirthDate = birthdate;
            Awards = new HashSet<int>(awards);
            Image = null;
        }

        public User(int id, string username, DateTime birthdate, IEnumerable<int> awards, int? image)
        {
            Id = id;
            Username = username;
            BirthDate = birthdate;
            Awards = new HashSet<int>(awards);
            Image = image;
        }
    }
}