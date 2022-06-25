using System;
using System.Collections.Generic;
using System.Text;
using Test.Generic.Interface;

namespace Test.Generic.Model
{
    public class CustomIn<T> : ICustomIn<T>
    {
        //public T GetValue()
        //{
        //    return default(T);
        //}

        public void SetValue(T t)
        {
            
        }
    }
}
