using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using RedisHelper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Models.ViewModel
{
    public class LoginResultViewModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string UserName { get; set; }
        public int UserId { get; set; }


        /// <summary>
        /// 缓存刷新token
        /// </summary>
        /// <param name="redisStringService"></param>
        /// <param name="value">刷新token缓存的数据</param>
        /// <param name="timeSpan"></param>
        public void CacheRefreshToken(RedisStringService redisStringService, CSUser value, TimeSpan timeSpan)
        {
            redisStringService.Set(RefreshToken, value, timeSpan);
        }

        /// <summary>
        /// 获取缓存刷新token的值
        /// </summary>
        /// <param name="redisStringService"></param>
        /// <param name="value">刷新token缓存的数据</param>
        /// <param name="timeSpan"></param>
        public static CSUser GetCacheRefreshToken(RedisStringService redisStringService, string refreshToken)
        {
            return redisStringService.Get<CSUser>(refreshToken);
        }
    }
}
