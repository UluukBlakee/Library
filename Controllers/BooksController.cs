using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;
        public BooksController (LibraryContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            List<Book> books = _context.Books.OrderBy(b => b.DateAdded).ToList();
            return View(books);
        }

        public ActionResult Details(int id)
        {
            Book book = _context.Books.FirstOrDefault(t => t.Id == id);
            if (book != null)
                return View(book);
            return NotFound();
        }

        public ActionResult Create()
        {
            List<Category> categories = _context.Categories.ToList();
            ViewData["Categories"] = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (book != null)
            {
                book.Status = "В наличии";
                book.DateAdded = DateTime.UtcNow;
                _context.Add(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        
        public ActionResult Edit(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
                return View(book);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (book != null)
            {
                _context.Update(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public ActionResult Delete(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
                return View(book);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Book book)
        {
            if (book != null)
            {
                _context.Remove(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
    }
}
