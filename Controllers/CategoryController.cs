﻿using Mango.Data;
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

    // POST - Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        // Server side validation
        if (ModelState.IsValid) {
            _db.Category.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        return View(obj);
    }

    // GET - Edit
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0) {
            return NotFound();
        }

        var obj = _db.Category.Find(id);
        if (obj == null) {
            return NotFound();
        }


        return View(obj);
    }

    // POST - Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        // Server side validation
        if (ModelState.IsValid) {
            _db.Category.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(obj);
    }
}
