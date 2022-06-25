using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MySocket
{
    /// <summary>
    /// 命令过滤器
    /// </summary>
    public class LogTimeCommandFilter : CommandFilterAttribute
    {
        /// <summary>
        /// 命令执行后被调用
        /// </summary>
        /// <param name="commandContext"></param>
        public override void OnCommandExecuted(CommandExecutingContext commandContext)
        {
            var session = commandContext.Session as TestOneAppSession;

            var startTime = session.Items.GetValue<DateTime>("StartTime");
            var ts = DateTime.Now.Subtract(startTime);

            if (ts.TotalSeconds > 5)
            {
                Console.WriteLine($"{commandContext.CurrentCommand.Name}执行时间：{ts.ToString()} 秒");
            }
            
        }

        /// <summary>
        /// 命令执行前被调用
        /// </summary>
        /// <param name="commandContext"></param>
        public override void OnCommandExecuting(CommandExecutingContext commandContext)
        {
            commandContext.Session.Items["StartTime"] = DateTime.Now;
            var session = commandContext.Session as TestOneAppSession;
            //没有登录就取消执行命令
            if (session.UserId == 0)
                commandContext.Cancel = true;
        }
    }
}
