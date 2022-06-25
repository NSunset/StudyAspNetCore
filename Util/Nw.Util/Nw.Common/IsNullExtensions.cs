using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Common
{
    public static class IsNullExtensions
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 检查对象是否为null,为null则抛出<see cref="ArgumentNullException"/> 异常
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static void CheckNull(this object obj, string parameterName)
        {
            if (obj == null)
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> value)
        {
            if (value == null)
                return true;
            return !value.Any();
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid? value)
        {
            if (value == null) return true;
            return value == Guid.Empty;
        }
    }
}
