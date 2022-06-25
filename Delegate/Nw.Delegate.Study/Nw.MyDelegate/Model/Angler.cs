using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyDelegate
{
    /// <summary>
    /// 钓者
    /// </summary>
    public class Angler : ISubscription
    {
        /// <summary>
        /// 这里是根据面向对象思想实现的
        /// </summary>
        public void Operate()
        {
            TieRod();
        }

        /// <summary>
        /// 钓着看到鱼咬钩就拉杆
        /// </summary>
        public void TieRod()
        {
            Console.WriteLine("钓鱼的人拉杆");
        }
    }
}
