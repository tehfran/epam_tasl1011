using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserBase.WebUI.Models
{
    public class ImageLinker
    {
        public static UserLogic.ImageLogic Instance { get; }

        static ImageLinker()
        {
            Instance = new UserLogic.ImageLogic();
        }
    }
}