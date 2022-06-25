using Nw.LiveBackgroundManage.HangfireService.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManage.HangfireService.Interface
{
    public interface ITimedTaskService
    {
        /// <summary>
        ///  执行定时任务--同步字段数据
        /// </summary>
        //[DisableMultipleQueuedItemsFilter]
        public void SynchronizeFieldData();
    }
}
