using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    public class MySqlHelp : IDbHelp
    {
        public void Add<T>(T t)
        {
            Console.WriteLine("MySql添加数据");
        }

        public void Delete<T>(T t)
        {
            Console.WriteLine("MySql删除数据");
        }

        public void Find(int id)
        {
            Console.WriteLine("MySql查询数据");
        }

        public void Update<T>(T t)
        {
            Console.WriteLine("MySql修改数据");
        }
    }
}
