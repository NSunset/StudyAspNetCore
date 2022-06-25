using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Consumer.old
{
    /// <summary>
    /// 简单消费测试
    /// </summary>
    public class SimpleConsumer
    {

        public static void Show()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string tag = path.Split('/', '\\').Last(s => !string.IsNullOrEmpty(s));
            Console.WriteLine($"这里是{tag}启动了");

            ConnectionFactory factory = new ConnectionFactory();

            factory.HostName = "192.168.157.128";
            factory.VirtualHost = "my_vhost";
            factory.UserName = "admin";
            factory.Password = "admin";

            using (IConnection connection = factory.CreateConnection())
            {
                IModel channel = connection.CreateModel();
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                //队列名称
                string queueName = "nwTest";

                //声明队列,生产端有声明， 消费端也有声明，但是名称一样不影响。如果消费端不声明，那么可能消费端会报错。比如消费端先启动了，但是生产端启动。没有队列
                channel.QueueDeclare(
                    queue: queueName,
                    durable: true,//支持持久化
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                Console.ForegroundColor = ConsoleColor.Green;

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                //手动通知RabbitMQ已经正确消费了消息，删除消息
                {
                    consumer.Received += (send, e) =>
                    {
                        ReadOnlyMemory<byte> body = e.Body;
                        string message = Encoding.UTF8.GetString(body.ToArray());

                        Console.WriteLine($"{tag}接收消息：{message}");
                        //手动标记消息已经正确处理了
                        channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
                    };

                    //使用消费者
                    string consumerTag = channel.BasicConsume(
                          queue: queueName,
                          autoAck: false,
                          consumer: consumer
                          );

                }


                //自动删除消息
                {
                    //consumer.Received += (send, e) =>
                    //{
                    //    ReadOnlyMemory<byte> body = e.Body;
                    //    string message = Encoding.UTF8.GetString(body.ToArray());

                    //    Console.WriteLine($"{tag}接收消息：{message}");
                    //};

                    ////使用消费者
                    //channel.BasicConsume(
                    //      queue: queueName,
                    //      autoAck: true,//自动删除消息，不管消息是否正确处理
                    //      consumer: consumer
                    //      );
                }

                Console.ReadLine();
            }
        }
    }
}
