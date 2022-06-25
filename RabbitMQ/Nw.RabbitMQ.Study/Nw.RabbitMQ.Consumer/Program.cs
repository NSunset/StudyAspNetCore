using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus;
using EventBus.Events;
using EventBus.Interface;
using Nw.RabbitMQ.Api.EventHandling;
using Nw.RabbitMQ.Api.Models;
using Nw.RabbitMQ.Consumer.old;
using Nw.RabbitMQ.Tool;
using Nw.RabbitMQ.Tool.Init;
using Nw.RabbitMQ.Tool.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQTool.Interface;

namespace Nw.RabbitMQ.Consumer
{
    /// <summary>
    /// 消费者
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {

                //IConnectionFactory connectionFactory = new ConnectionFactory
                //{
                //    HostName = "192.168.157.128",
                //    VirtualHost = "my_vhost",
                //    UserName = "admin",
                //    Password = "admin"
                //};

                //IConnection connection = connectionFactory.CreateConnection();
                //IConnection connection1 = connectionFactory.CreateConnection();
                //IConnection connection2 = connectionFactory.CreateConnection();

                //SimpleConsumer.Show();

                //ConsumerConfirm.Show();

                //Console.ReadLine();

                //List<RabbitMQExchange> rabbitMQExchanges = Init();
                //Console.ForegroundColor = ConsoleColor.Green;
                //{
                //    RabbitMQConnectionConfig config = new RabbitMQConnectionConfig
                //    {
                //        HostName = "192.168.157.128",
                //        VirtualHost = "my_vhost",
                //        UserName = "admin",
                //        Password = "admin"
                //    };

                //    RabbitMQInitialize.Initialize(rabbitMQExchanges, config);

                //    DefaultRabbitMQHandler rabbitMQHandler = new DefaultRabbitMQHandler(config);

                //    {
                //        await rabbitMQHandler.RegistReciveActionAsync("TestEqeue_Direct001", x =>
                //         {
                //             Console.WriteLine($"接收消息：{x}");
                //             return true;
                //         });
                //        await rabbitMQHandler.RegistReciveActionAsync("TestEqeue_Direct002", x =>
                //        {
                //            Console.WriteLine($"接收消息：{x}");
                //            return true;
                //        });
                //        await rabbitMQHandler.RegistReciveActionAsync("TestEqeue_Direct003", x =>
                //        {
                //            Console.WriteLine($"接收消息：{x}");
                //            return true;
                //        });
                //    }

                //}


                Console.ReadLine();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public static List<RabbitMQExchange> Init()
        {
            List<RabbitMQExchange> rabbitMQExchanges = new List<RabbitMQExchange>
            {
                ///使用Key一对一绑定交换机和队列
                new RabbitMQExchange
                {
                    ExchangeName="TestExchange_Direct",
                    ExchangeType=ExchangeType.Direct,
                    RabbitMQEqeue=new List<RabbitMQEqeue>
                    {
                        new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Direct001",
                            Key="001"
                        },
                        new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Direct002",
                            Key="002"
                        },
                        new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Direct003",
                            Key="003"
                        }
                    }
                },
                ///发布订阅模式，一个交换机分发所有队列
                new RabbitMQExchange
                {
                    ExchangeName="TestExchange_Fanout",
                    ExchangeType=ExchangeType.Fanout,
                    RabbitMQEqeue=new List<RabbitMQEqeue>
                    {
                        new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Fanout001",
                        },
                         new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Fanout002",
                        },
                          new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Fanout003",
                        }
                    }
                },
                ///模糊匹配Key,Key有自己的匹配规则
                new RabbitMQExchange
                {
                    ExchangeName="TestExchange_Topic",
                    ExchangeType=ExchangeType.Topic,
                    RabbitMQEqeue=new List<RabbitMQEqeue>
                    {
                        ///Key需要以routingKey.开头，才能发送到这个队列
                        new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Topic001",
                            Key="routingKey.#"
                        },
                        ///Key需要以.routing结尾，才能发送到这个队列
                        new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Topic002",
                            Key="#.routing"
                        },
                    }
                },
                ///使用Headers头信息来匹配
                new RabbitMQExchange
                {
                    ExchangeName="TestExchange_Headers",
                    ExchangeType=ExchangeType.Headers,
                    RabbitMQEqeue=new List<RabbitMQEqeue>
                    {
                        ///发送消息时设置的头信息必须包含name和pwd，且值跟这里相同，才能发送到这个队列
                        new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Headers001",
                            BindArguments=new Dictionary<string, object>
                            {
                                {"x-match","all" },
                                {"name","张三" },
                                {"pwd","123456" }
                            }
                        },
                        ///发送消息时设置的头信息只需要包含name和pwd其中一个，且值跟这里相同，才能发送到这个队列
                        new RabbitMQEqeue
                        {
                            QueueName="TestEqeue_Headers002",
                            BindArguments=new Dictionary<string, object>
                            {
                                {"x-match","any" },
                                {"name","张三" },
                                {"pwd","123456" }
                            }
                        },
                    }
                }
            };

            return rabbitMQExchanges;
        }
    }
}
