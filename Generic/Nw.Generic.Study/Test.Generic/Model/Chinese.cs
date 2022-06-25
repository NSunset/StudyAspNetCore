using System;
using System.Collections.Generic;
using System.Text;
using Test.Generic.Interface;

namespace Test.Generic.Model
{
    public class Chinese : IPeople
    {
        public virtual void Speak()
        {
            Console.WriteLine("说中文");
        }
    }
}
