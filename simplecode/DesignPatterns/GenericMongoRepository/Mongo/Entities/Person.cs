using System;
using GenericMongoRepository.Mongo.Infrastructure.Attributes;
using GenericMongoRepository.Mongo.Infrastructure.Models;

namespace GenericMongoRepository.Mongo.Entities
{
    [BsonCollection("people")]
    public class Person : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
