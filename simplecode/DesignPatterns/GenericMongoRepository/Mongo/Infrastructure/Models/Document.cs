using System;
using GenericMongoRepository.Mongo.Models;
using MongoDB.Bson;

namespace GenericMongoRepository.Mongo.Infrastructure.Models
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
        public DateTime UpdatedDate { get; set; }
    }
}
