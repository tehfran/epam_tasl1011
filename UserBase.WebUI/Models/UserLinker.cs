using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBase.WebUI.Models
{
    public class UserLinker
    {
        public static UserLogic.UserLogic Instance { get; }

        static UserLinker()
        {
            Instance = new UserLogic.UserLogic();
        }
    }
}