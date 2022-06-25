using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Nw.RabbitMQ.Producer.old
{
    /// <summary>
    /// 持久化消息，确认发送成功
    /// </summary>
    public class PersistenceAndConfirmationSend
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

                string queueTest = "test";
                channel.QueueDeclare(
                    queue: queueTest,
                    //持久化
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                string exchangeTest = "exchangeTest";
                channel.ExchangeDeclare(
                    exchange: exchangeTest,
                    type: ExchangeType.Direct,
                    //持久化
                    durable: true,
                    autoDelete: false,
                    arguments: null
                    );

                string key = "001";
                channel.QueueBind(
                    queue: queueTest,
                    exchange: exchangeTest,
                    routingKey: key,
                    arguments: null
                    );

                IBasicProperties basicProperties = channel.CreateBasicProperties();
                //持久化
                basicProperties.Persistent = true;//false不持久化

                for (int i = 0; i < 10; i++)
                {
                    //开启事务
                    channel.TxSelect();
                    try
                    {
                        string msg = $"测试队列数据持久化{i + 1}";
                        byte[] body = Encoding.UTF8.GetBytes(msg);

                        //发送消息
                        channel.BasicPublish(
                            exchange: exchangeTest,
                            routingKey: key,
                            basicProperties: basicProperties,
                            body: body
                            );

                        //如果中途出错，则所有信息回滚，都不发送,否则全部发送成功
                        //if (i == 5)
                        //    throw new Exception("报错了");

                        Console.WriteLine($"发送消息:{msg}");
                        //没问题，事务提交
                        channel.TxCommit();
                    }
                    catch (Exception ex)
                    {
                        //事务回滚
                        channel.TxRollback();
                        throw ex;
                    }
                    
                }
            }
        }
    }
}
