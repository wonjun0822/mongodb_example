using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace mongodb_example.Model;

public class Article : BaseEntity
{
    [BsonElement("title")]
    [JsonPropertyName("title")]
    public string title { get; set; } = string.Empty;

    [BsonElement("content")]
    [JsonPropertyName("content")]
    public string? content { get; set; }

    [BsonElement("comments")]
    [JsonPropertyName("comments")]
    public List<Comment> comments { get; private set; } = new List<Comment>();
}