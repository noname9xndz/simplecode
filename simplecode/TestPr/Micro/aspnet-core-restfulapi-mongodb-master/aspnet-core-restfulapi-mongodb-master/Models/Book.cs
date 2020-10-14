using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AwesomeAPI.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}