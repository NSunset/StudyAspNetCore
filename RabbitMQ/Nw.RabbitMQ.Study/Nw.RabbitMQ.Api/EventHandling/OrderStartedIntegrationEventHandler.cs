using EventBus.Interface;
using Microsoft.Extensions.Logging;
using Nw.RabbitMQ.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Api.EventHandling
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly ILogger<OrderStartedIntegrationEventHandler> _logger;

        public OrderStartedIntegrationEventHandler(
            ILogger<OrderStartedIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderStartedIntegrationEvent @event)
        {
            _logger.LogInformation("----- 处理集成事件：{AppName} 处的 {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, Startup.AppName, @event);

            _logger.LogInformation($"----- OrderStartedIntegrationEventHandler - 开始订单{@event.UserId}");
        }
    }
}
