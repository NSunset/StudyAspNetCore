using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Create
{
    public class User
    {
        private static User _user = null;

        /// <summary>
        /// 饿加载
        /// 通过静态字段实现单例。多线程不影响
        /// </summary>
        //private static User _user = new User();


        private static object _objLock = new object();
        private User()
        {
            Console.WriteLine($"{nameof(User)}被实例化");
        }

        //饿加载
        //static User()
        //{
        //    _user = new User();
        //}

        /// <summary>
        /// 使用双判断加锁实现单例，反多线程
        /// </summary>
        /// <returns></returns>
        public static User CreateExample()
        {
            //使用锁，懒加载
            if (_user == null)
            {
                lock (_objLock)//反多线程
                {
                    if (_user == null)
                    {
                        _user = new User();
                    }
                }
            }
            return _user;
        }

        /// <summary>
        /// 通过静态构造函数实现单例。多线程不影响
        /// </summary>
        /// <returns></returns>
        public static User Show()
        {
            return _user;
        }
    }
}
