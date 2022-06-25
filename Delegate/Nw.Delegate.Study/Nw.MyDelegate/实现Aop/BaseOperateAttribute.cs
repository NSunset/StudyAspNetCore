using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyDelegate
{
    /// <summary>
    /// 额外功能父类特性，所有额外功能特性都要继承这里
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseOperateAttribute : Attribute
    {
        /// <summary>
        /// 传入的委托是操作。在实现方法中可以扩展委托操作
        /// 把扩展后的操作在包装成委托来返回。实现嵌套
        /// </summary>
        /// <param name=""></param>
        public abstract Action Operate(Action operate);
    }
}
