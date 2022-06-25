using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.Common.WechatPayCore.ConfigManager;
using Autofac;

namespace Nw.LiveBackgroundManagement.Common.WechatPayCore
{
    public static class WechatRegisterExtend
    {
        public static void AddWechatPay(this IServiceCollection services)
        {
            services.AddTransient<IWxPayConfig, WxPayConfig>();
            services.AddTransient<PayHelper>();
            services.AddTransient<WxPayApi>();
            services.AddTransient<WxPayHttpService>(); 
        }

        public static void AddWechatPay(this ContainerBuilder builder)
        {
            builder.RegisterType<WxPayConfig>().As<IWxPayConfig>();
            builder.RegisterType(typeof(PayHelper));
            builder.RegisterType(typeof(WxPayApi));
            builder.RegisterType(typeof(WxPayHttpService));
        }
    }
}
