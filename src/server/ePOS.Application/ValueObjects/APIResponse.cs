using System.Text.Json.Serialization;

namespace ePOS.Application.ValueObjects;

public class APIResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }
    
    [JsonPropertyName("message"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }
}

public class APIResponse<TData> : APIResponse
{
    [JsonPropertyName("data")]
    public TData Data { get; set; } = default!;
}