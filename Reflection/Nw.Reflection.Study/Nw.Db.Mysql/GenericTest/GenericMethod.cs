using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Db.Mysql
{
    public class GenericMethod
    {
        public void Show<T,X,Y>(T t,X x,Y y)
        {
            Console.WriteLine($"t.type={t.GetType().Name},x.type={x.GetType().Name},y.type={y.GetType().Name}");
        }
    }
}
