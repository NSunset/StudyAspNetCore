using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQTool.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQTool.Services
{
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
        private readonly IConnectionFactory _connectionFactory;
        private readonly int _retryCount;
        private IConnection _connection;
        private bool _disponsed;

        private object syncRoot = new object();

        public DefaultRabbitMQPersistentConnection(
            ILogger<DefaultRabbitMQPersistentConnection> logger,
            IConnectionFactory connectionFactory,
            int retryCount = 5
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _retryCount = retryCount;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disponsed)
            {
                if (disposing)
                {
                    try
                    {
                        _connection.Dispose();
                        _disponsed = true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "回收RabbitMQ链接错误", null);
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disponsed;

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("没有 RabbitMQ 连接可用于执行此操作");
            }
            return _connection.CreateModel();
        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ 客户端正在尝试连接");

            lock (syncRoot)
            {
                var policy = RetryPolicy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(
                    _retryCount,
                    retryAttemp => TimeSpan.FromSeconds(
                        Math.Pow(2, retryAttemp)),
                        (ex, time) =>
                        {
                            _logger.LogInformation(
                                ex,
                                "RabbitMQ 客户端在 {TimeOut} 秒后无法连接 ({ExceptionMessage})",
                                $"{time.TotalSeconds:n1}",
                                ex.Message
                                );
                        }
                    );

                policy.Execute(() =>
                {
                    _connection = _connectionFactory.CreateConnection();
                });
                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    _logger.LogInformation($"RabbitMQ 客户端获取到“{_connection.Endpoint.HostName}”的持久连接并订阅失败事件");

                    return true;
                }
                else
                {
                    _logger.LogError("致命错误：无法创建和打开 RabbitMQ 连接");
                    return false;
                }
            }
        }

        /// <summary>
        /// 链接关闭时发出信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disponsed) return;
            _logger.LogInformation("RabbitMQ 连接已关闭。 正在尝试重新连接...");
            TryConnect();
        }

        /// <summary>
        /// 当连接调用的回调中发生异常时发出信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disponsed) return;
            _logger.LogInformation("RabbitMQ 连接抛出异常。 正在尝试重新连接...");

            TryConnect();
        }

        /// <summary>
        /// 当连接被破坏时引发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            if (_disponsed) return;
            _logger.LogInformation("RabbitMQ 连接正在关闭。 正在尝试重新连接...");

            TryConnect();
        }

    }
}
