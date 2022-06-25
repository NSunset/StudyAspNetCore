using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Producer.old
{
    public class SimpleProducer
    {
        public static void Show()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string tag = path.Split('/', '\\').Last(s => !string.IsNullOrEmpty(s));
            Console.WriteLine($"这里是{tag}启动了");

            //1、添加包RabbitMQ.Client
            //2、创建连接
            //3、创建信道
            //4、基于信道来创建队列
            ConnectionFactory connectionFactory = new ConnectionFactory();

            connectionFactory.HostName = "192.168.157.128";
            connectionFactory.UserName = "admin";
            connectionFactory.Password = "admin";
            connectionFactory.VirtualHost = "my_vhost";



            //基础测试
            {
                using (IConnection connection = connectionFactory.CreateConnection())
                {
                    //创建信道
                    IModel channel = connection.CreateModel();

                    //队列名
                    string queueName = "nwTest";

                    //交换机名
                    string exchangeName = "ProducerWriteExchange";

                    //删除队列
                    channel.QueueDelete(queueName);
                    //删除交换机
                    channel.ExchangeDelete(exchangeName);

                    //声明队列
                    channel.QueueDeclare(
                        queue: queueName,
                        durable: true,//支持持久化
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    //声明交换机
                    channel.ExchangeDeclare(
                        exchange: exchangeName,
                        type: ExchangeType.Direct,
                        durable: true,//支持持久化
                        autoDelete: false,
                        arguments: null
                        );

                    string routingKey = "advanced";
                    //绑定交换机和队列
                    channel.QueueBind(
                        queue: queueName,
                        exchange: exchangeName,
                        routingKey: routingKey,
                        arguments: null
                        );

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"生产者{tag}已准备就绪");
                    {
                        IBasicProperties basicProperties = channel.CreateBasicProperties();
                        basicProperties.Persistent = true;//持久性
                                                          //basicProperties.Persistent = false;//非持久性
                                                          //basicProperties.DeliveryMode = 1;//非持久化
                                                          //basicProperties.DeliveryMode = 2;//支持持久化
                        for (int i = 0; i < 20; i++)
                        {
                            string message = $"{tag}测试生产者发布消息{i + 1}";
                            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);

                            //发布消息
                            channel.BasicPublish(
                                exchange: exchangeName,
                                routingKey: routingKey,
                                mandatory: false,
                                basicProperties: basicProperties,
                                body: messageBodyBytes
                                );

                            Console.WriteLine($"{message}已发送");

                            Thread.Sleep(500);
                        }

                        while (true)
                        {
                            Console.WriteLine("请输入消息内容：");
                            string message = Console.ReadLine();
                            byte[] body = Encoding.UTF8.GetBytes(message);
                            channel.BasicPublish(
                                exchange: exchangeName,
                                routingKey: routingKey,
                                mandatory: false,
                                basicProperties: basicProperties,
                                body: body
                                );

                            Console.WriteLine($"{message} 已发送~");
                            Thread.Sleep(500);
                        }

                    }

                    Console.ReadLine();
                }
            }

            //指定消息优先级
            {
                //using (IConnection connection = connectionFactory.CreateConnection())
                //{
                //    //指定最大优先级是10
                //    Dictionary<string, object> arguments = new Dictionary<string, object>
                //    {
                //            {"x-max-priority",10 }
                //    };

                //    //创建信道
                //    IModel channel = connection.CreateModel();

                //    //队列名
                //    string queueName = "nwTest";

                //    //交换机名
                //    string exchangeName = "ProducerWriteExchange";

                //    //删除队列
                //    channel.QueueDelete(queueName);
                //    //删除交换机
                //    channel.ExchangeDelete(exchangeName);

                //    //声明队列
                //    channel.QueueDeclare(
                //        queue: queueName,
                //        durable: true,
                //        exclusive: false,
                //        autoDelete: false,
                //        arguments: arguments//设置队列里的消息最大优先级是10
                //        );

                //    //声明交换机
                //    channel.ExchangeDeclare(
                //        exchange: exchangeName,
                //        type: ExchangeType.Direct,
                //        durable: true,
                //        autoDelete: false,
                //        arguments: null
                //        );

                //    string routingKey = "advanced";
                //    //绑定交换机和队列
                //    channel.QueueBind(
                //        queue: queueName,
                //        exchange: exchangeName,
                //        routingKey: routingKey,
                //        arguments: null
                //        );

                //    Console.ForegroundColor = ConsoleColor.Red;
                //    Console.WriteLine($"生产者{tag}已准备就绪");
                //    {
                //        IBasicProperties basicProperties = channel.CreateBasicProperties();
                //        basicProperties.Persistent = true;//非持久性

                //        for (int i = 0; i < 10; i++)
                //        {
                //            //指定消息优先级
                //            basicProperties.Priority = (byte)(i + 1);

                //            string message = $"{tag}消息优先级为{i + 1}";
                //            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);

                //            //发布消息
                //            channel.BasicPublish(
                //                exchange: exchangeName,
                //                routingKey: routingKey,
                //                mandatory: false,
                //                basicProperties: basicProperties,
                //                body: messageBodyBytes
                //                );

                //            Console.WriteLine($"{message}已发送");

                //            Thread.Sleep(500);
                //        }

                //        for (int i = 0; i < 10; i++)
                //        {
                //            basicProperties.Priority = 1;//指定消息优先级为1

                //            string message = $"{tag}消息优先级始终为1";
                //            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);

                //            //发布消息
                //            channel.BasicPublish(
                //                exchange: exchangeName,
                //                routingKey: routingKey,
                //                mandatory: false,
                //                basicProperties: basicProperties,
                //                body: messageBodyBytes
                //                );

                //            Console.WriteLine($"{message}已发送");

                //            Thread.Sleep(500);
                //        }

                //        while (true)
                //        {
                //            Console.WriteLine("请输入消息内容：");
                //            string message = Console.ReadLine();
                //            byte[] body = Encoding.UTF8.GetBytes(message);
                //            channel.BasicPublish(
                //                exchange: exchangeName,
                //                routingKey: routingKey,
                //                mandatory: false,
                //                basicProperties: basicProperties,
                //                body: body
                //                );

                //            Console.WriteLine($"{message} 已发送~");
                //            Thread.Sleep(500);
                //        }

                //    }

                //    Console.ReadLine();
                //}
            }
        }
    }
}
