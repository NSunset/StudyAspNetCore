using RedisHelper.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisHelper.Service
{
    public class RedisZSetService : BaseRedisService
    {
        private string _defaultKeyPrefix = "NW_ZSet";

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

        public RedisZSetService(
            IRedisPersistentConnection persistentConnection,
            IRedisConfigureHelper redisConfig) : base(persistentConnection, redisConfig)
        {
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd(string redisKey, string member, double score)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _database.SortedSetAdd(redisKey, member, score);
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync(string redisKey, string member, double score)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _database.SortedSetAddAsync(redisKey, member, score);
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<string> SortedSetRangeByRank(string redisKey, long start = 0L, long stop = -1L,
            Order order = Order.Ascending)
        {
            redisKey = AddKeyPrefix(redisKey);
            var a = _database.SortedSetRangeByRankWithScores(redisKey);
            a.Select(x => new { ranking = x.Element, popularity = x.Score });
            return _database.SortedSetRangeByRank(redisKey, start, stop, (Order)order).Select(x => x.ToString());
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public SortedSetEntry[] SortedSetRangeByRankWithScores(string key,
            long start = 0,
            long stop = -1,
            Order order = Order.Ascending,
            CommandFlags flags = CommandFlags.None)
        {
            string redisKey = AddKeyPrefix(key);
            return _database.SortedSetRangeByRankWithScores(redisKey, start, stop, order, flags);
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<SortedSetEntry[]> SortedSetRangeByRankWithScoresAsync(string key,
            long start = 0,
            long stop = -1,
            Order order = Order.Ascending,
            CommandFlags flags = CommandFlags.None)
        {
            string redisKey = AddKeyPrefix(key);
            return await _database.SortedSetRangeByRankWithScoresAsync(redisKey, start, stop, order, flags);
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> SortedSetRangeByRankAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return ConvertToStrings(await _database.SortedSetRangeByRankAsync(redisKey));
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public long SortedSetLength(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _database.SortedSetLength(redisKey);
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _database.SortedSetLengthAsync(redisKey);
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public bool SortedSetRemove(string redisKey, string memebr)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _database.SortedSetRemove(redisKey, memebr);
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync(string redisKey, string memebr)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _database.SortedSetRemoveAsync(redisKey, memebr);
        }

        /// <summary>
        /// 删除有序集合
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long SortedSetRemoveRangeByRank(string redisKey, long start, long stop, CommandFlags flags = CommandFlags.None)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _database.SortedSetRemoveRangeByRank(redisKey, start, stop, flags);
        }

        /// <summary>
        /// 删除有序集合
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<long> SortedSetRemoveRangeByRankAsync(string redisKey, long start, long stop, CommandFlags flags = CommandFlags.None)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await _database.SortedSetRemoveRangeByRankAsync(redisKey, start, stop, flags);
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string redisKey, T member, double score)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = Serialize(member);

            return _database.SortedSetAdd(redisKey, json, score);
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string redisKey, T member, double score)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = Serialize(member);

            return await _database.SortedSetAddAsync(redisKey, json, score);
        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double SortedSetIncrement(string redisKey, string member, double value = 1)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _database.SortedSetIncrement(redisKey, member, value);
        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<double> SortedSetIncrementAsync(string redisKey, string member, double value = 1)
        {
            redisKey = AddKeyPrefix(redisKey);
            return _database.SortedSetIncrementAsync(redisKey, member, value);
        }

     
    }
}
