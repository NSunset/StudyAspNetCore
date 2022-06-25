using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyDelegate
{
    /// <summary>
    /// 鱼:钓鱼时，鱼应该是发布者。当鱼咬钩后，鱼漂和钓者做出相应的反应
    /// </summary>
    public class Fish
    {

        #region 面向对象方式实现，发布订阅

        private static List<ISubscription> subscriptions = null;

        /// <summary>
        /// 添加订阅者
        /// </summary>
        public static void AddSubscription(ISubscription subscription)
        {
            if (subscriptions == null)
            {
                subscriptions = new List<ISubscription>();
            }
            subscriptions.Add(subscription);
        }


        #endregion


        /// <summary>
        /// 委托实现发布订阅
        /// </summary>
        public static Action SubscriptionHandler;


        /// <summary>
        /// 事件实现发布订阅
        /// </summary>
        public static event Action SubscriptionEventHandler;

        /// <summary>
        /// 鱼咬钩时
        /// </summary>
        public static void Bite()
        {
            Console.WriteLine("鱼咬钩了");

            //面向对象方式实现观察者模式
            {
                if (subscriptions != null)
                {
                    foreach (var item in subscriptions)
                    {
                        item.Operate();
                    }
                }
            }

            //委托实现观察者模式
            {
                SubscriptionHandler?.Invoke();
            }

            //事件实现观察者模式
            {
                SubscriptionEventHandler?.Invoke();
            }
        }
    }
}
