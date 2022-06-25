using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.MicroService.IService
{
    public interface IStorageService
    {
        /// <summary>
        /// 减库存
        /// </summary>
        /// <returns></returns>
        Task<bool> DecreaseStorage(long? productId, int? count);

        Task<bool> DecreaseStorage1(long? productId, int? count);
    }
}
