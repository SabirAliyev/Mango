using Mango.Data;
using Mango.Models;
using Mango.Utility; // using our own implementation
using Microsoft.AspNetCore.Mvc;

namespace Mango.Controllers;

public class CartController : Controller
{
    public readonly ApplicationDbContext _db;

    public CartController(ApplicationDbContext db)
    {
        _db= db;
    }


    public IActionResult Index()
    {
        List<ShoppingCart> shoppingCarttList = new List<ShoppingCart>();
        if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null 
            && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0) 
        {
            shoppingCarttList = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).ToList();
        }
        
        List<int> prodsInCart = shoppingCarttList.Select(i => i.ProductId).ToList();

        // Get products list from DB.
        IEnumerable<Product> productList = _db.Product.Where(u => prodsInCart.Contains(u.Id));

        return View(productList);
    }

    public IActionResult Remove(int id)
    {
        List<ShoppingCart> shoppingCarttList = new List<ShoppingCart>();
        if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
            && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Count() > 0) {
            shoppingCarttList = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).ToList();
        }

        // remove approptiate product from cart list
        shoppingCarttList.Remove(shoppingCarttList.FirstOrDefault(u => u.ProductId == id));

        // update shopping cart list
        HttpContext.Session.Set(WebConstants.SessionCart, shoppingCarttList);

        // redirect to Action Index
        return RedirectToAction(nameof(Index));
    }

}
