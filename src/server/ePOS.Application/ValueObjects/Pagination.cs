using System.Text.Json.Serialization;

namespace ePOS.Application.ValueObjects;

public class Pagination
{
    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; }
    
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }
    
    [JsonPropertyName("totalRecords")]
    public int TotalRecords { get; set; }
        
    [JsonPropertyName("totalPages")]
    public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);
}