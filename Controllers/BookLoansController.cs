using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class BookLoansController : Controller
    {
        private readonly LibraryContext _context;
        public BookLoansController(LibraryContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            List<BookLoan> bookLoans = _context.BookLoans.Include(b => b.Book).Include(u => u.User).ToList();
            return View(bookLoans);
        }

        public ActionResult Create(int bookId)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id ==  bookId);
            if (book != null)
                return View(new BookLoan() { Book = book });
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int bookId, string email)
        {
            User user = _context.Users.FirstOrDefault(u => u.Email == email);
            Book book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (user != null && book != null)
            {
                int numberOfBooks = _context.BookLoans.Where(bl => bl.UserId == user.Id && bl.ReturnDate == null).Count();
                if (numberOfBooks == 3)
                {
                    TempData["Error"] = "Вы уже взяли 3 книги, надо сдать хотя бы одну книгу, чтобы получить другую!";
                    return RedirectToAction("Create", "BookLoans", new { bookId = bookId });
                }
                else
                {
                    TempData["Error"] = " ";
                    book.Status = "Выдано";
                    BookLoan bookLoan = new BookLoan
                    {
                        UserId = user.Id,
                        BookId = book.Id,
                        LoanDate = DateTimeOffset.UtcNow,
                    };
                    _context.BookLoans.Add(bookLoan);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["Error"] = "Пользователь с таким email не найден или книга не существует.";
            return RedirectToAction("Create", "BookLoans", new { bookId = bookId });
        }
        public ActionResult GetBooksByUser()
        {
            return View();
        }

        [HttpPost]        
        public ActionResult PersonalCabinet(string email)
        {
            User user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                ViewBag.Error = " ";
                List<Book> booksOnLoan = _context.BookLoans
                    .Where(bl => bl.UserId == user.Id && bl.ReturnDate == null)
                    .Select(bl => bl.Book)
                    .ToList();

                return View(booksOnLoan);
            }
            else
            {
                ViewBag.Error = "Пользователь с таким email не найден.";
                return View("GetBooksByUser");
            }
        }
        public ActionResult ReturnBook(int bookId)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.Status = "В наличии";
            }
            BookLoan bookLoan = _context.BookLoans.FirstOrDefault(bl => bl.BookId == bookId && bl.ReturnDate == null);
            if (bookLoan != null)
            {
                bookLoan.ReturnDate = DateTimeOffset.UtcNow;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
