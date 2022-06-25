using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nw.MyAttribute
{
    /// <summary>
    /// 指定可以加到属性上，并且当需要验证的对象父类属性打上了标记，子类也会验证属性.因为Inherited=true(默认为true,可以继承)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BaseValidateAttribute : Attribute
    {
        public abstract ValidateResult Validate(string propertyName, object value);
    }
}
