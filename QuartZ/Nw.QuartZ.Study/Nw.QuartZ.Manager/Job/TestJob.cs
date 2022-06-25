using log4net;
using Nw.Common;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nw.QuartZ.Manager
{
    public class TestJob : BaseJob
    {
        public override async Task Execute(IJobExecutionContext context)
        {
            LogHelper.Info("测试日志");
            await Task.Run(() =>
            {
                try
                {
                    throw new Exception("报错了");
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex.Message);
                }
                //Thread.Sleep(10_000);
                Console.WriteLine($"这是每3秒执行一次的任务：{DateTime.Now}");
                

            });
        }
    }
}
