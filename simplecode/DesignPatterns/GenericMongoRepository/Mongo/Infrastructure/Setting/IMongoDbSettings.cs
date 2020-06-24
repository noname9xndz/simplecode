namespace GenericMongoRepository.Mongo.Infrastructure.Setting
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
