namespace DShop.Monolith.Infrastructure.Mongo
{
    public class MongoDbOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool Seed { get; set; }        
    }
}