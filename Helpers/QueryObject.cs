namespace api.Helpers;

public class QueryObject
{
    // Filtering 
    public string? Name { get; set; } = null;
    public int? CategoryId { get; set; } = null;
    public bool? InStock { get; set; } 
    
    // Sorting
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
}