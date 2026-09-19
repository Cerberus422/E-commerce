using System.ComponentModel.DataAnnotations;

namespace api.Helpers;

public class CommentQueryObject
{
    // Filtering 
    [Range(1, 5)]
    public int? Rating { get; set; }
    [Range(1, 5)]
    public int? MinRating { get; set; }
    [Range(1, 5)]
    public int? MaxRating { get; set; }
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
    
    // Sorting
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
    
    // Pagination
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}