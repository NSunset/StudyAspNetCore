using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Models
{
    /// <summary>
    /// 房间类别
    /// </summary>
    public class CSRoomType
    {
        /// <summary>
        /// 房间类别
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 房间类别描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 房间
        /// </summary>
        public virtual CSRoom CSRoom { get; set; }
    }
}
