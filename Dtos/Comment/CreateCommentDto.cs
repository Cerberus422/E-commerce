using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment;

public class CreateCommentDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Title must be more than 3 characters")]
    [MaxLength(20, ErrorMessage = "Title must be less than 20 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(3, ErrorMessage = "Content must be more than 3 characters")]
    [MaxLength(400, ErrorMessage = "Content must be less than 400 characters")]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    [Range(1,5)]
    public int Rating { get; set; }
    
}