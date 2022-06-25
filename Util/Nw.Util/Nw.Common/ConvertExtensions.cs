using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Common
{
    public static class ConvertExtensions
    {
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

        /// <summary>
        /// 数字格式字符串转Int32
        /// </summary>
        /// <param name="number">数字格式字符串</param>
        /// <param name="defaultNumber">转换失败返回值，默认值为0</param>
        /// <returns></returns>
        public static int ToInt(this string number, int defaultNumber = 0)
        {
            if (defaultNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(defaultNumber));
            int.TryParse(number, out defaultNumber);
            return defaultNumber;
        }

        /// <summary>
        /// Decimal格式字符串转Decimal
        /// </summary>
        /// <param name="decimalValue">Decimal格式字符串</param>
        /// <param name="defaultDecimal">转换失败返回值，默认值为0.00</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string decimalValue, decimal defaultDecimal = 0.00m)
        {
            if (defaultDecimal <= 0)
                throw new ArgumentOutOfRangeException(nameof(defaultDecimal));
            decimal.TryParse(decimalValue, out defaultDecimal);
            return defaultDecimal;
        }

        /// <summary>
        /// Bool格式字符串转Bool
        /// </summary>
        /// <param name="flage">Bool格式字符串</param>
        /// <param name="defaultValue">转换失败返回值，默认值为false</param>
        /// <returns></returns>
        public static bool ToBool(this string flage, bool defaultValue = false)
        {
            bool.TryParse(flage, out defaultValue);
            return defaultValue;
        }
    }
}
