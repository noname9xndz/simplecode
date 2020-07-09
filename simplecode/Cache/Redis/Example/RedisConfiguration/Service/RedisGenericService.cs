using System;
using RedisConfig.Base;
using StackExchange.Redis;

namespace RedisConfig.Service
{
    public class RedisGenericService<T> : BaseService<T>, IRedisGenericService<T>
    {
        internal readonly IDatabase Db;
        protected readonly IRedisConnectionFactory ConnectionFactory;

        public RedisGenericService(IRedisConnectionFactory connectionFactory)
        {
            this.ConnectionFactory = connectionFactory;
            this.Db = GetCurrentRedisDatabase();
        }

        public void Delete(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key.Contains(":")) throw new ArgumentException("invalid key");

            key = this.GenerateKey(key);
            this.Db.KeyDelete(key);
        }

        public T Get(string key)
        {
            key = this.GenerateKey(key);
            var hash = this.Db.HashGetAll(key);
            return this.MapFromHash(hash);
        }

        public void Save(string key, T obj)
        {
            if (obj != null)
            {
                var hash = this.GenerateHash(obj);
                key = this.GenerateKey(key);

                if (this.Db.HashLength(key) == 0)
                {
                    this.Db.HashSet(key, hash);
                }
                else
                {
                    var props = this.Properties;
                    foreach (var item in props)
                    {
                        if (this.Db.HashExists(key, item.Name))
                        {
                            this.Db.HashIncrement(key, item.Name, Convert.ToInt32(item.GetValue(obj)));
                        }
                    }
                }
            }
        }

        private IDatabase GetCurrentRedisDatabase()
        {
            return this.ConnectionFactory.Connection().GetDatabase();
        }
    }
}