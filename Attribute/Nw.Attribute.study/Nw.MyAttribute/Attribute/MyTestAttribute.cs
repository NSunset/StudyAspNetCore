using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyAttribute
{
    /// <summary>
    /// 这里表示MyTestAttribute特性能标记所有元素上。不能重复标记，当前特性不能继承
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class MyTestAttribute : Attribute
    {
        public int Id { get; set; }
    }

    /// <summary>
    /// 这里表示MyTestAttribute特性能标记所有元素上。不能重复标记(AllowMultiple默认false)
    /// 当前特性可以继承(Inherited默认true，可以继承)
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class MyTest1Attribute : Attribute
    {
        public int Id { get; set; }
    }

    [MyTestAttribute(Id = 123)]
    [MyTest1Attribute(Id =456)]
    public class Usera
    {

    }

    public class Userb : Usera
    {

    }
}
