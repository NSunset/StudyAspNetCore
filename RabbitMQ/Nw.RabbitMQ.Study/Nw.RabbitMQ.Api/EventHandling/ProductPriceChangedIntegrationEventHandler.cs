using EventBus.Interface;
using Microsoft.Extensions.Logging;
using Nw.RabbitMQ.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Api.EventHandling
{
    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        private readonly ILogger<ProductPriceChangedIntegrationEventHandler> _logger;

        public ProductPriceChangedIntegrationEventHandler(
            ILogger<ProductPriceChangedIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            _logger.LogInformation("----- 处理集成事件：{AppName} 处的 {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, Startup.AppName, @event);

            await UpdatePriceInBasketItems(@event.ProductId, @event.NewPrice, @event.OldPrice);
        }

        private async Task UpdatePriceInBasketItems(int productId, decimal newPrice, decimal oldPrice)
        {
            _logger.LogInformation("----- ProductPriceChangedIntegrationEventHandler - 为用户更新购物篮中的商品：{BuyerId} ({@Items})", 10, 5);
        }
    }
}
