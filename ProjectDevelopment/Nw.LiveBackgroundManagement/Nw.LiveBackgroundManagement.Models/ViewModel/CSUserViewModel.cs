using System;
using System.Collections.Generic;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class CSUserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string ImgUrl { get; set; }

        public int UserType { get; set; }

        public byte Status { get; set; }

        public int? ApplysState { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public long? QQ { get; set; }

        public string WeChat { get; set; }

        public byte? Sex { get; set; }

        /// <summary>
        /// 播放地址 M3u8 格式
        /// </summary>
        public string M3u8 { get; set; }

        /// <summary>
        /// 播放地址 Flv 格式
        /// </summary>
        public string Flv { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? LastModifyTime { get; set; }

        public int? LastModifyId { get; set; }

        public string LiveWorksId { get; set; }
    }
}
