using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBase.WebUI.Models
{
    public class AwardLinker
    {
        public static UserLogic.AwardLogic Instance { get; }

        static AwardLinker()
        {
            Instance = new UserLogic.AwardLogic();
        }
    }
}
