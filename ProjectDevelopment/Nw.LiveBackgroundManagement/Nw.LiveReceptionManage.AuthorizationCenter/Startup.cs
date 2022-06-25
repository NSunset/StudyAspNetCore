using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Business.Interface.Automapping;
using Nw.LiveBackgroundManagement.Business.Services;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.Common.WechatPayCore;
using Nw.LiveBackgroundManagement.Common.WechatPayCore.ConfigManager;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using Nw.LiveBackgroundManagement.Models;
using Nw.LiveReceptionManage.AuthorizationCenter.JWTHelper;
using Nw.LiveReceptionManage.AuthorizationCenter.Utility.Filter;
using RedisHelper.Interface;
using RedisHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.AuthorizationCenter
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nw.LiveReceptionManage.AuthorizationCenter", Version = "v1" });
            });

            services.AddControllers(configure =>
            {
                configure.Filters.Add<CustomExceptionFilterAttribute>();
            });

            //AutoMapper映射添加
            services.AddAutoMapper(typeof(ServiceProfile));

            //注册服务
            //services.AddTransient<DbContext, AuthorityDbContext>();
            services.AddDbContextConfigure(Configuration);
            services.AddTransient<IJwtService, RsaJwtService>();
            services.AddTransient<ICSUserservice, CSUserservice>();

            //redis
            #region redis配置

            //redis注册
            services.AddTransient<IRedisConfigureHelper, RedisConfigureHelper>();
            services.AddTransient<IRedisPersistentConnection, DefaultRedisPersistentConnection>();

            services.AddTransient<RedisStringService>();
            services.AddTransient<RedisHashService>();
            services.AddTransient<RedisListService>();
            services.AddTransient<RedisZSetService>();

            #endregion

            //微信支付相关
            services.AddWechatPay();

            //注册配置文件
            services.AddConfig(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nw.LiveReceptionManage.AuthorizationCenter v1"));
            //}

            if (Configuration.GetValue<bool>("SwaggerOpen"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nw.LiveReceptionManage.WebApi v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
