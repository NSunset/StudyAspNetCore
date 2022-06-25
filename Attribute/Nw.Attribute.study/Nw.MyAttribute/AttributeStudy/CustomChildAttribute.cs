using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyAttribute
{
    /// <summary>
    /// 这里继承了MyCustomAttribute
    /// 当前类如果不加AttributeUsage约束，则继承MyCustomAttribute的AttributeUsage约束
    /// 当前类如果加AttributeUsage约束，则根据AttributeUsage约束来限定,一旦写了AttributeUsage则覆盖父级的约束
    /// </summary>
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method|AttributeTargets.Constructor,AllowMultiple =true)]
    public class CustomChildAttribute : MyCustomAttribute
    {

    }
}
