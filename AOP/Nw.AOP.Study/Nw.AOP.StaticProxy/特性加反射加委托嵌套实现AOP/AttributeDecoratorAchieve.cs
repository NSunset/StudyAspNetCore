using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nw.AOP.StaticProxy
{
    public static class AttributeDecoratorAchieve
    {
        public static void Show(this IUserService userService, string methodName, object?[]? parameters)
        {
            MethodInfo method = typeof(IUserService).GetMethod(methodName);

            if (method.IsDefined(typeof(BaseDecoratorAttribute), true))
            {
                Action methodInvoke = () =>
                {
                    method.Invoke(userService, parameters);
                };

                IEnumerable<BaseDecoratorAttribute> baseDecorators = method.GetCustomAttributes<BaseDecoratorAttribute>(true);

                foreach (BaseDecoratorAttribute decorato in baseDecorators)
                {
                    methodInvoke = decorato.Expand(methodInvoke);
                }

                methodInvoke.Invoke();
            }
        }

        public static T Show<T>(this IUserService userService, string methodName, object?[]? parameters)
        {
            MethodInfo method = typeof(IUserService).GetMethod(methodName);

            if (method.IsDefined(typeof(BaseDecoratorAttribute), true))
            {
                Func<T> methodInvoke = () =>
                {
                    object obj = method.Invoke(userService, parameters);
                    return (T)obj;
                };

                IEnumerable<BaseDecoratorAttribute> baseDecorators = method.GetCustomAttributes<BaseDecoratorAttribute>(true);

                foreach (BaseDecoratorAttribute decorato in baseDecorators)
                {
                    methodInvoke = decorato.Expand(methodInvoke);
                }

                T result = methodInvoke.Invoke();
                return result;
            }
            return default(T);
        }

    }
}
