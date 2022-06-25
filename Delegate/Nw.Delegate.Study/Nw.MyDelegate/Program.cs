using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nw.MyDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            //观察者实现方案
            {
                ////鱼漂
                //ISubscription subscription = new FishFloat();

                ////钓者
                //ISubscription subscription1 = new Angler();

                ////面向对象实现观察者
                //{
                //    //Fish.AddSubscription(subscription);
                //    //Fish.AddSubscription(subscription1);

                //}

                ////委托实现
                //{
                //    //Fish.SubscriptionHandler += subscription.Operate;
                //    //Fish.SubscriptionHandler += subscription1.Operate;

                //    //委托在声明类外部可以随意执行，安全性差，推荐使用事件
                //    //Fish.SubscriptionHandler.Invoke();
                //    //Fish.SubscriptionEventHandler.Invoke();
                //}

                ////事件实现
                //{
                //    Fish.SubscriptionEventHandler += subscription.Operate;
                //    Fish.SubscriptionEventHandler += subscription1.Operate;
                //}

                //Fish.Bite();

            }


            //AOP实现案例
            {
                User user = new User();
                Type type = user.GetType();

                MethodInfo show = type.GetMethod(nameof(User.Show));

                //执行Show方法
                //show.Invoke(user, null);

                //执行Show方法放入委托
                Action action = () =>
                {
                    //执行Show方法
                    show.Invoke(user, null);
                };

                if (show.IsDefined(typeof(BaseOperateAttribute), true))
                {
                    IEnumerable<BaseOperateAttribute> baseOperateAttributes = show.GetCustomAttributes<BaseOperateAttribute>();

                    foreach (BaseOperateAttribute item in baseOperateAttributes)
                    {
                        action = item.Operate(action);
                    }
                }

                action.Invoke();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Func委托是协变；Action委托是逆变
        /// </summary>

        public static void Show()
        {
            //func协变out。因为协变规定泛型T只能作为返回值，无法作为参数
            //所以规避了安全问题
            Func<User> func2 = new Func<UserChild>(() => { return new UserChild(); });



            //action逆变in。因为逆变规定泛型T只能作为参数，无法作为返回值
            Action<User> action = (User u) =>
            {
            };

            //左边是泛型子委托，右边是泛型父委托,通过逆变能直接赋值
            Action<UserChild> action1 = action;

            //调用时传递的也是子对象
            action1.Invoke(new UserChild());

        }
    }



}
