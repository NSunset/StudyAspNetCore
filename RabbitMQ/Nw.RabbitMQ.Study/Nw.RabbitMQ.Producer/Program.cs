using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Nw.RabbitMQ.Producer.old;
using RabbitMQ.Client;
using Nw.RabbitMQ.Tool;
using System.Threading.Tasks;
using Nw.RabbitMQ.Tool.Init;
using Nw.RabbitMQ.Tool.Models;
using EventBus.Interface;
using RabbitMQTool.Services;
using RabbitMQTool.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using EventBus;

namespace Nw.RabbitMQ.Producer
{
    /// <summary>
    /// 生产者
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //基础使用测试
                //SimpleProducer.Show();

                //交换机不同类型的测试
                //ExchangeDeclareTypeTest.Show();

                //测试队列数据持久化和确认发送成功
                //PersistenceAndConfirmationSend.Show();

                //封装RabbitMQ测试
                {

                    //fanout
                    {
                        //DefaultRabbitMQHandler rabbitMQHandler = new DefaultRabbitMQHandler(new RabbitMQConfig
                        //{
                        //    HostName = "192.168.157.128",
                        //    VirtualHost = "my_vhost",
                        //    UserName = "admin",
                        //    Password = "admin"
                        //});

                        //for (int i = 0; i < 5; i++)
                        //{
                        //    RabbitMQModel model = new RabbitMQModel(
                        //    "哈哈哈",
                        //    $"test{i + 1}",
                        //    "textEx"
                        //    );
                        //    rabbitMQHandler.Publish(model);
                        //    Console.WriteLine($"生产者发送消息：{model.Message}");
                        //}

                        //await rabbitMQHandler.RegistReciveActionAsync("test1", m =>
                        //{
                        //    Console.WriteLine($"消费者处理消息：{m}");
                        //    return true;
                        //}, true);




                    }

                    //direct
                    {
                        //DefaultRabbitMQHandler rabbitMQHandler = new DefaultRabbitMQHandler(new RabbitMQConfig
                        //{
                        //    HostName = "192.168.157.128",
                        //    VirtualHost = "my_vhost",
                        //    UserName = "admin",
                        //    Password = "admin"
                        //});


                        //using (rabbitMQHandler)
                        //{
                        //    for (int i = 0; i < 10; i++)
                        //    {
                        //        string msg = $"哈哈哈{i + 1}";
                        //        string key = $"key{i + 1}";
                        //        string queue = $"testQueue{i + 1}";
                        //        RabbitMQModel requestMsg = new RabbitMQModel(msg, queue, "testExchange", key);
                        //        rabbitMQHandler.Publish(requestMsg);
                        //        Console.WriteLine($"生产者发送消息：{requestMsg.Message}");
                        //    }
                        //}

                        //TaskFactory factory = Task.Factory;
                        //List<Task> tasks = new List<Task>();
                        //for (int i = 0; i < 10; i++)
                        //{
                        //    string queue = $"testQueue{i + 1}";
                        //    Task task = rabbitMQHandler.RegistReciveActionAsync(queue, m =>
                        //     {
                        //         Console.WriteLine($"消费端处理消息:{m}");
                        //         return true;
                        //     });
                        //    tasks.Add(task);
                        //}

                        //await Task.WhenAll(tasks.ToArray()).ContinueWith(t =>
                        //{
                        //    rabbitMQHandler.Dispose();
                        //});
                    }


                    //headers
                    {
                        //DefaultRabbitMQHandler rabbitMQHandler = new DefaultRabbitMQHandler(new RabbitMQConfig
                        //{
                        //    HostName = "192.168.157.128",
                        //    VirtualHost = "my_vhost",
                        //    UserName = "admin",
                        //    Password = "admin"
                        //});

                        //RabbitMQModel model = new RabbitMQModel(
                        //    "哈哈哈",
                        //    "test",
                        //    "textEx",
                        //    new Dictionary<string, object>
                        //    {
                        //    {"x-match","any" },
                        //    {"name","张三" },
                        //    {"pwd","123456" }
                        //    },
                        //    new Dictionary<string, object>
                        //    {
                        //   {"name","张三" }
                        //    }
                        //    );
                        //rabbitMQHandler.Publish(model);
                        //Console.WriteLine($"生产者发送消息：{model.Message}");

                        //await rabbitMQHandler.RegistReciveActionAsync("test", m =>
                        //{
                        //    Console.WriteLine($"消费者处理消息：{m}");
                        //    return true;
                        //}, true);
                    }


                    //集群测试
                    {
                        //List<string> hostNames = new List<string>
                        //{
                        //   "192.168.157.130",
                        //   "192.168.157.128",
                        //   "192.168.157.131",
                        //};
                        //foreach (var item in hostNames)
                        //{
                        //    RabbitMQConfig config = new RabbitMQConfig
                        //    {
                        //        HostName = item,
                        //        VirtualHost = "my_vhost",
                        //        UserName = "admin",
                        //        Password = "admin"
                        //    };
                        //    DefaultRabbitMQHandler rabbitMQHandler = new DefaultRabbitMQHandler(config);

                        //    RabbitMQModel model = new RabbitMQModel(
                        //    "哈哈哈",
                        //    $"test{config.HostName}",
                        //    "textEx"
                        //    );

                        //    rabbitMQHandler.Publish(model);
                        //    Console.WriteLine($"生产者发送消息：{model.Message}");

                        //    await rabbitMQHandler.RegistReciveActionAsync(model.QueueName, m =>
                        //    {
                        //        Console.WriteLine($"消费者处理消息：{m}");
                        //        return true;
                        //    }, true);

                        //}

                    }
                }

                //old测试
                {
                    //List<RabbitMQExchange> rabbitMQExchanges = Init();
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
                    //        PublishMsg msgDirect = new PublishMsg
                    //        {
                    //            ExchangeName = "TestExchange_Direct"
                    //        };

                    //        for (int i = 0; i < 5; i++)
                    //        {
                    //            msgDirect.Message = $"测试消息_00{i + 1}";
                    //            msgDirect.PublishKey = $"00{new Random().Next(1, 4)}";
                    //            rabbitMQHandler.Publish(msgDirect);
                    //            Console.WriteLine($"发送消息：{msgDirect.Message}");
                    //            Thread.Sleep(500);
                    //        }
                    //    }

                    //}
                    //Console.ReadLine();
                }


                {
                    ServiceCollection services = new ServiceCollection();
                    services.AddLogging(builder =>
                    {
                        builder.AddConsole();
                    });

                    services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                    {
                        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                        var factory = new ConnectionFactory()
                        {
                            HostName = "192.168.157.128",
                            VirtualHost = "my_vhost",
                            UserName = "admin",
                            Password = "admin"
                        };
                        return new DefaultRabbitMQPersistentConnection(logger, factory, 5);
                    });
                    
                    services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                    {
                        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();

                        var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();

                        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                        return new EventBusRabbitMQ(
                            rabbitMQPersistentConnection,
                            logger,
                            iLifetimeScope, 
                            eventBusSubcriptionsManager,
                            "TestBasket", 
                            5);
                    });
                    services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

                }

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
