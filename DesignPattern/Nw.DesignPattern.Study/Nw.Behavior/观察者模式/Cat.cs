using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Behavior
{
    /// <summary>
    /// 发布方，提供订阅操作
    /// </summary>
    public class Cat
    {
        /// <summary>
        /// 订阅者订阅操作
        /// </summary>
        public event Action SubscribeEventHandler = null;

        public void Miao()
        {
            Console.WriteLine("猫叫了");
            SubscribeEventHandler?.Invoke();
        }
    }
}
