using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Nw.EFCore.DbFirst.Context;
using Nw.EFCore.DbFirst.Models;
using System.Collections.Generic;
using System.Linq;

namespace Nw.Test.Repository
{
    /// <summary>
    /// 数据库新加表，重新反向生成数据实体，去掉DbContext配置。生成新的DbContext,指定生成对应的数据表名，就能添加新的数据实体。吧新加的DbContext里面的属性复制到之前的DbContext然后删除就行了
    /// </summary>
    public class DbFirstTest
    {
        private DbContextOptions<MyDemoContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MyDemoContext>()
                .UseMySql(
                "server=192.168.157.128;database=mydemo;user id=root;password=root;charset=utf8", 
                new MySqlServerVersion(
                    new System.Version(5, 7, 37)
                    )
                ).Options;
        }

        [Test]
        public void Test1()
        {
            List<TbUser> users = null;
            using (MyDemoContext context = new MyDemoContext(options))
            {
                users = context.TbUser.AsNoTracking().ToList();
            }

            Assert.IsTrue(users.Count > 0);
        }

        [Test]
        public void Test2()
        {
            List<TbAddtable> addtables = null;
            using (MyDemoContext context = new MyDemoContext(options))
            {
                addtables = context.TbAddtable.AsNoTracking().ToList();
            }

            Assert.IsTrue(addtables.Count > 0);
        }
    }
}