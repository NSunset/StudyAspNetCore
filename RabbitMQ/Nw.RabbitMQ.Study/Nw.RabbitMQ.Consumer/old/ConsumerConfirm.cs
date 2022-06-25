using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Nw.RabbitMQ.Consumer.old
{
    /// <summary>
    /// 消费确认
    /// </summary>
    public class ConsumerConfirm
    {
        public static void Show()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = "192.168.157.128";
            connectionFactory.VirtualHost = "my_vhost";
            connectionFactory.UserName = "admin";
            connectionFactory.Password = "admin";

            using (IConnection connection = connectionFactory.CreateConnection())
            {
                IModel channel = connection.CreateModel();
                //消费端最少要定义队列，否则可能会报错，比如生产端没有启动，rabbitMQ没有任何数据
                channel.QueueDeclare(
                    queue: "test",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );


                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                int i = 0;
                consumer.Received += (send, e) =>
                {
                    try
                    {
                        string msg = Encoding.UTF8.GetString(e.Body.ToArray());

                        Console.WriteLine($"消费端处理消息:{msg}");
                        if (i==5)
                        {
                            throw new Exception("处理消息报错了");
                        }
                        //这里是手动确认可以删除这个信息了
                        channel.BasicAck(e.DeliveryTag, false);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        //重新把消息加入队列
                        channel.BasicReject(e.DeliveryTag, true);
                        throw ex;
                    }
                    
                };
                //指定手动确认
                channel.BasicConsume("test", false, consumer);

                Console.ReadLine();
            }
        }
    }
}
