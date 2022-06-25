using Nw.RabbitMQ.Tool.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Tool.Init
{
    public class RabbitMQInitialize
    {
        /// <summary>
        /// RabbitMQ初始化所有交换机和队列的绑定信息
        /// </summary>
        /// <param name="rabbitMQExchanges"></param>
        /// <param name="mqConnectionConfif"></param>
        public static void Initialize(
            List<RabbitMQExchange> rabbitMQExchanges, 
            RabbitMQConnectionConfig mqConnectionConfif)
        {
            using (IModel channel = RabbitMQConnection.GetConnection(mqConnectionConfif).CreateModel())
            {
                foreach (RabbitMQExchange item in rabbitMQExchanges)
                {
                    ///声明交换机
                    channel.ExchangeDeclare(
                    exchange: item.ExchangeName,
                    type: item.ExchangeType,
                    //是否持久化
                    durable: true,
                    //连接关闭后，是否删除
                    autoDelete: false,
                    arguments: null
                    );

                    foreach (RabbitMQEqeue queue in item.RabbitMQEqeue)
                    {
                        //声明队列
                        channel.QueueDeclare(
                            queue: queue.QueueName,
                            //是否持久化
                            durable: true,
                            //连接关闭后，队列是否删除
                            exclusive: false,
                            //当最后一个消费者取消订阅时（如果有消费者）,是否自动删除队列
                            autoDelete: false,
                            arguments: null
                            );

                        //绑定交换机和队列
                        channel.QueueBind(
                            queue: queue.QueueName,
                            exchange: item.ExchangeName,
                            routingKey: queue.Key,
                            arguments: queue.BindArguments
                            );
                    }
                }
            }
            
        }
    }
}
