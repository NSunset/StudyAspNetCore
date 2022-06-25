using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string SmsVerificationCode { get; set; }

        public string GetUserVerifyVodeKey
        {
            get
            {
                return $"user:verify:code:{Mobile}";
            }
        }

        public static string GetVerifyVodeKey(string phone)
        {
            return $"user:verify:code:{phone}";
        }

        /// <summary>
        /// 加盐
        /// </summary>
        public static string PwdAddsalt(string pwd)
        {
            return $"Nw_{pwd}";
        }
    }
}
