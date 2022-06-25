using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SocketHelper socketHelper = new SocketHelper();
                Input input = new Input
                {
                    Key = 3,
                    Body = new
                    {
                        UserName = "张三",
                        Pwd = "123456"
                    }
                };

                Input order = new Input
                {
                    Key = 1,
                    Body = 1
                };
                socketHelper.Send(input);
                //socketHelper.Send(order);
                {
                    //Input input = new Input
                    //{
                    //    Key = 1,
                    //    Body = new
                    //    {
                    //        UserName = "张三",
                    //        Pwd = "123456"
                    //    }
                    //};

                    //socketHelper.Send(input);
                }
                {
                    //Input input = new Input
                    //{
                    //    Key = 3,
                    //    Body = new
                    //    {
                    //        UserName = "张三",
                    //        Pwd = "123456"
                    //    }
                    //};

                    //Input input1 = new Input
                    //{
                    //    Key = 3,
                    //    Body = new
                    //    {
                    //        UserName = "李四",
                    //        Pwd = "123456"
                    //    }
                    //};

                    //socketHelper.Send(input);
                    //socketHelper.Send(input1);

                    Input heartBeat = new Input
                    {
                        Key = 2,
                        Body = 1
                    };
                    for (int i = 0; i < 10; i++)
                    {
                        socketHelper.Send(heartBeat);
                        System.Threading.Thread.Sleep(500);
                    }
                }

                Console.Read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
