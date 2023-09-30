using Library.Models;
using Library.ViewModels;
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
        public ActionResult Index(int page = 1)
        {
            List<Book> books = _context.Books.OrderByDescending(b => b.DateAdded).ToList();
            int pageSize = 2;
            var count = books.Count();
            var items = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Books = items
            };
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            Book book = _context.Books.Include(c => c.Category).FirstOrDefault(b => b.Id == id);
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
                book.DateAdded = DateTimeOffset.UtcNow;
                _context.Add(book);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        
        public ActionResult Edit(int id)
        {
            List<Category> categories = _context.Categories.ToList();
            ViewData["Categories"] = categories;
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
