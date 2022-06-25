using Nw.Cache.Redis.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Cache.Redis.Service
{
    /// <summary>
    /// key-value 键值对:value可以是序列化的数据
    /// </summary>
    public class RedisStringService : RedisBase
    {
        private string _defaultKeyPrefix = "NW_String";

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

        public RedisStringService(IRedisConfig redisConfig) : base(redisConfig)
        {
        }

        /// <summary>
        /// 设置 key 并保存字符串（如果 key 已存在，则覆盖值）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.StringSet(redisKey, redisValue, expiry);
        }

        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.StringSetAsync(redisKey, redisValue, expiry);
        }

        /// <summary>
        /// 保存多个 Key-value
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public bool Set(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(AddKeyPrefix(x.Key), x.Value));
            return Db.StringSet(pairs.ToArray());
        }

        /// <summary>
        /// 保存一组字符串值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(AddKeyPrefix(x.Key), x.Value));
            return await Db.StringSetAsync(pairs.ToArray());
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public string Get(string redisKey, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.StringGet(redisKey);
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.StringGetAsync(redisKey);
        }



        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = Serialize(redisValue);
            return Db.StringSet(redisKey, json, expiry);
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = Serialize(redisValue);
            return await Db.StringSetAsync(redisKey, json, expiry);
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public T Get<T>(string redisKey, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Deserialize<T>(Db.StringGet(redisKey));
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string redisKey, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Deserialize<T>(await Db.StringGetAsync(redisKey));
        }


        /// <summary>
        /// 递增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public double Increment(string redisKey, double value, CommandFlags flags = CommandFlags.None)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.StringIncrement(redisKey, value, flags);
        }

        /// <summary>
        /// 递增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<double> IncrementAsync(string redisKey, double value, CommandFlags flags = CommandFlags.None)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.StringIncrementAsync(redisKey, value, flags);
        }

        /// <summary>
        /// 递减
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public double Decrement(string redisKey, double value, CommandFlags flags = CommandFlags.None)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.StringDecrement(redisKey, value, flags);
        }

        /// <summary>
        /// 递减
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<double> DecrementAsync(string redisKey, double value, CommandFlags flags = CommandFlags.None)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.StringDecrementAsync(redisKey, value, flags);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="redisKey"></param
        /// <returns></returns>
        public bool Remove(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.KeyDelete(redisKey);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="redisKey"></param
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.KeyDeleteAsync(redisKey);
        }
    }
}
