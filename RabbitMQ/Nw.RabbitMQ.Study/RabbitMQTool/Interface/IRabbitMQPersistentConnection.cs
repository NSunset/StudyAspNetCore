using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQTool.Interface
{
    /// <summary>
    /// RabbitMQ持久化链接
    /// </summary>
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        /// <summary>
        /// 已连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 尝试链接
        /// </summary>
        /// <returns></returns>
        bool TryConnect();


        /// <summary>
        /// 创建信道
        /// </summary>
        /// <returns></returns>
        IModel CreateModel();
    }
}
