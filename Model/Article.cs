using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace mongodb_example.Model;

public class Article : BaseEntity
{
    public Article(string title, string? content) {
        this.title = title;
        this.content = content;
    }

    [BsonElement("title")]
    [JsonPropertyName("title")]
    public string title { get; set; } = string.Empty;

    [BsonElement("content")]
    [JsonPropertyName("content")]
    public string? content { get; set; }
}