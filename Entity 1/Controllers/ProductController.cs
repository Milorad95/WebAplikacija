﻿using Entity_1.Data;
using Entity_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Entity_1.Controllers
{
    public class ProductController : Controller
    {
        readonly DataContext _context;
        readonly string _ImagePath;
        public ProductController(DataContext productContext, IWebHostEnvironment env)
        {
            _context = productContext;
            _ImagePath = env.ContentRootPath + @"\wwwroot\images\";
        }
        public IActionResult Index([FromQuery] string filter, int[] categories)
        {
            if (filter == null)
            {
                filter = "";
            }
            var cat = _context.Category.ToList();
            IEnumerable<Product> product;
            if (string.IsNullOrEmpty(filter) && !categories.Any())
            {
                product = _context.Product.ToList();
            }
            else
            {
                product = _context.Product.Where(p => p.Name.ToLower().Contains(filter.ToLower())
                || p.Code.ToLower().Contains(filter.ToLower()));
                product = product.Where(p => (!categories.Any() || categories.Contains(p.Category.CategoryId)));
            }

            var categoryFilter = cat.Select(p => new CategoryFilter
            {
                Id = p.CategoryId,
                Name = p.Name,
                Selected = categories.Contains(p.CategoryId)


            });

            return View(new ProductListView
            {
                CategoryFilter = categoryFilter,
                Filter = filter,
                Products = product
            });
        }
        public IActionResult Create()
        {
            var categories = _context.Category.ToList();
            ViewData["categories"] = new SelectList(categories, "CategoryId", "Name");
            return View("Edit", new Product());
        }
        public IActionResult Edit(int id)
        {
            var categories = _context.Category.ToList();
            ViewData["categories"] = new SelectList(categories, "CategoryId", "Name");

            return View(_context.Product.SingleOrDefault(p => p.ProductId == id));
        }
        public IActionResult Details(int id)
        {
            var product = _context.Product.Include(a => a.Category)
                .SingleOrDefault(p => p.ProductId == id);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(product);

                if (_context.Product.Any(p => p.Code == product.Code && p.ProductId != product.ProductId))
                {
                    var categories = _context.Category.ToList();
                    ViewData["categories"] = new SelectList(categories, "CategoryId", "Name");

                    ModelState.AddModelError("Code", "Sifra vec postoji");
                    return View(product);
                }


                if (product.ProductId == 0)
                    _context.Product.Add(product);
                else
                {
                    product.Category = null;
                    _context.Product.Update(product);
                }


                if (_context.Product.Any(p => p.Code == product.Code && p.ProductId != product.ProductId))
                {
                    var categories = _context.Category.ToList();
                    ViewData["categories"] = new SelectList(categories, "CategoryId", "Name");

                    ModelState.AddModelError("Code", "Sifra vec postoji");
                    return View(product);
                }
                if (product.NewImage != null)
                {
                    if (product.NewImage.Length > 0)
                    {
                        using (var stream = System.IO.File.Create(_ImagePath + product.NewImage.FileName))
                        {
                            product.NewImage.CopyTo(stream);
                        }
                    }
                    DeleteImage(product.ImageName);
                    product.ImageName = product.NewImage.FileName;
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {

                return View(product);
            }


        }
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Product.SingleOrDefault(p => p.ProductId == id);
                if (product != null)
                {
                    _context.Product.Remove(product);
                    _context.SaveChanges();

                    DeleteImage(product.ImageName);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public void DeleteImage(string imageName)
        {
            var fullImagePath = _ImagePath + imageName;
            if (System.IO.File.Exists(fullImagePath) && !imageName.EndsWith("_nd.jpg"))
            {
                System.IO.File.Delete(fullImagePath);
            }

        }
    }
}
