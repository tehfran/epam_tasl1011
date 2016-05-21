using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBase.BLContracts;
using UserBase.Entities;
using UserBase.DALContracts;

namespace UserBase.UserLogic
{
    public class ImageLogic : IImageLogic
    {
        private int MaxID { get; set; }

        private IUserDao _userDao;
        private IAwardDao _awardDao;
        private IImageDao _imageDao;

        public ImageLogic()
        {
            _userDao = DaoLinker.userDao;
            _awardDao = DaoLinker.awardDao;
            _imageDao = DaoLinker.imageDao;
            MaxID = GetAll().Select(x => x.Id).DefaultIfEmpty().Max();
        }

        public int Add(byte[] content, string type, string source, int sourceId)
        {
            int id = ++MaxID;
            var image = new Image(id, content, type);
            {
                if (source == "user")
                {
                    _imageDao.Add(image);  //This is important to repeat in both branches because you won't be able to add it otherwise
                    _userDao.AddImage(sourceId, id);
                }
                else if (source == "award")
                {
                    _imageDao.Add(image);
                    _awardDao.AddImage(sourceId, id);
                }
                else
                {
                    throw new ArgumentException($"Incorrect source parameter: {source}");
                }
            }
            return id;
        }

        public Image Get(int id) => _imageDao.Get(id);

        public IEnumerable<Image> GetAll() => _imageDao.GetAll();

        public bool Delete(int id) => _imageDao.Delete(id);
    }
}
