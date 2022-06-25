using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Db.Mysql
{
    /// <summary>
    /// 反射测试类
    /// </summary>
    public class ReflectionTest
    {
        #region 构造函数
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public ReflectionTest()
        {
            Console.WriteLine($"{nameof(ReflectionTest)}无参数构造函数被构造");
        }

        /// <summary>
        /// 带一个参数的
        /// </summary>
        /// <param name="id"></param>
        public ReflectionTest(int id)
        {
            Console.WriteLine($"{nameof(ReflectionTest)}参数{nameof(id)}={id}构造函数被构造");
        }

        /// <summary>
        /// 带一个参数的重载
        /// </summary>
        /// <param name="name"></param>
        public ReflectionTest(string name)
        {
            Console.WriteLine($"{nameof(ReflectionTest)}参数{nameof(name)}={name}构造函数被构造");
        }

        /// <summary>
        /// 带两个参数的
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public ReflectionTest(int id, string name)
        {
            Console.WriteLine($"{nameof(ReflectionTest)}参数{nameof(id)}={id};{nameof(name)}={name}构造函数被构造");
        }

        #endregion

        #region 方法

        /// <summary>
        /// 无参方法
        /// </summary>
        public void Show()
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show)}");
        }

        /// <summary>
        /// 无参方法1
        /// </summary>
        public void Show1()
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show1)}");
        }

        /// <summary>
        /// 私有方法
        /// </summary>
        private void Show2(string name)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show2)}-{nameof(name)}={name}");
        }

        /// <summary>
        /// 私有方法重载
        /// </summary>
        private void Show4(string name)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show4)}-{nameof(name)}={name}");
        }

        /// <summary>
        /// 私有方法重载
        /// </summary>
        private void Show4(int id)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show4)}-{nameof(id)}={id}");
        }

        /// <summary>
        /// 静态方法
        /// </summary>
        public static void Show3(string name)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show3)}-{nameof(name)}={name}");
        }

        /// <summary>
        /// 静态方法重载
        /// </summary>
        public static void Show5(string name)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show5)}-{nameof(name)}={name}");
        }

        /// <summary>
        /// 静态方法重载
        /// </summary>
        public static void Show5(int id)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show5)}-{nameof(id)}={id}");
        }

        /// <summary>
        /// 静态私有方法
        /// </summary>
        private static void Show6()
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show6)}");
        }

        /// <summary>
        /// 静态私有方法重载
        /// </summary>
        private static void Show6(int id)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show6)}-{nameof(id)}={id}");
        }

        /// <summary>
        /// 重载一个参数方法
        /// </summary>
        /// <param name="id"></param>
        public void Show(int id)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show)}-{nameof(id)}={id}");
        }

        /// <summary>
        /// 重载一个参数方法
        /// </summary>
        /// <param name="name"></param>
        public void Show(string name)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show)}-{nameof(name)}={name}");
        }

        /// <summary>
        /// 重载两个参数方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Show(int id,string name)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show)}-{nameof(id)}={id}-{nameof(name)}={name}");
        }

        /// <summary>
        /// 重载两个参数方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public void Show(string name,int id)
        {
            Console.WriteLine($"这里是{nameof(ReflectionTest)}-{nameof(Show)}-{nameof(name)}={name}-{nameof(id)}={id}");
        }




        #endregion


    }
}
