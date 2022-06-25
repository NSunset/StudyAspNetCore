using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Create
{
    public class Singleton<T> where T : new()
    {
        protected static T t;
        static Singleton()
        {
            t = new T();
        }

        public static T CreateInstance()
        {
            return t;
        }
    }

    public class UserChild
    {
        public UserChild()
        {
            Console.WriteLine($"{nameof(UserChild)}被实例化");
        }
    }
}
