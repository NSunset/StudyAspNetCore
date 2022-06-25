using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Reflection
{
    public class Singleton
    {
        private static Singleton _singleton = null;
        private Singleton()
        {
            Console.WriteLine($"{nameof(Singleton)}被构造");
            
        }

        static Singleton()
        {
            _singleton = new Singleton();
        }

        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <returns></returns>
        public static Singleton GetSingleton()
        {
            return _singleton;
        }
    }
}
