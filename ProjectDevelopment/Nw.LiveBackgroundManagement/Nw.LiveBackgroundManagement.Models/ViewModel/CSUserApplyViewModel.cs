using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Models.CSEnum;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class CSUserApplyViewModel
    {
        //public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int CSUserId { get; set; }

        /// <summary>
        /// 状态：1.审批中  2.审批通过   3.审批驳回
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 房间名称
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// 房间类别Id
        /// </summary>
        public int CSRoomTypeId { get; set; }

        /// <summary>
        /// 房间描述
        /// </summary>
        public string Description { get; set; }

        public string StateMessage
        {
            get
            {
                switch (State)
                {
                    case (int)ApprovalStatusEnum.NoApproval:
                        return "无需审批";

                    case (int)ApprovalStatusEnum.UnderApproval:
                        return "审批中";

                    case (int)ApprovalStatusEnum.Approved:
                        return "审批通过";

                    case (int)ApprovalStatusEnum.ApprovalRejected:
                        return "审批驳回";
                    default:
                        throw new Exception("没有这个枚举");

                }
            }
        }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string ApprovalMsg { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }


        /// <summary>
        /// 修改时间
        /// </summary>
        public string StrLastModifyTime
        {
            get
            {
                return this.LastModifyTime?.ToString("yyyy-MM-dd");
            }
        }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// 发起审批时间
        /// </summary>
        public string StrCreateTime
        {

            get
            {
                return this.CreateTime.ToString("yyyy-MM-dd HH:mm ss");
            }
        }

    }
}
