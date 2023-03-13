namespace mongodb_example.DTO;

public record CommentListDTO {
    public string id { get; set; } = string.Empty;
    public string comment { get; set; } = string.Empty;
    public List<ReplyListDTO> replys { get; set; } = new List<ReplyListDTO>();
} 