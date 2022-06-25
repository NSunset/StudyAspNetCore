using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storage.MicroService.IService;
using Storage.MicroServie.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.MicroServie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [Route("decrease/{productId}/{count}")]
        [HttpGet]
        public ApiResponse DecreaseStock(long productId, int count)
        {
            try
            {
                _storageService.DecreaseStock(productId, count);
            }
            catch (Exception)
            {
                return ApiResponse.Fail("扣减库存失败");
            }
            return ApiResponse.OK();
        }
    }
}
