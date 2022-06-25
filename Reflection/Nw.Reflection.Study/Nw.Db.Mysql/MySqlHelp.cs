using Nw.Db.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Db.Mysql
{
    public class MySqlHelp : IDbHelp
    {
        public void Get()
        {
            Console.WriteLine("MySql使用");
        }
    }
}
