using Hangfire;
using Nw.LiveBackgroundManage.HangfireService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManage.HangfireService
{
    public class HangfireInit
    {
        /// <summary>
        /// 注册后台作业
        /// </summary>
        public static void Init()
        {
            //每分钟同步数据
            RecurringJob.AddOrUpdate<ITimedTaskService>(
                nameof(ITimedTaskService.SynchronizeFieldData),
                x => x.SynchronizeFieldData(),
                Cron.Minutely()
                );
        }
    }
}
