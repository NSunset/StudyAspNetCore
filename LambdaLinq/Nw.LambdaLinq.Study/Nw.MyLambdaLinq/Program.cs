using System;
using System.Collections.Generic;

namespace Nw.MyLambdaLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lambda
            {
                //简化了委托的写法，这里使用go=>to语法,匿名方法
                Action<User> user = u =>
                {
                    int id = u.Id;
                };

                //匿名类
                var u = new
                {
                    Id = 10,
                    Name = "田七",
                    Price = 55
                };

                int id = u.Id;
                string name = u.Name;
                //u.Price = 100;//匿名类属性只能读，不能改


                

            }


            //Linq
            {
                User user = new User();

                List<User> list = user.GetUsers();


                List<User> result = new List<User>();
                foreach (var item in list)
                {
                    if (item.Price > 50)
                    {
                        result.Add(item);
                    }
                }

                //linq思想吧固定逻辑封住起来，把可变逻辑包装成委托传递
                IEnumerable<User> users = list.NwWhere(u => u.Price > 80);
            }
           


            Console.ReadLine();
        }
    }
}
