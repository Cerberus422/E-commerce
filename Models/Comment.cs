using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

[Table("Comments")]
public class Comment
{
    public int id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    
    public int ProductId { get; set;}
    public Product Product { get; set; }
    
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}