using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyDelegate
{
    /// <summary>
    /// 鱼漂
    /// </summary>
    public class FishFloat : ISubscription
    {
        /// <summary>
        /// 这里是根据面向对象思想实现的
        /// </summary>
        public void Operate()
        {
            Sink();
        }

        /// <summary>
        /// 当鱼咬钩时，鱼漂下沉
        /// </summary>
        public void Sink()
        {
            Console.WriteLine("鱼漂下沉");
        }
    }
}
