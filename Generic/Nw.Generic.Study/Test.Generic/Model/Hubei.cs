using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Generic.Model
{
    public class Hubei : Chinese
    {
        public override void Speak()
        {
            Console.WriteLine("说武汉话");
        }
    }
}
