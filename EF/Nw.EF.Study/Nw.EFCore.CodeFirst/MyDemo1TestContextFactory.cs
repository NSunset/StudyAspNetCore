using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nw.EFCore.CodeFirst.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.EFCore.CodeFirst
{
    /// <summary>
    /// 用于迁移链接数据库
    /// </summary>
    public class MyDemo1TestContextFactory : IDesignTimeDbContextFactory<MyDemo1TestDbContext>
    {
        public MyDemo1TestDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDemo1TestDbContext> builder= new DbContextOptionsBuilder<MyDemo1TestDbContext>();

            builder.UseMySql("server=192.168.157.128;database=mydemo1;user id=root;password=root;charset=utf8", new MySqlServerVersion(new Version(5, 7, 37)));

            return new MyDemo1TestDbContext(builder.Options);
        }
    }
}
