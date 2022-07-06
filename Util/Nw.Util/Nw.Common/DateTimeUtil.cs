using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Common
{
    public static class DateTimeUtil
    {
        private static readonly string[] day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        /// <summary>
        /// 计算当天所在周是一年中的第几周
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int WeekOfYear(int day)
        {
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            return gc.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// 获取指定时间当前周的开始时间
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public static DateTime GetCurrentWeekStart(DateTime time)
        {
            DateTime startWeek = time.AddDays(1 - Convert.ToInt32(time.DayOfWeek.ToString("d")));
            return startWeek;
        }

        /// <summary>
        /// 获取指定时间当前周的结束时间
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public static DateTime GetCurrentWeekEnd(DateTime time)
        {
            return GetCurrentWeekStart(time).AddDays(6);
        }

        /// <summary>
        /// 获取指定时间本月月初时间
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public static DateTime GetCurreentMonthStart(DateTime time)
        {
            return time.AddDays(1 - time.Day);
        }

        /// <summary>
        /// 获取指定时间本月月末时间
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public static DateTime GetCurreentMonthEnd(DateTime time)
        {
            return GetCurreentMonthStart(time).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 获取指定时间本季度初
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public static DateTime GetCurrentQuarterStart(DateTime time)
        {
            return time.AddMonths(0 - (time.Month - 1) % 3).AddDays(1 - time.Day);
        }

        /// <summary>
        /// 获取指定时间本季度末
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public static DateTime GetCurrentQuarterEnd(DateTime time)
        {
            return GetCurrentQuarterStart(time).AddMonths(3).AddDays(-1);
        }

        /// <summary>
        /// 获取指定时间本年年初
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public static DateTime GetCurrentYearStart(DateTime time)
        {
            return new DateTime(time.Year, 1, 1);
        }

        /// <summary>
        /// 获取指定时间本年年末
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <returns></returns>
        public static DateTime GetCurrentYearEnd(DateTime time)
        {
            return new DateTime(time.Year, 12, 31); ;
        }

        /// <summary>
        /// 获取指定时间是周几的中文
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetChineseWeek(DateTime time)
        {
            string week = day[Convert.ToInt32(time.DayOfWeek.ToString("d"))].ToString();
            return week;
        }

        /// <summary>
        /// 可为空的时间格式转为DateTime?
        /// </summary>
        /// <param name="time">可为空的时间格式</param>
        /// <param name="defaulTime">
        /// 转换失败给的时间；默认为null
        /// </param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this object time, DateTime? defaulTime = null)
        {
            if (!Equals(time, null))
            {
                DateTime result;
                if (DateTime.TryParse(time.ToString(), out result))
                {
                    return result;
                }
            }
            return defaulTime;
        }

        /// <summary>
        /// 时间格式字符串转为DateTime
        /// </summary>
        /// <param name="time">时间格式的字符串</param>
        /// <param name="defaulTime">
        /// 转换失败给的时间；默认为DateTime.MinValue
        /// </param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string time, DateTime? defaulTime = null)
        {
            DateTime result;
            if (defaulTime == null)
            {
                defaulTime = DateTime.MinValue;
            }
            if (DateTime.TryParse(time, out result))
            {
                return result;
            }
            return defaulTime.Value;
        }

        /// <summary>
        /// 格式化字符串，带时分秒格式 "yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="time">要格式化的时间</param>
        /// <param name="removeSecond">是否删除秒</param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime time, bool removeSecond = false)
        {
            if (removeSecond)
                return time.ToString("yyyy-MM-dd HH:mm");
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 格式化字符串，带时分秒格式 "yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="time">要格式化的时间</param>
        /// <param name="removeSecond">是否删除秒</param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime? time, bool removeSecond = false)
        {
            if (time == null)
                return string.Empty;
            return time.Value.ToDateTimeString(removeSecond);
        }

        /// <summary>
        /// 格式化字符串，格式 "yyyy-MM-dd"
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 格式化字符串，格式 "yyyy-MM-dd"
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime? time)
        {
            if (time == null)
                return string.Empty;
            return time.Value.ToDateString();
        }

        /// <summary>
        /// 格式化字符串，格式 "HH:mm:ss"
        /// </summary>
        /// <param name="time">要格式化的时间</param>
        /// <param name="removeSecond">是否删除秒</param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime time, bool removeSecond = false)
        {
            if (removeSecond)
                return time.ToString("HH:mm");
            return time.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 格式化字符串， 格式"HH:mm:ss"
        /// </summary>
        /// <param name="time">要格式化的时间</param>
        /// <param name="removeSecond">是否删除秒</param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime? time, bool removeSecond = false)
        {
            if (time == null)
                return string.Empty;
            return time.Value.ToTimeString(removeSecond);
        }

        /// <summary>
        /// 格式化字符串带毫秒，格式 "yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToMillisecondString(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 格式化字符串带毫秒，格式 "yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToMillisecondString(this DateTime? time)
        {
            if (time == null)
                return string.Empty;
            return time.Value.ToMillisecondString();
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToChineseDateString(this DateTime time)
        {
            return string.Format("{0}年{1}月{2}日", time.Year, time.Month.ToString("00"), time.Day.ToString("00"));
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToChineseDateString(this DateTime? time)
        {
            if (time == null)
                return string.Empty;
            return time.Value.ToChineseDateString();
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分ss秒"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="removeSecond">是否移除秒</param>
        public static string ToChineseDateTimeString(this DateTime dateTime, bool removeSecond = false)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(dateTime.ToChineseDateString());
            result.AppendFormat(" {0}时{1}分", dateTime.Hour.ToString("00"), dateTime.Minute.ToString("00"));
            if (removeSecond == false)
                result.AppendFormat("{0}秒", dateTime.Second.ToString("00"));
            return result.ToString();
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分ss秒"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="removeSecond">是否移除秒</param>
        /// <returns></returns>
        public static string ToChineseDateTimeString(this DateTime? dateTime, bool removeSecond = false)
        {
            if (dateTime == null)
                return string.Empty;
            return dateTime.Value.ToChineseDateTimeString(removeSecond);
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="span">时间间隔</param>
        /// <returns></returns>
        public static string ToDescription(this TimeSpan span)
        {
            StringBuilder result = new StringBuilder();
            if (span.Days > 0)
                result.AppendFormat("{0}天", span.Days);
            if (span.Hours > 0)
                result.AppendFormat("{0}小时", span.Hours);
            if (span.Minutes > 0)
                result.AppendFormat("{0}分", span.Minutes);
            if (span.Seconds > 0)
                result.AppendFormat("{0}秒", span.Seconds);
            if (span.Milliseconds > 0)
                result.AppendFormat("{0}毫秒", span.Milliseconds);
            if (result.Length > 0)
                return result.ToString();
            return string.Format("{0}毫秒", span.TotalSeconds * 1000);
        }
    }
}
