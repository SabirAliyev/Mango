using Mango.Data;
using Mango.Models;
using Mango.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        IEnumerable<Product> objList = _db.Product;

        foreach (var obj in objList) {
            obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
        }

        return View(objList);
    }

    // GET - Upsert (universal method for Create and Edit Product)
    public IActionResult Upsert(int? id)
    {
        //// Get Categories list of Product for dropdown list
        //IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
        //{
        //    Text = i.Name,
        //    Value = i.Id.ToString()
        //});

        //// Get the dynamic ViewBag
        //ViewBag.CategoryDropDown = CategoryDropDown;

        // Product product = new Product();

        ProductVM productVM = new ProductVM()
        {
            Product = new Product(),
            CategorySelectList = _db.Category.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            })
        };


        if (id == null) {
            // this is for create
            return View(productVM);
        } else {
            productVM.Product = _db.Product.Find(id);
            if (productVM.Product == null) {
                return NotFound();
            }
            return View(productVM);
        }
    }

    //POST - Upsert
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Product obj)
    {
        // Server side validation
        if (ModelState.IsValid) {
            _db.Product.Add(obj);
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
