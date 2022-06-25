using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Generic
{
    /// <summary>
    /// 泛型缓存
    /// 根据传入类型的不同创建不同的类，每个类都会执行静态构造函数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CacheTest<T>
    {
        private static Dictionary<Type, object> _date = null;
        

        static CacheTest()
        {
            Console.WriteLine("静态构造函数初始化");
            _date = new Dictionary<Type, object>();
        }

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T">具体类型</typeparam>
        /// <returns></returns>
        public static object GetCache()
        {
            Type type = typeof(T);
            if (!_date.ContainsKey(type))
            {
                _date[type] = type.FullName;
            }
            return _date[type];
        }
    }
}
