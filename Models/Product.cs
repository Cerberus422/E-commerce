using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultNamespace;
[Table("Product")]

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Column(TypeName = "decimal(6,2)")]
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}