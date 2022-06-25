using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.Common.CacheHelper;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Models.CSEnum;
using RedisHelper.Interface;
using RedisHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nw.LiveBackgroundManagement.Service.Test
{
    public class Tests
    {
        private static ConnectionStringsConfigure connectionStrings = new ConnectionStringsConfigure
        {
            AuthorityDbContext = "server=192.168.157.128;database=LiveBackgroundManagement;uid=root;pwd=root;charset=utf8",
            HangfireDbContext = "server=192.168.157.128;database=LiveBackgroundManagementHangfire;uid=root;pwd=root;charset=utf8;Allow User Variables=True"
        };

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            DbContextOptions<AuthorityDbContext> value = new DbContextOptionsBuilder<AuthorityDbContext>()
                .UseMySql(
                        connectionStrings.HangfireDbContext,
                        new MySqlServerVersion(new Version(5, 7, 37))
                        )
                .Options;

            using (AuthorityDbContext context = new AuthorityDbContext(value))
            {

                List<CSRoomType> cSRoomTypelist = new List<CSRoomType>();
                for (int i = 0; i < 200; i++)
                {
                    cSRoomTypelist.Add(new CSRoomType()
                    {
                        Text = $"房间类别{i + 1}",
                        Description = "Description"
                    });
                }
                context.Set<CSRoomType>().AddRange(cSRoomTypelist);
                context.SaveChanges();

                List<SysMenu> roleList = context.SysMenu.ToList();

                //初始化前台用户数据
                {
                    List<CSUser> csuserlist = new List<CSUser>();
                    for (int i = 0; i < 200; i++)
                    {
                        CSUser cSUser = new CSUser()
                        {
                            Name = $"用户{i}",
                            Password = "123456",
                            UserType = (int)UserTypeEnum.OrdinaryUsers,
                            Status = (int)UserStatusEnum.Normal,
                            Phone = "18672713698",
                            Mobile = "18672713698",
                            Address = "汉阳人信汇",
                            Email = "18672713698@163.com",
                            QQ = 1030499676,
                            WeChat = "MrRichard2020",
                            Sex = 1,
                            LastLoginTime = DateTime.Now,
                            CreateTime = DateTime.Now.AddDays(i),
                            LastModifyTime = DateTime.Now,
                            LastModifyId = 1,
                            // ApplysState = (int)ApprovalStatusEnum.UnderApproval
                        };

                        int x = i % 4;
                        if (x == 0)
                        {
                            cSUser.ApplysState = (int)ApprovalStatusEnum.UnderApproval;
                        }
                        if (x == 1)
                        {
                            cSUser.ApplysState = (int)ApprovalStatusEnum.ApprovalRejected;
                        }
                        if (x == 2)
                        {
                            cSUser.ApplysState = (int)ApprovalStatusEnum.Approved;
                        }
                        if (x == 3)
                        {
                            cSUser.ApplysState = (int)ApprovalStatusEnum.NoApproval;
                        }

                        csuserlist.Add(cSUser);
                    }
                    context.Set<CSUser>().AddRange(csuserlist);
                    context.SaveChanges();
                }

                ///当前用户
                //CSUser cSUser1 = context.Set<CSUser>().OrderByDescending(c => c.CreateTime).FirstOrDefault(a => a.UserType == (int)UserTypeEnum.OrdinaryUsers);
                //ICSUserservice cSUserservice = new CSUserservice(context, null);

                //for (int i = 0; i < 3; i++)
                //{
                //    CSUserApply csUserApply = new CSUserApply()
                //    {
                //        CSUserId = cSUser1.Id,
                //        LastModifyId = 1,
                //        LastModifyTime = DateTime.Now,
                //        CreateTime = DateTime.Now,
                //        State = (int)ApprovalStatusEnum.UnderApproval,
                //        ApprovalMsg = $"审批{ i + 1}"
                //    };
                //    //提交审批
                //    cSUserservice.ApplyToAnchor(csUserApply);
                //}

                ///审批驳回了；
                //cSUserservice.ApprovalRejected(cSUser1.Id, "审批驳回了");

                //CSUserApply csUserApply1 = new CSUserApply()
                //{
                //    CSUserId = cSUser1.Id,
                //    LastModifyId = 1,
                //    LastModifyTime = DateTime.Now,
                //    State = (int)ApprovalStatusEnum.UnderApproval,
                //    CreateTime = DateTime.Now,
                //    ApprovalMsg = null
                //};
                ////提交审批
                //cSUserservice.ApplyToAnchor(csUserApply1);

                /////审批通过；
                ////cSUserservice.ApprovalAnchor(cSUser1.Id, "审批通过了");
                //IEnumerable<CSUser> Anchorlist = cSUserservice.QueyrCSUserList(); 
            }
        }


        [Test]
        public void RedisTest()
        {
            IOptions<RedisConfigure> options = new TestOptions<RedisConfigure>();
            Assert.IsNotNull(options.Value);
            IRedisConfigureHelper redisConfig = new RedisConfigureHelper(options);

            var logger = new Mock<ILogger<DefaultRedisPersistentConnection>>();
            IRedisPersistentConnection redisPersistentConnection = new DefaultRedisPersistentConnection(logger.Object);

            RedisZSetService redisZSetService = new RedisZSetService(redisPersistentConnection, redisConfig);

            int month = DateTime.Now.Month;
            string key = $"{month}_month";

            redisZSetService.SortedSetRemoveRangeByRank(key, 0, -1);

            redisZSetService.SortedSetAdd(key, $"{month}_薇娅", 1000);
            redisZSetService.SortedSetAdd(key, $"{month}_李佳琪", 900);
            redisZSetService.SortedSetAdd(key, $"{month}_Eleven", 800);
            redisZSetService.SortedSetAdd(key, $"{month}_Richard", 700);
            redisZSetService.SortedSetAdd(key, $"{month}_张三", 600);
            redisZSetService.SortedSetAdd(key, $"{month}_李四", 500);
            redisZSetService.SortedSetAdd(key, $"{month}_王五", 400);
            redisZSetService.SortedSetAdd(key, $"{month}_赵六", 300);
            redisZSetService.SortedSetAdd(key, $"{month}_田七", 200);
            redisZSetService.SortedSetAdd(key, $"{month}_abcc", 100);

            var dicMonthdata = redisZSetService.SortedSetRangeByRankWithScores(key, order: StackExchange.Redis.Order.Descending);
            var result = dicMonthdata.Select(item => new { ranking = item.Element.ToString(), popularity = item.Score });


        }

        [Test]
        public void RedisTest1()
        {
            string daykey = CacheKeyConstant.GetDaydataKeyConstant();

            IOptions<RedisConfigure> options = new TestOptions<RedisConfigure>();
            Assert.IsNotNull(options.Value);
            IRedisConfigureHelper redisConfig = new RedisConfigureHelper(options);

            var logger = new Mock<ILogger<DefaultRedisPersistentConnection>>();
            IRedisPersistentConnection redisPersistentConnection = new DefaultRedisPersistentConnection(logger.Object);
            RedisZSetService redisZSetService = new RedisZSetService(redisPersistentConnection, redisConfig);

            try
            {
                var dicDaydata = redisZSetService.SortedSetRangeByRankWithScores(daykey);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}