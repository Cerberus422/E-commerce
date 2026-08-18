using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultNamespace;
[Table("Category")]
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}