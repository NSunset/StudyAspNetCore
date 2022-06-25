using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nw.Cache.Redis.Init;
using Nw.Cache.Redis.Interface;
using Nw.Cache.Redis.Service;
using StackExchange.Redis;
using static StackExchange.Redis.RedisChannel;

namespace Nw.Redis.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //RedisConnection.SetDefaultConnection("192.168.157.128:6379");
                //string测试
                {
                    //RedisStringService service = RedisFactory.GetDefaultRedis(RedisDataType.DbString);
                    //service.Set<string>("student1", "梦的翅膀");
                    //Console.WriteLine(service.Get("student1"));

                    //service.Set<string>("student2", "王", TimeSpan.FromSeconds(5));
                    //Thread.Sleep(5100);
                    //Console.WriteLine(service.Get("student2"));

                    //service.Set<int>("Age", 32);
                    //Console.WriteLine(service.Increment("Age", 2));
                    //Console.WriteLine(service.Decrement("Age", 1));

                }

                //Hash测试
                {
                    //RedisHashService service = RedisFactory.GetDefaultRedis(RedisDataType.DbHash);
                    //service.Set("student", "id", "123456");
                    //service.Set("student", "name", "张xx");
                    //service.Set("student", "remark", "高级班的学员");

                    //Console.WriteLine(service.Get("student", "id"));


                    //service.Delete("student", "id");
                    //Console.WriteLine(service.Get("student", "id"));
                }

                //排序集合测试
                {
                    //RedisZSetService service = RedisFactory.GetDefaultRedis(RedisDataType.DbSorted);

                    //service.SortedSetAdd("advanced", "1", 0);
                    //service.SortedSetAdd("advanced", "2", 1);
                    //service.SortedSetAdd("advanced", "5", 2);
                    //service.SortedSetAdd("advanced", "4", 3);
                    //service.SortedSetAdd("advanced", "7", 4);
                    //service.SortedSetAdd("advanced", "5", 6);
                    //service.SortedSetAdd("advanced", "9", 5);

                    //var result1 = service.SortedSetLength("advanced");
                    //var result2 = service.SortedSetRangeByRank("advanced", order: StackExchange.Redis.Order.Descending);

                    //service.SortedSetIncrement("Sort", "BY", 123234);
                    //service.SortedSetIncrement("Sort", "走自己的路", 123);
                    //service.SortedSetIncrement("Sort", "redboy", 45);
                    //service.SortedSetIncrement("Sort", "大蛤蟆", 7567);
                    //service.SortedSetIncrement("Sort", "路人甲", 9879);
                    //var result3 = service.SortedSetRangeByRank("Sort");


                }

                //List测试,已经可以实现生产消费模式了
                {
                    //RedisListService service = RedisFactory.GetDefaultRedis(RedisDataType.DbList);

                    //service.RightPush("article", "Zhaoxi1234");
                    //service.RightPush("article", "kevin");
                    //service.RightPush("article", "大叔");
                    //service.RightPush("article", "C卡");
                    //service.RightPush("article", "触不到的线");
                    //service.RightPush("article", "程序错误");

                    //var result1 = service.Range("article");
                    //var result2 = service.Range("article", 0, 3);
                    ////可以按照添加顺序自动排序；而且可以分页获取

                    //service.LeftPush("article", "Zhaoxi1234");
                    //service.LeftPush("article", "kevin");
                    //service.LeftPush("article", "大叔");
                    //service.LeftPush("article", "C卡");
                    //service.LeftPush("article", "触不到的线");
                    //service.LeftPush("article", "程序错误");

                    //for (int i = 0; i < 5; i++)
                    //{
                    //    Console.WriteLine(service.RightPop("article"));
                    //    var result3 = service.Range("article");
                    //}

                    #region 生产者消费者
                    //List<string> stringList = new List<string>();
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    stringList.Add(string.Format($"放入任务{i}"));
                    //}

                    //service.RightPush("test", "这是一个学生Add1");
                    //service.RightPush("test", "这是一个学生Add2");
                    //service.RightPush("test", "这是一个学生Add3");

                    //service.LeftPush("test", "这是一个学生LPush1");
                    //service.LeftPush("test", "这是一个学生LPush2");
                    //service.LeftPush("test", "这是一个学生LPush3");
                    //service.LeftPush("test", "这是一个学生LPush4");
                    //service.LeftPush("test", "这是一个学生LPush5");
                    //service.LeftPush("test", "这是一个学生LPush6");

                    //service.RightPush("test", "这是一个学生RPush1");
                    //service.RightPush("test", "这是一个学生RPush2");
                    //service.RightPush("test", "这是一个学生RPush3");
                    //service.RightPush("test", "这是一个学生RPush4");
                    //service.RightPush("test", "这是一个学生RPush5");
                    //service.RightPush("test", "这是一个学生RPush6");
                    //service.RightPush("test", stringList);

                    //Console.WriteLine(service.Length("test"));

                    //while (true)
                    //{
                    //    string msg = string.Format($"放入任务{new Random().Next(1, 99)}");
                    //    Console.WriteLine(msg);

                    //    service.RightPush("test", msg);
                    //    Thread.Sleep(500);
                    //}

                    //while (service.Length("test") > 0)
                    //{
                    //    Console.WriteLine(service.LeftPop("test"));
                    //    //Thread.Sleep(500);
                    //}

                    #endregion
                }

                //发布订阅,注意多线程
                {

                    //RedisSubscriberService service = RedisFactory.GetDefaultRedis(RedisDataType.RedisSubscribe);

                    //顺序执行，没有并发
                    {
                        //service.Subscribe("Nw", (channel, value) =>
                        //{
                        //    Console.WriteLine($"订阅1{channel}:{value}，Dosomething else");
                        //});

                        //service.Subscribe("Nw", message =>
                        //{
                        //    Console.WriteLine($"订阅2{message.Channel}:{message.Message}:{message.SubscriptionChannel}，Dosomething else");
                        //});

                        //service.Subscribe("Nw", message =>
                        //{
                        //    Console.WriteLine($"订阅3{message.Channel}:{message.Message}:{message.SubscriptionChannel}，Dosomething else");
                        //});

                        //service.Publish("Nw", "Nw123");
                    }

                    //多线程并发订阅
                    {
                        //Task.Run(() =>
                        //{
                        //    try
                        //    {
                        //        service.Subscribe("Nw", message =>
                        //        {
                        //            Console.WriteLine($"订阅1{message.Channel}:{message.Message}:{message.SubscriptionChannel}，Dosomething else");
                        //        });
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //    }

                        //});
                        //Task.Run(() =>
                        //{
                        //    try
                        //    {
                        //        service.Subscribe("Nw", message =>
                        //        {
                        //            Console.WriteLine($"订阅2{message.Channel}:{message.Message}:{message.SubscriptionChannel}，Dosomething else");
                        //        });
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //        throw ex;
                        //    }

                        //});
                        //Task.Run(() =>
                        //{
                        //    try
                        //    {
                        //        service.Subscribe("Nw", message =>
                        //        {
                        //            Console.WriteLine($"订阅3{message.Channel}:{message.Message}:{message.SubscriptionChannel}，Dosomething else");
                        //        });
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //        throw ex;
                        //    }

                        //});
                        //Task.Run(() =>
                        //{
                        //    service.Publish("Nw", "Nw123");
                        //});
                    }
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
