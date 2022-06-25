using Nw.Cache.Redis.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Cache.Redis.Init
{
    /// <summary>
    /// Redis管理中心   创建Redis链接
    /// </summary>
    public class RedisConnection
    {
        /// <summary>
        /// redis 连接对象缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<string, IConnectionMultiplexer> redisConnectionCache =
            new ConcurrentDictionary<string, IConnectionMultiplexer>();


        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IConnectionMultiplexer GetConnectionMultiplexer(string connectionString)
        {
            var isExist =
                redisConnectionCache.TryGetValue(connectionString, out IConnectionMultiplexer connMultiplexer);

            if (isExist && connMultiplexer.IsConnected)
                return connMultiplexer;

            connMultiplexer = GetConnection(connectionString);
            redisConnectionCache[connectionString] = connMultiplexer;

            return connMultiplexer;
        }

        /// <summary>
        /// 删除缓存连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static void RemoveConnectionMultiplexer(string connectionString)
        {
            redisConnectionCache.TryRemove(connectionString, out IConnectionMultiplexer connMultiplexer);
        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IDatabase GetDatabase(string connectionString, int db = -1)
        {
            return GetConnectionMultiplexer(connectionString).GetDatabase(db);
        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static ConnectionMultiplexer GetConnection(string connectionString)
        {
            var connect = ConnectionMultiplexer.Connect(connectionString);
            return AddRegisterEvent(connect);
        }

        #region 注册事件

        /// <summary>
        /// 添加注册事件
        /// </summary>
        private static ConnectionMultiplexer AddRegisterEvent(ConnectionMultiplexer connectionMultiplexer)
        {
            connectionMultiplexer.ConnectionRestored += ConnMultiplexer_ConnectionRestored;
            connectionMultiplexer.ConnectionFailed += ConnMultiplexer_ConnectionFailed;
            connectionMultiplexer.ErrorMessage += ConnMultiplexer_ErrorMessage;
            connectionMultiplexer.ConfigurationChanged += ConnMultiplexer_ConfigurationChanged;
            connectionMultiplexer.HashSlotMoved += ConnMultiplexer_HashSlotMoved;
            connectionMultiplexer.InternalError += ConnMultiplexer_InternalError;
            connectionMultiplexer.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;

            return connectionMultiplexer;
        }

        /// <summary>
        /// 重新配置广播时（通常意味着主从同步更改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            //LogHelper.Info($"Redis重新配置广播：{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生内部错误时（主要用于调试）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_InternalError(object sender, InternalErrorEventArgs e)
        {
            //LogHelper.Error($"Redis发生内部错误：{nameof(ConnMultiplexer_InternalError)}: {e.Exception}");
        }

        /// <summary>
        /// 更改集群时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            //LogHelper.Info($"Redis更改集群：{nameof(ConnMultiplexer_HashSlotMoved)}: {nameof(e.OldEndPoint)}-{e.OldEndPoint} To {nameof(e.NewEndPoint)}-{e.NewEndPoint}, ");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            //LogHelper.Info($"Redis配置更改：{nameof(ConnMultiplexer_ConfigurationChanged)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            //LogHelper.Error($"Redis发生错误：{nameof(ConnMultiplexer_ErrorMessage)}: {e.Message}");
        }

        /// <summary>
        /// 物理连接失败时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            //LogHelper.Error($"Redis物理连接失败：{nameof(ConnMultiplexer_ConnectionFailed)}: {e.Exception}");
        }

        /// <summary>
        /// 建立物理连接时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            //LogHelper.Info($"Redis建立物理连接：{nameof(ConnMultiplexer_ConnectionRestored)}: {e.Exception}");
        }

        #endregion 注册事件

    }
}
