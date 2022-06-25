using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Generic.Interface
{
    public interface ICustomIn<in T>
    {
        //T GetValue();

        void SetValue(T t);
    }
}
