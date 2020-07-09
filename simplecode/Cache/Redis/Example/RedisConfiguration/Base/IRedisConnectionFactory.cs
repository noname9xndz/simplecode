using StackExchange.Redis;

namespace RedisConfig.Base
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}