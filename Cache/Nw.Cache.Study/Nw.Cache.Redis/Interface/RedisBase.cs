
using Newtonsoft.Json;
using Nw.Cache.Redis.Init;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.Cache.Redis.Interface
{
    /// <summary>
    /// RedisBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisBase : IDisposable
    {
        /// <summary>
        /// 默认的 Key 值（用来当作 RedisKey 的前缀）
        /// </summary>
        protected abstract string DefaultKeyPrefix { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        protected IDatabase Db;

        protected string connectionString;
        protected int DbNum = -1;

        protected RedisBase(IRedisConfig redisConfig)
        {
            connectionString = redisConfig.GetRedisConnectionString();

            Db = RedisConnection.GetDatabase(connectionString);
        }

        public virtual void SetConnection(string connectionString, int dbNum = -1)
        {
            this.connectionString = connectionString;
            this.DbNum = dbNum;
            Db = RedisConnection.GetDatabase(connectionString, dbNum);
        }

        public virtual void SetDatabase(int db)
        {
            DbNum = db;
            Db = RedisConnection.GetDatabase(connectionString, DbNum);
        }

        /// <summary>
        /// 添加 Key 的前缀
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual string AddKeyPrefix(string key)
        {
            return $"{DefaultKeyPrefix}:{key}";
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        protected static IEnumerable<string> ConvertToStrings<T>(IEnumerable<T> list) where T : struct
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            return list.Select(x => x.ToString());
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            string text = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(text);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default(T);


            string text = Encoding.UTF8.GetString(data);
            T result = JsonConvert.DeserializeObject<T>(text);
            return result;
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
