using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Generic.Interface
{
    public interface ICustomOut<out T>
    {
        T GetValue();

        //void SetValue(T t);
    }
}
