using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Nw.LiveBackgroundManagement.Models.EventMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.WebApi.EventHandler
{
    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        private readonly ILogger<ProductPriceChangedIntegrationEventHandler> _logger;

        public ProductPriceChangedIntegrationEventHandler(
            ILogger<ProductPriceChangedIntegrationEventHandler> logger
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            _logger.LogInformation($"----- 处理集成事件: ({@event})");
            Console.WriteLine($"商品消费处理消息：{@event}");
            await Task.Yield();
        }
    }
}
