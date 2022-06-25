using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.AOP.StaticProxy
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseDecoratorAttribute : Attribute
    {
        /// <summary>
        /// 需要扩展的逻辑，传入委托，在委托执行前后写入扩展逻辑。在包装一个新的委托返回
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Action Expand(Action action);

        /// <summary>
        /// 需要扩展的逻辑，传入委托，在委托执行前后写入扩展逻辑。在包装一个新的委托返回
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract Func<T> Expand<T>(Func<T> action);
    }
}
