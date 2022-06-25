using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedisHelper.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisHelper.Service
{
    public abstract class BaseRedisService : IDisposable
    {
        private IRedisPersistentConnection _redisPersistentConnection;
        protected IDatabase _database;

        /// <summary>
        /// 默认的 Key 值（用来当作 RedisKey 的前缀）
        /// </summary>
        protected abstract string DefaultKeyPrefix { get; set; }

        public BaseRedisService(
            IRedisPersistentConnection redisPersistentConnection,
            IRedisConfigureHelper redisHelper,
            int db = -1
            )
        {
            _redisPersistentConnection = redisPersistentConnection;
            string connection = redisHelper.GetRedisConnectionString();
            _database = CreateDb(connection, db);
        }

        public virtual void SetConnection(string connectionString, int db = -1)
        {
            if (_database != null)
            {
                _database = null;
            }
            if (_redisPersistentConnection != null)
            {
                _redisPersistentConnection.CloseConnect();
            }
            _database = CreateDb(connectionString, db);
        }

        private IDatabase CreateDb(string connection, int db)
        {
            if (!_redisPersistentConnection.IsConnected)
            {
                _redisPersistentConnection.TryConnect(connection);
            }

            var database = _redisPersistentConnection.CreateDataBase(db);
            return database;
        }


        public void Dispose()
        {
            if (_database != null)
            {
                _database = null;
            }
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
    }
}
