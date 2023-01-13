using Mango.Data;
using Mango.Models;
using Mango.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mango.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> objList = _db.Product.Include(u=>u.Category);

        //foreach (var obj in objList) {
        //    obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
        //}
        
        return View(objList);
    }

    // GET - Upsert (universal method for Create and Edit Product)
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new ProductVM()
        {
            Product = new Product(),
            CategorySelectList = _db.Category.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
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
    public IActionResult Upsert(ProductVM productVM)
    {
        ModelState.Remove("Product.Category");
        ModelState.Remove("Product.Image");
        // Temporary ModelState Validation fix - TODO in Product ViewModel.

        if (ModelState.IsValid) {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;

            if (productVM.Product.Id == 0) {
                // creating
                string upload = webRootPath + WebConstants.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extention = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create)) {
                    files[0].CopyTo(fileStream);
                }

                productVM.Product.Image = fileName + extention;

                _db.Product.Add(productVM.Product);

            }
            else {
                // update
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        return View();
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
