using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisHelper.Interface
{
    public interface IRedisConfigureHelper
    {
        /// <summary>
        /// 获取redis链接字符串
        /// </summary>
        /// <param name="redisConfigInfo"></param>
        /// <returns></returns>
        public string GetRedisConnectionString();
    }
}
