using Microsoft.AspNetCore.Identity;

namespace Mango.Models.ViewModels;

public class ProductUserVM
{
    public IdentityUser ApplicationUser { get; set; }
    public IEnumerable<Product> ProductList { get; set; }

    public ProductUserVM()
    {
        ProductList = new List<Product>();
    }
}
