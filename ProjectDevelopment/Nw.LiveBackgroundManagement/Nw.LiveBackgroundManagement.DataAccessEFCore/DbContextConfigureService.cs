using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nw.LiveBackgroundManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore
{
    public static class DbContextConfigureService
    {
        /// <summary>
        /// 注入数据连接对象到容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContextConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionStringsConfigure connectionStrings = configuration.GetSection(ConnectionStringsConfigure.Key).Get<ConnectionStringsConfigure>();

            services.AddDbContext<AuthorityDbContext>(builder =>
            {
                builder.UseMySql(
                    connectionStrings.AuthorityDbContext,
                    new MySqlServerVersion(new Version(5, 7, 37))
                    );
                //使用动态代理进行延迟加载
                builder.UseLazyLoadingProxies();
            });

            return services;
        }
    }
}
