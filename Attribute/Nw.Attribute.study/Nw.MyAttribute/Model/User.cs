using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyAttribute
{
    /// <summary>
    /// 在使用特性时，使用中括号包裹起来，可以省略后缀Attribute
    /// 因为属性AllowMultiple指定可以重复标记，所以这里可以写多个
    /// </summary>
    [MyCustom]
    [MyCustomAttribute]
    [CustomChild]
    [CustomChildAttribute]
    public class User
    {
        /// <summary>
        /// 由于AttributeTargets参数指定只能标记Class,所以这里标记报错
        /// </summary>
        //[MyCustom]
        //[CustomChild]
        public User()
        {

        }

        /// <summary>
        /// 由于AttributeTargets参数指定只能标记Class,所以这里标记报错
        /// </summary>
        //[MyCustom]
        //[CustomChild]
        public int Name { get; set; }

        /// <summary>
        /// 由于AttributeTargets参数指定只能标记Class,所以这里标记报错
        /// </summary>
        //[MyCustom]
        //[CustomChild]
        public int id;

        /// <summary>
        /// 由于AttributeTargets参数指定只能标记Class,所以这里标记报错
        /// </summary>
        //[MyCustom]
        //[CustomChild]
        public void Show()
        {

        }
    }
}
