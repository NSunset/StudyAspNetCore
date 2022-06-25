using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Nw.LiveBackgroundManagement.Models.EventMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.WebApi.EventHandler
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly ILogger<OrderStartedIntegrationEventHandler> _logger;

        public OrderStartedIntegrationEventHandler(
            ILogger<OrderStartedIntegrationEventHandler> logger
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderStartedIntegrationEvent @event)
        {
            _logger.LogInformation($"----- 处理集成事件: ({@event})");
            Console.WriteLine($"订单消费处理消息：{@event}");
            await Task.Yield();
        }
    }
}
