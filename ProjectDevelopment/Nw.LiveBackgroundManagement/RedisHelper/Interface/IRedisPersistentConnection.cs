using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisHelper.Interface
{
    public interface IRedisPersistentConnection : IDisposable
    {
        /// <summary>
        /// 已连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 尝试链接
        /// </summary>
        /// <returns></returns>
        bool TryConnect(string connectionString);

        /// <summary>
        /// 关闭连接
        /// </summary>
        void CloseConnect();

        /// <summary>
        /// 获取DB
        /// </summary>
        /// <returns></returns>
        IDatabase CreateDataBase(int db);
    }
}
