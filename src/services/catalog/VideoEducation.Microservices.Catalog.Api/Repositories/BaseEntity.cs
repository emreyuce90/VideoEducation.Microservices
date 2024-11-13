using MongoDB.Bson.Serialization.Attributes;

namespace VideoEducation.Microservices.Catalog.Api.Repositories {
    public class BaseEntity {
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
