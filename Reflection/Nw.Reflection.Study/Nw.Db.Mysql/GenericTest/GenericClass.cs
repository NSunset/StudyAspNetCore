using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Db.Mysql
{
    public class GenericClass<T, X, Y>
    {
        public void Show(T t, X x, Y y)
        {
            Console.WriteLine($"t.type={t.GetType().Name},x.type={x.GetType().Name},y.type={y.GetType().Name}");
        }
    }
}
