using System;
using Elasticsearch.Net;
using Nest;

namespace ElasticSearchCrud.ES
{
    public class ElasticSearchFactory : IElasticSearchFactory
    {
        public ElasticClient ESFactory()
        {
            var nodes = new Uri[]
            {
                new Uri("http://localhost:9200/"),
            };

            var connectionPool = new StaticConnectionPool(nodes);
            var connectionSettings = new ConnectionSettings(connectionPool).DisableDirectStreaming();
            var elasticClient = new ElasticClient(connectionSettings);

            return elasticClient;
        }
    }
}