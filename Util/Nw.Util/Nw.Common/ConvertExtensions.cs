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
