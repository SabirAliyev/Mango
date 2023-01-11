using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mango.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [DisplayName("Category Name")]
    [Required]
    public string CategoryName { get; set; }

    [MaxLength(100)]
    public string Description { get; set; }

    [Required]
    [Range(0, 5, ErrorMessage = "Status must be in range of 0 - 5")]
    public int Status { get; set; }

    public int CategoryId { get; set; }

    public bool IsManaged { get; set; }
}
