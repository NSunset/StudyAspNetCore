using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nw.RabbitMQ.Tool.Init
{
    public class RabbitMQConnection
    {
        /// <summary>
        /// RabbitMQ 连接对象缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<string, IConnection> _rabbitMQConnectionCache =
            new ConcurrentDictionary<string, IConnection>();


        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="rabbitMQConfig"></param>
        /// <returns></returns>
        private static IConnection CreateConnection(RabbitMQConnectionConfig rabbitMQConfig)
        {
            try
            {
                ConnectionFactory connectionFactory = new ConnectionFactory();
                connectionFactory.HostName = rabbitMQConfig.HostName;
                connectionFactory.VirtualHost = rabbitMQConfig.VirtualHost;
                connectionFactory.UserName = rabbitMQConfig.UserName;
                connectionFactory.Password = rabbitMQConfig.Password;
                IConnection connection = connectionFactory.CreateConnection();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("链接RabbitMQ失败，请重试");
                throw ex;
            }

        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IConnection GetConnection(RabbitMQConnectionConfig config)
        {
            string key = JsonConvert.SerializeObject(config);
            var isExist =
                _rabbitMQConnectionCache.TryGetValue(key, out IConnection connection);

            if (isExist)
            {
                if (!connection.IsOpen)
                {
                    bool flage = _rabbitMQConnectionCache.TryRemove(key, out IConnection closeConnection);
                    if (flage)
                    {
                        closeConnection.Dispose();
                    }
                }
                else
                {
                    return connection;
                }
            }

            connection = CreateConnection(config);
            _rabbitMQConnectionCache[key] = connection;

            return connection;
        }

        /// <summary>
        /// 删除指定缓存链接
        /// </summary>
        /// <param name="config"></param>
        public static void Remove(RabbitMQConnectionConfig config)
        {
            if (config == null) return;
            string key = JsonConvert.SerializeObject(config);
            if (_rabbitMQConnectionCache.ContainsKey(key))
            {
                _rabbitMQConnectionCache.Remove(key, out IConnection connection);
                connection.Dispose();
            }
        }

        /// <summary>
        /// 清空所有缓存链接
        /// </summary>
        public static void RemoveAll()
        {
            foreach (var item in _rabbitMQConnectionCache)
            {
                item.Value.Dispose();
            }
            _rabbitMQConnectionCache.Clear();
        }
    }
}
