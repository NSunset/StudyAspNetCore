using Microsoft.Extensions.Logging;
using Order.MicroService.IService;
using Order.MicroService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.MicroService.Service
{
    /// <summary>
    /// 订单微服务实现
    /// </summary>
    public class OrderServiceImpl : IOrderService
    {
        private ILogger<OrderServiceImpl> _logger;
        private OrderDbContext _dBContext;
        private IStorageService _storageService;
        private IAccountService _accoutnService;

        public OrderServiceImpl(OrderDbContext dBContext,
            ILogger<OrderServiceImpl> logger,
            IStorageService storageService,
            IAccountService accountService)
        {
            _dBContext = dBContext;
            _logger = logger;
            _storageService = storageService;
            _accoutnService = accountService;
        }

        public long CreateOrder(Models.Order order)
        {
            try
            {
                _logger.LogInformation("------->下单开始");
                //本应用创建订单
                AddOrder(order);

                //远程调用库存服务扣减库存
                _logger.LogInformation("------->order-service中扣减库存开始");
                _storageService.DecreaseStorage(order.ProductId, order.Count);
                _logger.LogInformation("------->order-service中扣减库存结束");

                //远程调用账户服务扣减余额
                _logger.LogInformation("------->order-service中扣减余额开始");
                _accoutnService.DeductMoney(order.UserId, order.Money);
                _logger.LogInformation("------->order-service中扣减余额结束");

                //修改订单状态为已完成
                _logger.LogInformation("------->order-service中修改订单状态开始");
                UpdateOrder(order, 1);
                _logger.LogInformation("------->order-service中修改订单状态结束");

                _logger.LogInformation("------->下单结束");
            }
            catch (System.Exception)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order"></param>
        private void AddOrder(Models.Order order)
        {
            order.Status = 0;
            _dBContext.Order.Add(order);
            _dBContext.SaveChanges();
        }

        private void UpdateOrder(Models.Order order, int status)
        {
            Models.Order orderObj = _dBContext.Order.First(o => o.Id == order.Id && o.UserId == order.UserId);
            if (orderObj != null)
            {
                orderObj.Status = status;
            }
            _dBContext.SaveChanges();
        }
    }
}
