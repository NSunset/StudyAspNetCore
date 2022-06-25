using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.MyLambdaLinq
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }


        public List<User> GetUsers()
        {
            return new List<User>
            {
                new User{
                    Id=10,
                    Name="张三",
                    Price=99
                },
                new User
                {
                    Id=20,
                    Name="李四",
                    Price=88
                },
                new User
                {
                    Id=30,
                    Name="王五",
                    Price=77
                },
                new User
                {
                    Id=40,
                    Name="赵六",
                    Price=66
                }
            };
        }
    }
}
