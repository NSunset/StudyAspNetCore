using EventBus.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nw.RabbitMQ.Api.Models;
using RabbitMQTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRabbitMQController : ControllerBase
    {
        private readonly IEventBus _eventBusRabbitMQ;
        public EventRabbitMQController(IEventBus eventBusRabbitMQ)
        {
            _eventBusRabbitMQ = eventBusRabbitMQ;
        }

        [Route("CreateOrder")]
        [HttpGet]

        public IActionResult CreateOrder(string userid)
        {
            OrderStartedIntegrationEvent @event = new OrderStartedIntegrationEvent(userid);
            _eventBusRabbitMQ.Publish(@event);
            return Ok();
        }

        [Route("AddProduct")]
        [HttpGet]
        public IActionResult AddProduct(int productId,decimal newPrice,decimal oldPrice)
        {
            ProductPriceChangedIntegrationEvent @event = new ProductPriceChangedIntegrationEvent(productId, newPrice, oldPrice);

            _eventBusRabbitMQ.Publish(@event);

            return Ok();
        }
    }
}
