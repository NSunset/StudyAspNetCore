using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nw.LiveBackgroundManage.HangfireService.Filter;
using Nw.LiveBackgroundManage.HangfireService.Interface;
using Nw.LiveBackgroundManagement.Business.Interface.Hangfire;
using Nw.LiveBackgroundManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Nw.LiveBackgroundManage.HangfireService
{
    public static class ConfigureService
    {
        public static IServiceCollection AddHangfireConfigure(
            this IServiceCollection services,
            IConfiguration configure)
        {
            ConnectionStringsConfigure connectionStrings = configure.GetSection(ConnectionStringsConfigure.Key).Get<ConnectionStringsConfigure>();

            services.AddHangfire(configuration =>
            {
                configuration
                //同个任务取消并行执行，期间进来的任务不会等待，会被取消
                .UseFilter(new DisableMultipleQueuedItemsFilter())
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseLog4NetLogProvider()
                .UseStorage(new MySqlStorage(connectionStrings.HangfireDbContext,
                            new MySqlStorageOptions
                            {
                                //TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                                //QueuePollInterval = TimeSpan.FromSeconds(15),
                                //JobExpirationCheckInterval = TimeSpan.FromHours(1),
                                //CountersAggregateInterval = TimeSpan.FromMinutes(5),
                                //PrepareSchemaIfNecessary = true,
                                //DashboardJobListLimit = 50000,
                                //TransactionTimeout = TimeSpan.FromMinutes(1),
                                //TablesPrefix = "Hangfire"

                                #region 配置说明
                                //TransactionIsolationLevel - 事务隔离级别。默认为已提交读。
                                //QueuePollInterval - 作业队列轮询间隔。默认值为 15 秒。
                                //JobExpirationCheckInterval - 作业过期检查间隔（管理过期记录）。默认值为 1 小时。
                                //CountersAggregateInterval - 聚合计数器的间隔。默认为 5 分钟。
                                //PrepareSchemaIfNecessary - 如果设置为true，它会创建数据库表。默认为true。
                                //DashboardJobListLimit - 仪表板作业列表限制。默认值为 50000。
                                //TransactionTimeout - 交易超时。默认值为 1 分钟。
                                //TablesPrefix - 数据库中表的前缀。默认为无
                                #endregion
                            }
                ));

            });
            services.AddHangfireServer();
            return services;
        }


        /// <summary>
        /// 配置Hangfire中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHangfireConfigure(this IApplicationBuilder app)
        {
            Dashboard(app);

            HangfireInit.Init();

            return app;
        }

        /// <summary>
        /// 使用仪表盘
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static IApplicationBuilder Dashboard(IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/custom/hangfire", new DashboardOptions
            {
                Authorization = new[] {
                    app.ApplicationServices.GetService<IDashboardAuthorizationFilter>()
                }
                //只能看，不能操作
                //IsReadOnlyFunc = context => true
            });
            return app;
        }
    }

}
