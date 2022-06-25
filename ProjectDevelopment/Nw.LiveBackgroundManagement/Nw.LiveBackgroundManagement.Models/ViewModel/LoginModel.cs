using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class LoginModel
    {
        public string Name { get; set; } 
        public string Password { get; set; }
        public string ImgCode { get; set; }

        public string Tag { get; set; }

        public static string GetVerifyCodeKey(string tag)
        {
            return $"{tag}_VerifyCode";
        }
    }
}
