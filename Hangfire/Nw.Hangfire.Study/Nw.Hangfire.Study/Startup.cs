using Hangfire;
using Hangfire.HttpJob;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Nw.Hangfire.Study
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nw.Hangfire.Study", Version = "v1" });
            });

            #region Hangfire配置数据持久化，使用Mysql

            services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseStorage(
                        new MySqlStorage(
                            Configuration.GetConnectionString("HangfireConnection"),
                            new MySqlStorageOptions
                            {
                                TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                                QueuePollInterval = TimeSpan.FromSeconds(15),
                                JobExpirationCheckInterval = TimeSpan.FromHours(1),
                                CountersAggregateInterval = TimeSpan.FromMinutes(5),
                                PrepareSchemaIfNecessary = true,
                                DashboardJobListLimit = 50000,
                                TransactionTimeout = TimeSpan.FromMinutes(1),
                                TablesPrefix = "Hangfire"
                            }

                            #region MySqlStorageOptions参数说明
                        //TransactionIsolationLevel - 事务隔离级别。默认为已提交读。
                        //QueuePollInterval - 作业队列轮询间隔。默认值为 15 秒。
                        //JobExpirationCheckInterval - 作业过期检查间隔（管理过期记录）。默认值为 1 小  时。
                        //CountersAggregateInterval - 聚合计数器的间隔。默认为 5 分钟。
                        //PrepareSchemaIfNecessary - 如果设置为true，它会创建数据库表。默认为true。
                        //DashboardJobListLimit - 仪表板作业列表限制。默认值为 50000。
                        //TransactionTimeout - 交易超时。默认值为 1 分钟。
                        //TablesPrefix - 数据库中表的前缀。默认为无

                            #endregion
                        )
                    )
                    .UseHangfireHttpJob()
                    .UseLog4NetLogProvider()//配置日志
                                            //.UseActivator(new ContainerJobActivator())
            );
            services.AddHangfireServer();

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nw.Hangfire.Study v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions
                {
                    Authorization = new[] 
                    {
                        new MyAuthorizationFilter()
                    }
                });

            });
        }
    }
}
