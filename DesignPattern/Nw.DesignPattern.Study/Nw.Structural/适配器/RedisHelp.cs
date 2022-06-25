using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    public class RedisHelp
    {
        public void RedisAdd<T>(T t)
        {
            Console.WriteLine("Redis添加数据");
        }

        public void Update<T>(T t)
        {
            Console.WriteLine("Redis修改数据");
        }

        public void Delete<T>(T t)
        {
            Console.WriteLine("Redis删除数据");
        }

        public void Find(int id)
        {
            Console.WriteLine("Redis查找数据");
        }
    }
}
