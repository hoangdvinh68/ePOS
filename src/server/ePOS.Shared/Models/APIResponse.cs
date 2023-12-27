namespace ePOS.Shared.Models;

public class APIResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }
    
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

public class APIResponse<TData>
{
    [JsonPropertyName("data")]
    public TData Data { get; set; } = default!;
}