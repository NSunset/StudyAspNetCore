using EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Nw.LiveBackgroundManagement.Models.EventMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.WebApi.EventHandler
{
    public static class InitSubscribe
    {
        /// <summary>
        /// 初始化具体消息类型和消费实现
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder Subscribe(this IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetService(typeof(IEventBus)) as IEventBus;

            #region 初始化具体消息类型和消费实现

            eventBus.Subscribe<OrderStartedIntegrationEvent, OrderStartedIntegrationEventHandler>();

            eventBus.Subscribe<ProductPriceChangedIntegrationEvent, ProductPriceChangedIntegrationEventHandler>();

            #endregion

            return app;
        }
    }
}
