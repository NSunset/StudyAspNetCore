using Nw.Db.Interface;
using System;

namespace Nw.Db.SqlServer
{
    public class SqlServerHelp : IDbHelp
    {
        public void Get()
        {
            Console.WriteLine("SqlServer使用");
        }
    }
}
