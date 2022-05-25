using Entity_1.Data;
using Entity_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Entity_1.Controllers
{
    public class CategoryController : Controller
    {
        readonly DataContext _context;
        public CategoryController(DataContext categoryContext)
        {
            _context = categoryContext;
        }
        public IActionResult Index([FromQuery] string filter)
        {
            ViewData["filter"] = filter;
            if (string.IsNullOrEmpty(filter)) 
            {
                return View(_context.Category.ToList());
            }
            else
            {
                var cat = _context.Category.Where(p => p.Name.ToLower().Contains(filter.ToLower())
                || p.Code.ToLower().Contains(filter.ToLower()));
                return View(cat.ToList());
            }
        }
        public IActionResult Edit(int id)
        {
            return View(_context.Category.SingleOrDefault(p => p.CategoryId == id));

        }
        public IActionResult Create()
        {
            return View("Edit",new Category());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }
                else
                {
                    if (category.CategoryId == 0)
                    
                        _context.Category.Add(category);
                    _context.Category.Update(category);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                

            }
            catch (Exception)
            {

                return View(category);
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var cat = _context.Category.SingleOrDefault(p => p.CategoryId == id);
                if (cat != null)
                {
                    _context.Category.Remove(cat);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
        
    }
}
