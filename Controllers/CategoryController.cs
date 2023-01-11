using Mango.Data;
using Mango.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Controllers;

public class CategoryController : Controller
{
    public readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> categories = _db.Category;

        return View(categories);
    }

    // GET - Create
    public IActionResult Create()
    {
        return View();
    }


}
