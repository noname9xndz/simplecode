using StackExchange.Redis;

namespace RedisConfig.Service
{
    public interface IRedisGenericService<T>
    {
        T Get(string key);

        void Delete(string key);

        void Save(string key, T obj);
    }
}