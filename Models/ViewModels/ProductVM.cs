using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Models.ViewModels;

public class ProductVM
{
    public Product Product { get; set; }

    public IEnumerable<SelectListItem> CategorySelectList { set; get; }
}
