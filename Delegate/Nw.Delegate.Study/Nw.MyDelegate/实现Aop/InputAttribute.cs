using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyDelegate
{
    /// <summary>
    /// 输出传入参数
    /// </summary>
    public class InputAttribute : BaseOperateAttribute
    {
        /// <summary>
        /// 这里在操作前输出传入参数
        /// </summary>
        /// <param name="operate"></param>
        /// <returns></returns>
        public override Action Operate(Action operate)
        {
            Action action = () =>
            {
                Console.WriteLine("在执行操作前输出传入参数");
                operate.Invoke();
            };
            return action;
        }
    }
}
