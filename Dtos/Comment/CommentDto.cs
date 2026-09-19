namespace api.Dtos.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedOn { get; set; }
    
    public int ProductId { get; set;}
    public string ProductName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
}