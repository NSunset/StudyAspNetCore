using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.MicroService.Common;
using Order.MicroService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.MicroServie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Route("create")]
        [HttpPost]
        public ApiResponse BookOrder([FromForm] MicroService.Models.Order order)
        {
            if (_orderService.CreateOrder(order) > 0)
            {
                return ApiResponse.OK("下单成功");
            }
            else
            {
                return ApiResponse.Fail("下单失败");
            }
        }
    }
}
