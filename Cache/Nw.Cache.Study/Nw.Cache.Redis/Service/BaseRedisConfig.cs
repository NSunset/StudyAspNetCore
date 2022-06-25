using Microsoft.Extensions.Options;
using Nw.Cache.Redis.Init;
using Nw.Cache.Redis.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Cache.Redis.Service
{
    public class BaseRedisConfig : IRedisConfig
    {
        private readonly RedisConfigInfo _redisConfigInfo;
        public BaseRedisConfig(IOptions<RedisConfigInfo> redisConfigInfo)
        {
            _redisConfigInfo = redisConfigInfo.Value;
        }

        public string GetRedisConnectionString()
        {
            return _redisConfigInfo.WriteServerList[0];            
        }
    }
}
