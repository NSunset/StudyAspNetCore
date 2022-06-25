using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    public class SqlServerHelp : IDbHelp
    {
        public void Add<T>(T t)
        {
            Console.WriteLine("SqlServer添加数据");
        }

        public void Delete<T>(T t)
        {
            Console.WriteLine("SqlServer删除数据");
        }

        public void Find(int id)
        {
            Console.WriteLine("SqlServer查询数据");
        }

        public void Update<T>(T t)
        {
            Console.WriteLine("SqlServer修改数据");
        }
    }
}
