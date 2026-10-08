namespace api.Helpers;

public class UserQueryObject
{
    // Sorting
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
    
    // Pagination
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}