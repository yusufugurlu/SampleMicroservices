using MongoDB.Bson.Serialization.Attributes;

namespace SampleMicroservices.Services.Catalog.Models
{
    public class Category:BaseEntities
    {
        public string Name { get; set; }
    }
}
