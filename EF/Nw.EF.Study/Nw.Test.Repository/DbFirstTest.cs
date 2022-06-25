using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Nw.EFCore.DbFirst.Context;
using Nw.EFCore.DbFirst.Models;
using System.Collections.Generic;
using System.Linq;

namespace Nw.Test.Repository
{
    /// <summary>
    /// ���ݿ��¼ӱ����·�����������ʵ�壬ȥ��DbContext���á������µ�DbContext,ָ�����ɶ�Ӧ�����ݱ�������������µ�����ʵ�塣���¼ӵ�DbContext��������Ը��Ƶ�֮ǰ��DbContextȻ��ɾ��������
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