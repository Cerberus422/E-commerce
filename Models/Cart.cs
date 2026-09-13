using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;
[Table("Cart")]

public class Cart
{
    public int Id { get; set; }
    
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public List<CartItem> CartItems { get; set; } = new();
}