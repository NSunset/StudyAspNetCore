using System;
using System.Collections.Generic;
using System.Text;

namespace Nw.Structural
{
    /// <summary>
    /// Redis和IDbHelp的适配器，让两个类可以一起使用
    /// </summary>
    public class RedisAdapter : IDbHelp
    {
        //通过组合的形式，实现适配。比继承好的地方是还可以适配RedisHelp的子类
        private readonly RedisHelp _redis = null;
        public RedisAdapter(RedisHelp redisHelp)
        {
            _redis = redisHelp;
        }
        public void Add<T>(T t)
        {
            _redis.RedisAdd(t);
        }

        public void Delete<T>(T t)
        {
            _redis.Delete(t);
        }

        public void Find(int id)
        {
            _redis.Find(id);
        }

        public void Update<T>(T t)
        {
            _redis.Update(t);
        }
    }
}
