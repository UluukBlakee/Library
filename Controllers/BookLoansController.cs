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
                    ViewBag.Error = "Вы уже взяли 3 книги, надо сдать хотя бы одну книгу, чтобы получить другую!";
                else
                {
                    BookLoan bookLoan = new BookLoan
                    {
                        UserId = user.Id,
                        BookId = book.Id,
                        LoanDate = DateTimeOffset.UtcNow,
                    };
                    _context.BookLoans.Add(bookLoan);
                    _context.SaveChanges();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
