using Nest;

namespace ElasticSearchCrud.ES
{
    public interface IElasticSearchFactory
    {
        ElasticClient ESFactory();
    }
}