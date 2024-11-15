using MongoDB.Bson.Serialization.Attributes;

public class Feature {
    [BsonElement("educatorFullName")]
    public string EducatorFullName { get; set; } = default!;

    [BsonElement("duration")]
    public int Duration { get; set; }

    [BsonElement("rating")]
    public double Rating { get; set; }
}
