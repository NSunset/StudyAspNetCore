using Autofac;
using EventBus;
using EventBus.Interface;
using Microsoft.Extensions.Logging;
using Nw.RabbitMQ.Api.EventHandling;
using RabbitMQ.Client;
using RabbitMQTool.Interface;
using RabbitMQTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Api
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region RabbitMQ相关服务

            builder.RegisterType<InMemoryEventBusSubscriptionsManager>().As<IEventBusSubscriptionsManager>().SingleInstance();

            builder.Register<IRabbitMQPersistentConnection>(context =>
            {
                var logger = context.Resolve<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = "192.168.157.128",
                    VirtualHost = "my_vhost",
                    UserName = "admin",
                    Password = "admin",
                    DispatchConsumersAsync = true
                };
                return new DefaultRabbitMQPersistentConnection(logger, factory, 5);

            }).SingleInstance();

            builder.Register<IEventBus>(sp =>
            {
                var rabbitMQPersistentConnection = sp.Resolve<IRabbitMQPersistentConnection>();

                var iLifetimeScope = sp.Resolve<ILifetimeScope>();

                var logger = sp.Resolve<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.Resolve<IEventBusSubscriptionsManager>();

                return new EventBusRabbitMQ(
                    rabbitMQPersistentConnection,
                    logger,
                    iLifetimeScope,
                    eventBusSubcriptionsManager,
                    "TestBasket",
                    5);
            }).SingleInstance();

            builder.RegisterType<ProductPriceChangedIntegrationEventHandler>();
            builder.RegisterType<OrderStartedIntegrationEventHandler>();

            #endregion



        }
    }
}
