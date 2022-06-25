using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nw.LiveBackgroundManagement.Common.JWTHelper;
using Nw.LiveBackgroundManagement.Common.WechatPayCore.ConfigManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common
{
    public static class ServiceAddConfigureExtensions
    {
        public static IServiceCollection AddConfig(
            this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ConnectionStringsConfigure>(config.GetSection(ConnectionStringsConfigure.Key));

            //redis配置
            services.Configure<RedisConfigure>(config.GetSection(RedisConfigure.Key));

            //wx配置
            services.Configure<BasicConfig>(config.GetSection(BasicConfig.WxPayConfigure));

            //配置Jwt
            services.Configure<JwtConfigure>(config.GetSection(JwtConfigure.Key));

            //RabbitMQ配置
            services.Configure<RabbitMQConfigure>(config.GetSection(RabbitMQConfigure.Key));
            return services;
        }
    }
}
