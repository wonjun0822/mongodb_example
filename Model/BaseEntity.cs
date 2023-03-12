using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongodb_example.Model;

public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; protected set; } = ObjectId.GenerateNewId().ToString();
}