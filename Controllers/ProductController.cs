﻿using Mango.Data;
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
        IEnumerable<Product> objList = _db.Product;

        foreach (var obj in objList) {
            obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
        }

        return View(objList);
    }

    // GET - Upsert (universal method for Create and Edit Product)
    public IActionResult Upsert(int? id)
    {
        Product product = new Product();
        if (id == null) {
            // this is for create
            return View(product);
        } else {
            product = _db.Product.Find(id);
            if (product == null) {
                return NotFound();
            }
            return View(product);
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
