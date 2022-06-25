using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyAttribute
{
    /// <summary>
    /// 自定义特性：特性是一个类，继承或者间接继承Attribute就行了
    /// 
    /// AttributeUsage:这个也是特性，用来给自定义的特性加约束
    ///     AttributeTargets:构造函数里的参数AttributeTargets用来指定自定义的特性能标记在那些地方，这里约束只能标记Class
    ///     AllowMultiple:属性AllowMultiple用来确定是否可以重复标记,默认不可以。这里约束可以重复标记
    ///     Inherited：如果该属性可以由派生类和重写成员继承，则为 true；否则为 false。 默认值为 true。这里约束可以继承
    ///     
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class MyCustomAttribute : Attribute
    {
        /// <summary>
        /// 可以有构造函数
        /// </summary>
        public MyCustomAttribute()
        {

        }
    }
}
