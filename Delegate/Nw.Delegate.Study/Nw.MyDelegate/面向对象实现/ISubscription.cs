using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyDelegate
{
    /// <summary>
    /// 订阅者接口，所有的订阅者都要实现这个接口
    /// </summary>
    public interface ISubscription
    {
        void Operate();
    }
}
