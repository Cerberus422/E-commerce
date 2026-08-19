using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace api.Models;
[Table("Category")]
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}