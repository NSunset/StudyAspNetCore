using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.StaticProxy
{
    public class LogDecoratorAttribute : BaseDecoratorAttribute
    {
        /// <summary>
        /// 扩展逻辑是在执行方法前写入日志
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public override Action Expand(Action action)
        {
            Action next = () =>
            {
                Console.WriteLine("在执行业务前写入日志");
                action.Invoke();
            };
            return next;
        }

        /// <summary>
        /// 扩展逻辑是在执行方法前写入日志
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public override Func<T> Expand<T>(Func<T> action)
        {
            Func<T> next = () =>
            {
                Console.WriteLine("在执行业务前写入日志");

                T t = action.Invoke();

                return t;
            };
            return next;
        }
    }
}
