using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UserBase.DALContracts;
using UserBase.Entities;

namespace UserBase.FileDAL
{
    public class FileUserDao : IUserDao
    {
        public FileUserDao()
        {
            _userpath = ConfigurationManager.AppSettings["userpath"];
            Console.WriteLine("_userpath");
            if (!PathChecker(_userpath))
            {
                _users = new XDocument(new XElement("users"));
                _users.Save(_userpath);
            }
            else
            {
                _users = XDocument.Load(_userpath);
            }
        }

        private string _userpath;
        private XDocument _users;

        private static bool PathChecker(string path) => File.Exists(path);

        public bool Add(User user)
        {
            XElement newUser = new XElement("user",
                            new XAttribute("id", user.Id),
                            new XAttribute("username", user.Username),
                            new XAttribute("birthdate", user.BirthDate),
                            new XElement("awards", user.Awards.Select(award => new XElement("award", new XAttribute("id", award)))));
            _users.Root.Add(newUser);
            _users.Root.Save(_userpath);
            return true;
        }

        public bool Delete(int id)
        {
            _users.Root.Descendants("user").Where(x => x.Attribute("id").Value == id.ToString()).Remove();
            _users.Save(_userpath);
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Root.Descendants("user")
                .Select(u => new User(int.Parse(u.Attribute("id").Value), u.Attribute("username").Value, DateTime.Parse(u.Attribute("birthdate").Value), u.Element("awards").Attributes().Select(x => int.Parse(x.Value))));
        }

        public User Get(int id)
        {
            return _users.Root.Descendants("user").Where(u => u.Attribute("id").Value == id.ToString())
                .Select(u => new User(int.Parse(u.Attribute("id").Value), u.Attribute("username").Value, DateTime.Parse(u.Attribute("birthdate").Value), u.Element("awards").Elements("award").Select(x => int.Parse(u.Attribute("id").Value))))
                .FirstOrDefault();
        }

        public bool Rescind(int userId, int awardId)
        {
            _users.Root.Descendants("user").Where(u => u.Attribute("id").Value == userId.ToString()).FirstOrDefault().Element("awards").Descendants().Where(a => a.Attribute("id").Value == awardId.ToString()).Remove();
            _users.Save(_userpath);
            return true;
        }

        public bool Grant(int userId, int awardId)
        {
            _users.Root.Descendants("user").Where(u => u.Attribute("id").Value == userId.ToString()).FirstOrDefault().Element("awards").Add(new XElement("award", new XAttribute("id", awardId)));
            _users.Save(_userpath);
            return true;
        }

        public bool Edit(User user)
        {
            var userId = user.Id;
            var thisUser = _users.Root.Descendants("user").Where(u => u.Attribute("id").Value == userId.ToString()).FirstOrDefault();
            thisUser.Attribute("username").SetValue(user.Username);
            thisUser.Attribute("birthdate").SetValue(user.BirthDate);
            _users.Save(_userpath);
            return true;
        }

        public bool AddImage(int userId, int? imageId)
        {
            throw new NotImplementedException();
        }
    }
}