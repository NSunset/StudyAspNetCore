using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyDelegate
{
    /// <summary>
    /// 写日志
    /// </summary>
    public class WriteLogAttribute : BaseOperateAttribute
    {
        /// <summary>
        /// 这里是写日志
        /// 通过委托来控制在哪里写日志
        /// </summary>
        public override Action Operate(Action operate)
        {
            Action action = () =>
            {
                operate.Invoke();
                Console.WriteLine("在方法执行完之后写日志");
            };
            return action;
        }
    }
}
