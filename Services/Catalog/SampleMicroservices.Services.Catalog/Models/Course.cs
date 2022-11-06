using MongoDB.Bson.Serialization.Attributes;
namespace SampleMicroservices.Services.Catalog.Models
{
    public class Course:BaseEntities
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }

        public Feature Feature { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)] //Buradaki ilişkili olacak prop karşı tarafta hangi type aldıysa burada aynı tipte olmalı.
        public string CategoryId { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }
    }
}
