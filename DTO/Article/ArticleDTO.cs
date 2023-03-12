namespace mongodb_example.DTO;

public record ArticleDTO {
    /// <summary>
    /// 게시글 ID
    /// </summary>
    public string id { get; set; } = string.Empty;

    /// <summary>
    /// 제목
    /// </summary>
    public string title { get; set; } = string.Empty;
}
