using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SampleMicroservices.Services.Catalog.Models
{
    public class BaseEntities
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }
    }
}
