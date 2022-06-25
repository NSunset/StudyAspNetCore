using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.MicroService.IService
{
    public interface IStorageService
    {
        void DecreaseStock(long? productId, int? count);
    }
}
