using Microsoft.Extensions.Logging;
using Storage.MicroService.IService;
using Storage.MicroService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.MicroService.Service
{
    public class StorageService : IStorageService
    {
        private ILogger<StorageService> _logger;
        private StorageDbContext _dbContext;

        public StorageService(ILogger<StorageService> logger,
            StorageDbContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        public void DecreaseStock(long? productId, int? count)
        {
            _logger.LogInformation("扣减库存开始....");
            Models.Storage storage = _dbContext.Storage.First(s => s.ProductId == productId);
            if (storage.Residue - count < 0)
            {
                throw new Exception("库存不足");
            }
            storage.Residue -= count;
            storage.Used += count;
            _logger.LogInformation("扣减库存结束....");
            _dbContext.SaveChanges();
        }
    }
}
