using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public byte Status { get; set; }
        public string Phone { get; set; }
        public int UserType { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long? QQ { get; set; }
        public string WeChat { get; set; }
        public byte? Sex { get; set; }
        public int CreateId { get; set; }
        public int? LastModifyId { get; set; }
        public int? CompanyId { get; set; }

        /// <summary>
        /// 审批状态：
        /// </summary>
        public int ApplysState { get; set; }

        /// <summary>
        /// 主播对应的房间
        /// </summary>
        public virtual List<CSUserApply> CSUserApply { get; set; }

    }
}
