using Autofac;
using Autofac.Extras.DynamicProxy;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Hangfire.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nw.LiveBackgroundManage.HangfireService.Filter;
using Nw.LiveBackgroundManage.HangfireService.Interface;
using Nw.LiveBackgroundManage.HangfireService.Service;
using Nw.LiveBackgroundManagement.Business.Interface;
using Nw.LiveBackgroundManagement.Business.Interface.AopExtension;
using Nw.LiveBackgroundManagement.Business.Interface.Hangfire;
using Nw.LiveBackgroundManagement.Business.Service.Hangfire;
using Nw.LiveBackgroundManagement.Business.Services;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.Common.WechatPayCore;
using Nw.LiveBackgroundManagement.DataAccessEFCore;
using Nw.LiveReceptionManage.WebApi.EventHandler;
using RabbitMQ.Client;
using RedisHelper.Interface;
using RedisHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManage.WebApi.AotoFacConfig
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //服务层
            #region 业务逻辑

            builder.RegisterType<CSRoomService>().As<ICSRoomService>();
            builder.RegisterType<CSUserservice>().As<ICSUserservice>();

            //添加AOP实现
            builder.RegisterType(typeof(CustomAutofacStatisticsAop));
            builder.RegisterType<CSStatisticsService>().As<ICSStatisticsService>()
                //使用AOP
                .EnableClassInterceptors()
                ;

            #endregion

            //微信支付相关
            builder.AddWechatPay();


            //redis
            #region redis配置

            builder.RegisterType<RedisConfigureHelper>().As<IRedisConfigureHelper>();
            builder.RegisterType<DefaultRedisPersistentConnection>().As<IRedisPersistentConnection>();
            builder.RegisterType<RedisHashService>();
            builder.RegisterType<RedisListService>();
            builder.RegisterType<RedisStringService>();
            builder.RegisterType<RedisZSetService>();

            #endregion

            //Hangfire自定义的用户服务
            #region Hangfire

            builder.RegisterType<CustomAuthorizationFilter>().As<IDashboardAuthorizationFilter>();
            builder.RegisterType<HangfireUserService>().As<IHangfireUserService>();
            builder.RegisterType<TimedTaskService>().As<ITimedTaskService>();
            #endregion


            //EventBusRabbitMQ
            #region EventBusRabbitMQ

            builder.RegisterType<InMemoryEventBusSubscriptionsManager>().As<IEventBusSubscriptionsManager>().SingleInstance();

            builder.Register<IRabbitMQPersistentConnection>(context =>
            {
                RabbitMQConfigure rabbitMQConfigure = context.Resolve<IOptions<RabbitMQConfigure>>().Value;

                var logger = context.Resolve<ILogger<DefaultRabbitMQPersistentConnection>>();

                IConnectionFactory connectionFactory = new ConnectionFactory
                {
                    HostName = rabbitMQConfigure.HostName,
                    VirtualHost = rabbitMQConfigure.VirtualHost,
                    UserName = rabbitMQConfigure.UserName,
                    Password = rabbitMQConfigure.Password,
                    DispatchConsumersAsync = true
                };
                return new DefaultRabbitMQPersistentConnection(
                    connectionFactory,
                    logger,
                    rabbitMQConfigure.RetryCount
                    );
            }).SingleInstance();

            builder.Register<IEventBus>(context =>
            {
                RabbitMQConfigure rabbitMQConfigure = context.Resolve<IOptions<RabbitMQConfigure>>().Value;

                IRabbitMQPersistentConnection rabbitMQPersistent= context.Resolve<IRabbitMQPersistentConnection>();

                var logger = context.Resolve<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();

                var aotofacScope = context.Resolve<ILifetimeScope>();

                var subscriptionManger = context.Resolve<IEventBusSubscriptionsManager>();

                return new EventBusRabbitMQ.EventBusRabbitMQ(
                    rabbitMQPersistent,
                    logger,
                    aotofacScope,
                    subscriptionManger,
                    rabbitMQConfigure.SubscriptionClientName,
                    rabbitMQConfigure.RetryCount
                    );
            }).SingleInstance();

            builder.RegisterType<OrderStartedIntegrationEventHandler>();
            builder.RegisterType<ProductPriceChangedIntegrationEventHandler>();

            #endregion
        }
    }
}
