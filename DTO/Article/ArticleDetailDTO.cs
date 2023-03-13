using MongoDB.Bson;

namespace mongodb_example.DTO;

public record ArticleDetailDTO {
    public string id { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public string? content { get; set; } = string.Empty;
    public List<CommentListDTO>? comments { get; set; } = new List<CommentListDTO>();
}