using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongodb_example.Model;

public class Comment : BaseEntity
{
    public Comment(string comment) {
        this.comment = comment;
    }

    [BsonElement("comment")]
    [JsonPropertyName("comment")]
    public string comment { get; private set; } = string.Empty;

    [BsonElement("replys")]
    [JsonPropertyName("replys")]
    public List<Reply> replys { get; private set; } = new List<Reply>();
}