using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CategoriesContoller : Controller
    {
        private readonly LibraryContext _context;
        public CategoriesContoller(LibraryContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (category != null)
            {
                _context.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public ActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
                return View(category);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (category != null)
            {
                _context.Update(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public ActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
                return View(category);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category category)
        {
            if (category != null)
            {
                _context.Remove(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
