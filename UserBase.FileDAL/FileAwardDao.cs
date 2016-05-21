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
    public class FileAwardDao : IAwardDao
    {
        private string _awardpath;
        private XDocument _awards;

        public FileAwardDao()
        {
            _awardpath = ConfigurationManager.AppSettings["awardpath"];
            if (!PathChecker(_awardpath))
            {
                _awards = new XDocument(new XElement("awards"));
                _awards.Save(_awardpath);
            }
            else
            {
                _awards = XDocument.Load(_awardpath);
            }
        }

        private static bool PathChecker(string path) => File.Exists(path);

        public bool Add(Award award)
        {
            XElement newUser = new XElement("award",
                new XAttribute("id", award.Id),
                new XAttribute("name", award.Name));
            _awards.Root.Add(newUser);
            _awards.Root.Save(_awardpath);
            return true;
        }

        public bool AddImage(int userId, int? imageId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            _awards.Root.Descendants("award").Where(a => a.Attribute("id").Value == id.ToString()).Remove();
            _awards.Save(_awardpath);
            return true;
        }

        public bool Edit(Award award)
        {
            _awards.Root.Descendants("award").Where(a => a.Attribute("id").Value == award.Id.ToString()).FirstOrDefault().Attribute("name").SetValue(award.Name);
            return true;
        }

        public Award Get(int id)
        {
            return _awards.Root.Descendants("award").Where(a => a.Attribute("id").Value == id.ToString()).Select(a => new Award(int.Parse(a.Attribute("id").Value), a.Attribute("name").Value)).FirstOrDefault();
        }

        public IEnumerable<Award> GetAll()
        {
            return _awards.Root.Descendants("award").Select(a => new Award(int.Parse(a.Attribute("id").Value), a.Attribute("name").Value));
        }
    }
}