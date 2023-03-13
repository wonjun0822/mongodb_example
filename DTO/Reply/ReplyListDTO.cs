namespace mongodb_example.DTO;

public record ReplyListDTO {
    public string id { get; set; } = string.Empty;
    public string comment { get; set; } = string.Empty;
} 