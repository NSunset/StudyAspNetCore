using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nw.MyAttribute
{
    public static class ValidateExtend
    {
        /// <summary>
        /// 验证对象属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ValidateResult Validate<T>(this T model) where T : class
        {
            Type type = model.GetType();

            foreach (PropertyInfo item in type.GetProperties())
            {
                if (item.IsDefined(typeof(BaseValidateAttribute), true))
                {
                    object propertyInfoValue = item.GetValue(model);

                    BaseValidateAttribute validateAttribute = item.GetCustomAttribute<BaseValidateAttribute>(true);

                    ValidateResult result = validateAttribute.Validate(item.Name, propertyInfoValue);
                    if (!result.IsOk)
                    {
                        return result;
                    }
                }
            }
            return ValidateResult.Ok();
        }
    }
}
