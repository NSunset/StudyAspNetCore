using Nw.RabbitMQ.Tool.Init;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.RabbitMQ.Tool.Interface
{
    public abstract class RabbitMQBase : IDisposable
    {
        protected static RabbitMQConnectionConfig MQConfig;

        protected IModel Channel;

        protected RabbitMQBase(RabbitMQConnectionConfig config)
        {
            MQConfig = config;
            Channel = GetChannel();
        }

        /// <summary>
        /// 获取RabbitMQ链接
        /// </summary>
        /// <returns></returns>
        protected IModel GetChannel()
        {
            if (Channel != null && Channel.IsOpen)
            {
                return Channel;
            }
            IConnection connection = RabbitMQConnection.GetConnection(MQConfig);
            Channel = connection.CreateModel();
            return Channel;
        }

        private static object ch = new object();
        /// <summary>
        /// 信道操作
        /// </summary>
        /// <param name="channelHandler"></param>
        protected void ChannelHandler(Action<IModel> channelHandler)
        {
            lock (ch)
            {
                IModel channel = GetChannel();
                channelHandler(channel);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="exchangeName"></param>
        /// <param name="exchangeType"></param>
        /// <param name="msg"></param>
        public virtual void Publish(PublishMsg requestMsg)
        {
            ChannelHandler(channel =>
            {
                try
                {
                    //开启事务
                    channel.TxSelect();

                    byte[] body = Encoding.UTF8.GetBytes(requestMsg.Message);

                    IBasicProperties basicProperties = channel.CreateBasicProperties();
                    //持久化
                    basicProperties.Persistent = true;
                    if (requestMsg.PublishHeaders != null)
                    {
                        basicProperties.Headers = requestMsg.PublishHeaders;
                    }

                    channel.BasicPublish(
                        exchange: requestMsg.ExchangeName,
                        routingKey: requestMsg.PublishKey,
                        mandatory: true,
                        basicProperties: basicProperties,
                        body: body
                        );
                    channel.TxCommit();//提交


                }
                catch (Exception ex)
                {
                    //事务回滚
                    channel.TxRollback();
                    Console.WriteLine("发送消息失败，请重试");
                    throw ex;
                }
            });
        }

        /// <summary>
        /// 注册处理动作，处理完消息后，关闭当前连接
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="func"></param>
        /// <param name="isCloseCon">是否关闭当前连接</param>
        /// <returns></returns>
        public async Task RegistReciveActionAsync(string queueName, Func<string, bool> func, bool isCloseCon = false)
        {
            Task task = Task.Factory.StartNew(() =>
             {
                 ChannelHandler(channel =>
                 {
                     try
                     {
                         QueueDeclareOk declareOk = channel.QueueDeclarePassive(queueName);
                         uint messageCount = declareOk.MessageCount;

                         var consumer = new EventingBasicConsumer(channel);
                         channel.BasicQos(0, 0, true);

                         consumer.Received += (sender, ea) =>
                         {
                             messageCount--;
                             string str = Encoding.UTF8.GetString(ea.Body.ToArray());
                             if (func(str))
                             {
                                 channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);//确认已消费
                                 //Console.WriteLine($"消费端处理消息:{str}");
                             }
                             else
                             {
                                 channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: true);//放回队列--重新包装信息，放入其他队列
                             }
                         };

                         string consumerTag = channel.BasicConsume(queue: queueName,
                                               autoAck: false,//不ACK
                                               consumer: consumer);
                         while (messageCount > 0)
                         {
                             System.Threading.Thread.Sleep(300);
                         }
                     }
                     catch (Exception ex)
                     {

                         throw ex;
                     }
                 });
             });
            if (isCloseCon)
            {
                await task.ContinueWith(t =>
                  {
                      Dispose();
                  });
            }
            else
            {
                await task;
            }
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Channel.Dispose();
                    Channel = null;
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// 关闭当前连接
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
