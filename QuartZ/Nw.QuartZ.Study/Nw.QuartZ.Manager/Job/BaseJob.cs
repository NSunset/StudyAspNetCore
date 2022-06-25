using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nw.QuartZ.Manager
{
    [DisallowConcurrentExecution]//保证任务不会重叠执行
    public abstract class BaseJob : IJob
    {
        /// <summary>
        /// 执行定时任务的地方
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        public abstract Task Execute(IJobExecutionContext context);

    }
}
