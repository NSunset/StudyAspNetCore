using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    public class Student : ISubject
    {
        public void Add()
        {
            Console.WriteLine("用户操作相关逻辑");
        }

        public void Find(int id)
        {
            Console.WriteLine("用户查询相关逻辑");
        }
    }
}
