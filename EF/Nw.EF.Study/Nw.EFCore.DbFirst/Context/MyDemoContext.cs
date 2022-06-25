using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Nw.EFCore.DbFirst.Models;

#nullable disable

namespace Nw.EFCore.DbFirst.Context
{
    /// <summary>
    /// 看官方文档https://docs.microsoft.com/zh-cn/ef/core/cli/powershell
    /// 根据控制台链接数据库生成数据实体和DbContext
    /// </summary>
    public partial class MyDemoContext : DbContext
    {
        public MyDemoContext(DbContextOptions<MyDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbPeople> TbPeople { get; set; }
        public virtual DbSet<TbUser> TbUser { get; set; }

        public virtual DbSet<TbAddtable> TbAddtable { get; set; }

    }
}
