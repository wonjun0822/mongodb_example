namespace mongodb_example.DTO;

public record ArticleListDTO {
    public string id { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
}