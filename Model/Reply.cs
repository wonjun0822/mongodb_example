using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongodb_example.Model;

public class Reply : BaseEntity
{
    public Reply(string comment) {
        this.comment = comment;
    }

    [BsonElement("comment")]
    [JsonPropertyName("comment")]
    public string comment { get; set; } = string.Empty;
}