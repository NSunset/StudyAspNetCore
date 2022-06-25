using Microsoft.EntityFrameworkCore;
using Nw.EFCore.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.EFCore.CodeFirst.Context
{
    /// <summary>
    /// 看官方文档https://docs.microsoft.com/zh-cn/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    /// 看从设计时工厂，通过自实现创建DbContext实例来链接数据库。在通过控制台命令迁移生成数据库。看迁移命令
    /// </summary>
    public class MyDemo1TestDbContext : DbContext
    {
        public MyDemo1TestDbContext(DbContextOptions<MyDemo1TestDbContext> options) : base(options)
        {

        }

        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Addtable> Addtable { get; set; }
    }
}
