using System.Text.Json.Serialization;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using mongodb_example.DTO;

namespace mongodb_example.Model;

public class Article : BaseEntity
{
    public Article(ArticleWriteDTO dto) {
        this.title = dto.title;
        this.content = dto.content;
    }
    
    [BsonElement("title")]
    [JsonPropertyName("title")]
    public string title { get; } = string.Empty;

    [BsonElement("content")]
    [JsonPropertyName("content")]
    public string? content { get; }

    [BsonElement("comments")]
    [JsonPropertyName("comments")]
    public List<Comment> comments { get; } = new List<Comment>();
}