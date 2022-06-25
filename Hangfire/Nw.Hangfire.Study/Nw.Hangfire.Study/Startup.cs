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

            #region Hangfire�������ݳ־û���ʹ��Mysql

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

                            #region MySqlStorageOptions����˵��
                        //TransactionIsolationLevel - ������뼶��Ĭ��Ϊ���ύ����
                        //QueuePollInterval - ��ҵ������ѯ�����Ĭ��ֵΪ 15 �롣
                        //JobExpirationCheckInterval - ��ҵ���ڼ������������ڼ�¼����Ĭ��ֵΪ 1 С  ʱ��
                        //CountersAggregateInterval - �ۺϼ������ļ����Ĭ��Ϊ 5 ���ӡ�
                        //PrepareSchemaIfNecessary - �������Ϊtrue�����ᴴ�����ݿ��Ĭ��Ϊtrue��
                        //DashboardJobListLimit - �Ǳ����ҵ�б����ơ�Ĭ��ֵΪ 50000��
                        //TransactionTimeout - ���׳�ʱ��Ĭ��ֵΪ 1 ���ӡ�
                        //TablesPrefix - ���ݿ��б��ǰ׺��Ĭ��Ϊ��

                            #endregion
                        )
                    )
                    .UseHangfireHttpJob()
                    .UseLog4NetLogProvider()//������־
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
