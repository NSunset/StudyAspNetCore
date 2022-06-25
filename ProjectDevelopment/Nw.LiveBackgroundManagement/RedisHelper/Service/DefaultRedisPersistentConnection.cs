using Microsoft.Extensions.Logging;
using RedisHelper.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisHelper.Service
{
    public class DefaultRedisPersistentConnection : IRedisPersistentConnection
    {
        private ILogger<DefaultRedisPersistentConnection> _logger;

        private IConnectionMultiplexer _connection;

        private bool _disposed;

        private object _syncRoot = new object();

        public DefaultRedisPersistentConnection(
            ILogger<DefaultRedisPersistentConnection> logger
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private string _connectionString;

        public bool IsConnected => _connection != null && _connection.IsConnected && !_disposed;

        public IDatabase CreateDataBase(int db = -1)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("没有 Redis 连接可用于执行此操作");
            }
            return _connection.GetDatabase(db);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            try
            {
                _connection.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "回收Redis链接错误");
            }
        }

        public void CloseConnect()
        {
            if (_disposed) return;
            _connection.Dispose();
        }

        public bool TryConnect(string connectionString)
        {
            _connectionString = connectionString;
            _logger.LogInformation("Redis 客户端正在尝试连接");
            lock (_syncRoot)
            {
                _connection = ConnectionMultiplexer.Connect(_connectionString);
                if (IsConnected)
                {
                    _connection.InternalError += OnInternalError;
                    _connection.ConfigurationChanged += OnConfigurationChanged;
                    _connection.ConfigurationChangedBroadcast += OnConfigurationChangedBroadcast;
                    _connection.ConnectionFailed += OnConnectionFailed;
                    _connection.HashSlotMoved += OnHashSlotMoved;
                    _connection.ErrorMessage += OnErrorMessage;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 当物理连接失败时引发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _logger.LogWarning("Redis当物理连接失败时引发，正在尝试重新连接...");
            if (_disposed) return;
            TryConnect(_connectionString);
        }

        /// <summary>
        /// 服务器回复了一条错误消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnErrorMessage(object sender, RedisErrorEventArgs e)
        {
            _logger.LogError($"Redis服务器回复一条错误消息:{e.Message}");
        }

        /// <summary>
        /// 当哈希槽被重定位时引发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            _logger.LogWarning("Redis哈希槽被重定位，正在尝试重新连接...");
            if (_disposed) return;
            TryConnect(_connectionString);
        }

        /// <summary>
        /// 当节点被明确请求通过广播重新配置时引发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            _logger.LogWarning("Redis主/副本更改，正在尝试重新连接...");
            if (_disposed) return;
            TryConnect(_connectionString);
        }

        /// <summary>
        /// 当配置发生更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConfigurationChanged(object sender, EndPointEventArgs e)
        {
            _logger.LogWarning("Redis配置发生更改，正在尝试重新连接...");
            if (_disposed) return;
            TryConnect(_connectionString);
        }

        /// <summary>
        /// 当发生内部错误时引发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInternalError(object sender, InternalErrorEventArgs e)
        {
            _logger.LogWarning("Redis发生内部错误，正在尝试重新连接...");
            if (_disposed) return;
            TryConnect(_connectionString);
        }
    }
}
