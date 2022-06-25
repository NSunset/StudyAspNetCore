using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nw.MyAttribute
{
    public static class EnumExtend
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetRemark(this Enum @enum)
        {
            Type type = @enum.GetType();
            FieldInfo field = type.GetField(@enum.ToString());
            if (field.IsDefined(typeof(RemarkAttribute), true))
            {
                RemarkAttribute remarkAttribute = field.GetCustomAttribute<RemarkAttribute>(true);
                return remarkAttribute.Remark;
            }
            return @enum.ToString();
        }
    }
}
