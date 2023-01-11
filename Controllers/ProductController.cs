using Mango.Data;
using Mango.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Controllers;

public class ProductController : Controller
{
    public readonly ApplicationDbContext _db;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> products = _db.Product;
        return View(products);
    }

    // GET - Create
    public IActionResult Create() 
    {
        return View();
    }

    //POST - Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product prod) 
    { 
        _db.Product.Add(prod);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}
