using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.Common.CacheHelper
{
    public static class CacheKeyConstant
    {
        /// <summary>
        /// 当前用户的菜单集合  缓存的Key
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetCurrentUserMenuListKeyConstant(string userId) => $"user_{userId}_menu";

        /// <summary>
        /// 当前用户的菜单Url地址  缓存的key
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetCurrentUserMenuUrlKeyConstant(string userId) => $"user_{userId}_menuUrl";


        /// <summary>
        /// 统计当日排行数据--- 取Redis取值的一个Key
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetDaydataKeyConstant() => $"{DateTime.Now.ToString("yyyyMMdd")}_day";

        public static string GetWeekdataKeyConstant(string weekOfYear) => $"{DateTime.Now.Year}_{weekOfYear}_week";

        public static string GetMonthdataKeyConstant() => $"{DateTime.Now.Month}_month";

        public static string GetYeardataKeyConstant() => $"{DateTime.Now.ToString("yyyyMMdd")}_day";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLiveStateConstant(int userId) => $"{userId}_GetLiveStateConstant";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetPayUrlRedisKeyPrefix(string orderNum) => $"Create_Pay_Url_{orderNum}";

        /// <summary>
        /// 保存弹幕 到Redis需要的Key
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetBulletChatKeyContant(int userId, int cSWorksId) => $"BulletChat_{userId}_{cSWorksId}";
    }
}
