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
    public IActionResult Create(Product obj)
    {
        // Seerver side validation
        if (ModelState.IsValid) {
            _db.Product.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    // GET - Edit
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0) {
            return View();
        }

        var obj = _db.Product.Find(id);
        if (obj == null) {
            return View();
        }

        return View(obj);
    }

    //POST - Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product obj)
    {
        if (ModelState.IsValid) {
            _db.Product.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(obj);
    }

    // GET - Delete
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0) {
            return View();
        }

        var obj = _db.Product.Find(id);
        if (obj == null) {
            return View();
        }

        return View(obj);
    }

    //POST - Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int? id)
    {
        var obj = _db.Product.Find(id);
        if (obj == null) {
            return NotFound();
        }

        _db.Product.Remove(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }


}
