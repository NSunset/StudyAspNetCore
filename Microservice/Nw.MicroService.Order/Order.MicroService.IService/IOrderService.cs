using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.MicroService.IService
{
    public interface IOrderService
    {
        long CreateOrder(Models.Order order);
    }
}
