using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Common
{
    public class DateTimeUtil
    {
        private static readonly string[] day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        /// <summary>
        /// 获取指定时间当前周的开始时间
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public DateTime GetCurrentWeekStart(DateTime time)
        {
            DateTime startWeek = time.AddDays(1 - Convert.ToInt32(time.DayOfWeek.ToString("d")));
            return startWeek;
        }

        /// <summary>
        /// 获取指定时间当前周的结束时间
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public DateTime GetCurrentWeekEnd(DateTime time)
        {
            return GetCurrentWeekStart(time).AddDays(6);
        }

        /// <summary>
        /// 获取指定时间本月月初时间
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public DateTime GetCurreentMonthStart(DateTime time)
        {
            return time.AddDays(1 - time.Day);
        }

        /// <summary>
        /// 获取指定时间本月月末时间
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public DateTime GetCurreentMonthEnd(DateTime time)
        {
            return GetCurreentMonthStart(time).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 获取指定时间本季度初
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public DateTime GetCurrentQuarterStart(DateTime time)
        {
            return time.AddMonths(0 - (time.Month - 1) % 3).AddDays(1 - time.Day);
        }

        /// <summary>
        /// 获取指定时间本季度末
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public DateTime GetCurrentQuarterEnd(DateTime time)
        {
            return GetCurrentQuarterStart(time).AddMonths(3).AddDays(-1);
        }

        /// <summary>
        /// 获取指定时间本年年初
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public DateTime GetCurrentYearStart(DateTime time)
        {
            return new DateTime(time.Year, 1, 1);
        }

        /// <summary>
        /// 获取指定时间本年年末
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public DateTime GetCurrentYearEnd(DateTime time)
        {
            return new DateTime(time.Year, 12, 31); ;
        }

        /// <summary>
        /// 获取指定时间是周几的中文
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string GetChineseWeek(DateTime time)
        {
            string week = day[Convert.ToInt32(time.DayOfWeek.ToString("d"))].ToString();
            return week;
        }
    }
}
