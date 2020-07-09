using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;

namespace RedisConfig.Base
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Lazy<ConnectionMultiplexer> _connection;

        public RedisConnectionFactory(IOptions<RedisConfiguration> redis)
        {
            this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redis.Value.Host));
        }

        public ConnectionMultiplexer Connection()
        {
            return this._connection.Value;
        }
    }
}