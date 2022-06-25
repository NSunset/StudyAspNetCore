using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Nw.EFCore.CodeFirst.Context;
using Nw.EFCore.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Test.Repository
{
    public class CodeFirst
    {
        private DbContextOptions<MyDemo1TestDbContext> options;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MyDemo1TestDbContext>()
                .UseMySql(
                "server=192.168.157.128;database=mydemo1;user id=root;password=root;charset=utf8",
                new MySqlServerVersion(
                    new Version(5,7,37)
                    )
                )
                .Options;
        }

        [Test]
        public void Test1()
        {
            List<User> users = null;
            using (MyDemo1TestDbContext context = new MyDemo1TestDbContext(options))
            {
                users = context.User.AsNoTracking().ToList();
            }

            Assert.IsTrue(users.Count > 0);
        }

        [Test]
        public void Test2()
        {
            List<Addtable> addtables = null;
            using (MyDemo1TestDbContext context = new MyDemo1TestDbContext(options))
            {
                addtables = context.Addtable.AsNoTracking().ToList();
            }

            Assert.IsTrue(addtables.Count > 0);
        }
    }
}
