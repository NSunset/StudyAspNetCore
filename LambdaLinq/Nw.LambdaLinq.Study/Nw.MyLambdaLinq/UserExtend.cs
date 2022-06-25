using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyLambdaLinq
{
    public static class UserExtend
    {
        public static IEnumerable<User> NwWhere(this IEnumerable<User> users,Func<User,bool> func)
        {
            //List<User> result = new List<User>();
            foreach (var item in users)
            {
                //if (item.Price > 50)
                //{
                //    result.Add(item);
                //}
                if (func.Invoke(item))
                {
                    //      result.Add(item);
                    yield return item;
                }
            }
            //return result;
        }
    }
}
