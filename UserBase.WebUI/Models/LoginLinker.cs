using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBase.WebUI.Models
{
    public class LoginLinker
    {
        public static UserLogic.LoginLogic Instance { get; }

        static LoginLinker()
        {
            Instance = new UserLogic.LoginLogic();
        }
    }
}