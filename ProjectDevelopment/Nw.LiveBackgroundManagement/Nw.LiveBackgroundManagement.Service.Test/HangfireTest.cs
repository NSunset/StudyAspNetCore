using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Nw.LiveBackgroundManagement.Business.Interface.Hangfire;
using Nw.LiveBackgroundManagement.Business.Service.Hangfire;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.Common.Api;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using Nw.LiveBackgroundManagement.Models.ViewModel.Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nw.LiveBackgroundManagement.Service.Test
{
    public class HangfireTest
    {
        private static ConnectionStringsConfigure connectionStrings = new ConnectionStringsConfigure
        {
            AuthorityDbContext = "server=192.168.157.128;database=LiveBackgroundManagement;uid=root;pwd=root;charset=utf8",
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Register()
        {
            DbContextOptions<AuthorityDbContext> options = new DbContextOptionsBuilder<AuthorityDbContext>()
                .UseMySql(
                        connectionStrings.AuthorityDbContext,
                        new MySqlServerVersion(new Version(5, 7, 37))
                        )
                .Options;

            IHangfireUserService hangfireUserService = new HangfireUserService(new AuthorityDbContext(options));

            HangfireUserViewModel input = new HangfireUserViewModel
            {
                Name = "admin",
                Pwd = "123456"
            };

            ApiResult result= hangfireUserService.Register(input);
            Assert.IsTrue(result.Success);


        }

        [Test]
        public void Login()
        {
            DbContextOptions<AuthorityDbContext> options = new DbContextOptionsBuilder<AuthorityDbContext>()
                .UseMySql(
                        connectionStrings.AuthorityDbContext,
                        new MySqlServerVersion(new Version(5, 7, 37))
                        )
                .Options;

            IHangfireUserService hangfireUserService = new HangfireUserService(new AuthorityDbContext(options));

            HangfireUserViewModel input = new HangfireUserViewModel
            {
                Name = "admin",
                Pwd = "123456"
            };

            ApiResult result = hangfireUserService.Login(input);
            Assert.IsTrue(result.Success);


        }


    }
}