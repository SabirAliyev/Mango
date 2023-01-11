using System.ComponentModel.DataAnnotations;

namespace Mango.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    public int Name  { get; set; }

    public string DisplayOrder { get; set; }
}
