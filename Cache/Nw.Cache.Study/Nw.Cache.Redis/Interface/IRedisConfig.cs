using Nw.Cache.Redis.Init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Cache.Redis.Interface
{
    public interface IRedisConfig
    {
        /// <summary>
        /// 获取redis链接字符串
        /// </summary>
        /// <param name="redisConfigInfo"></param>
        /// <returns></returns>
        public string GetRedisConnectionString();
    }
}
