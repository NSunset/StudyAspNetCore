using Newtonsoft.Json;
using Nw.Cache.Redis.Init;
using Nw.Cache.Redis.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Cache.Redis.Service
{
    public class RedisSubscriberService : RedisBase
    {
        private string _defaultKeyPrefix = "NW_Subscriber";

        protected override string DefaultKeyPrefix
        {
            get
            {
                return _defaultKeyPrefix;
            }
            set
            {
                _defaultKeyPrefix = value;
            }
        }

        private IConnectionMultiplexer ConnMultiplexer;

        public RedisSubscriberService(IRedisConfig redisConfig) : base(redisConfig)
        {
            ConnMultiplexer = RedisConnection.GetConnectionMultiplexer(connectionString);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public long Publish(string redisKey, RedisValue message)
        {
            redisKey = AddKeyPrefix(redisKey);
            var sub = ConnMultiplexer.GetSubscriber();
            return sub.Publish(redisKey, message);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<long> PublishAsync(string redisKey, RedisValue message)
        {
            redisKey = AddKeyPrefix(redisKey);
            var sub = ConnMultiplexer.GetSubscriber();
            return await sub.PublishAsync(redisKey, message);
        }

        /// <summary>
        /// 发布（使用序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public long Publish<T>(string redisKey, T message)
        {
            redisKey = AddKeyPrefix(redisKey);
            var sub = ConnMultiplexer.GetSubscriber();
            return sub.Publish(redisKey, Serialize(message));
        }

        /// <summary>
        /// 发布（使用序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<long> PublishAsync<T>(string redisKey, T message)
        {
            redisKey = AddKeyPrefix(redisKey);
            var sub = ConnMultiplexer.GetSubscriber();
            return await sub.PublishAsync(redisKey, Serialize(message));
        }

        #region 不是并发时使用
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handle"></param>
        public void Subscribe(string redisKey, Action<RedisChannel, RedisValue> handle)
        {
            redisKey = AddKeyPrefix(redisKey);
            var sub = ConnMultiplexer.GetSubscriber();
            sub.Subscribe(redisKey, handle);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handle"></param>
        public async Task SubscribeAsync(string redisKey, Action<RedisChannel, RedisValue> handle)
        {
            redisKey = AddKeyPrefix(redisKey);
            var sub = ConnMultiplexer.GetSubscriber();
            await sub.SubscribeAsync(redisKey, handle);
        }

        #endregion

        #region 并发调用时使用

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handle"></param>
        public void Subscribe(string redisKey, Action<ChannelMessage> action)
        {
            redisKey = AddKeyPrefix(redisKey);
            var channelMessageQueue = ConnMultiplexer.GetSubscriber().Subscribe(redisKey);
            channelMessageQueue.OnMessage(action);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handle"></param>
        public void Subscribe(string redisKey, Func<ChannelMessage, Task> func)
        {
            redisKey = AddKeyPrefix(redisKey);
            var channelMessageQueue = ConnMultiplexer.GetSubscriber().Subscribe(redisKey);
            channelMessageQueue.OnMessage(func);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (ConnMultiplexer != null)
            {
                ConnMultiplexer.Dispose();
                ConnMultiplexer = null;

                RedisConnection.RemoveConnectionMultiplexer(connectionString);
            }

            base.Dispose(disposing);
        }

    }
}
