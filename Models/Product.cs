using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mango.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [DisplayName("Category Name")]
    public string CategoryName { get; set; }

    public string Description { get; set; }

    public int Status { get; set; }

    public int CategoryId { get; set; }

    public bool IsManaged { get; set; }
}
