using System.Configuration;
using UserBase.DALContracts;
using UserBase.DbDAL;
using UserBase.FileDAL;
using UserBase.MemoryDAL;

namespace UserBase.UserLogic
{
    public static class DaoLinker
    {
        static DaoLinker()
        {
            dalType = ConfigurationManager.AppSettings["DalType"];

            switch (dalType.ToLower())
            {
                case "memory":
                    userDao = new MemoryUserDao();
                    awardDao = new MemoryAwardDao();
                    break;

                case "files":
                    userDao = new FileUserDao();
                    awardDao = new FileAwardDao();
                    break;

                case "database":
                    userDao = new DbUserDao();
                    awardDao = new DbAwardDao();
                    imageDao = new DbImageDao();
                    loginDao = new DbLoginDao();
                    break;

                default:
                    throw new ConfigurationErrorsException($"Invalid dalType: {dalType}");
            }
        }

        private static string dalType;

        public static IUserDao userDao { get; set; }

        public static IAwardDao awardDao { get; set; }

        public static IImageDao imageDao { get; set; }

        public static ILoginDao loginDao { get; set; }
    }
}