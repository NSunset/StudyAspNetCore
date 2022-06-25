using System;
using System.Collections.Generic;
using System.Text;
using Test.Generic.Interface;
using Test.Generic.Model;

namespace Test.Generic
{
    /// <summary>
    /// 泛型约束
    /// </summary>
    public class Constraint
    {
        /// <summary>
        /// 直接强转，如果传入对象没有继承IPeople则报错
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="people"></param>
        public static void Speak<T>(T people)
        {
            IPeople peopleExample = people as IPeople;
            peopleExample.Speak();
        }

        /// <summary>
        /// 接口约束：没有继承IPeople接口的对象无法传入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="people"></param>
        public static void SpeakInterfaceConstraint<T>(T people) where T : IPeople
        {
            people.Speak();
        }

        /// <summary>
        /// 基类约束：没有继承Chinese的对象无法传入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="people"></param>
        public static void SpeakBaseClassConstraint<T>(T people) where T : Chinese
        {
            people.Speak();
        }

        /// <summary>
        /// 无参构造函数约束：没有无参构造函数的对象无法传入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="people"></param>
        public static void NoArgumentConstructorConstraint<T>(T people) where T : new()
        {
            ShanDong peopleExample = people as ShanDong;
            peopleExample.Speak();
        }

        /// <summary>
        /// 值类型约束：只能传入值类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="date"></param>
        public static void StructConstraint<T>(T date) where T : struct
        {

        }

        /// <summary>
        /// 引用类型约束：只能传入引用类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="date"></param>
        public static void ClassConstraint<T>(T date) where T : class
        {

        }

        /// <summary>
        /// 可以支持多个约束，但不能冲突
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public static void Test<T>(T value) where T : Chinese, IPeople
        {

        }


    }
}
