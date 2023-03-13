namespace mongodb_example.DTO;

/// <summary>
/// 게시글 작성 정보
/// </summary>
public record ArticleWriteDTO
{
    public string title { get; set; } = string.Empty;
    public string content { get; set; } = string.Empty;
}
