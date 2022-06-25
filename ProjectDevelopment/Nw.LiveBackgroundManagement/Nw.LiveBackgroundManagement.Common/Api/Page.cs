using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common.Api
{
    public class Page:ApiResult
    {
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///总数量
        /// </summary>
        public int TotalCount { get; set; }
    }
}
