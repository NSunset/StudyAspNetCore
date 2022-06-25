using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.MicroService.IService
{
    public interface IAccountService
    {
        Task<bool> DeductMoney(long? userId, decimal? money);

        Task<bool> DeductMoney1(long? userId, decimal? money);
    }
}
