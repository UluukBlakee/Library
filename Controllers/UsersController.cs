using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class UsersController : Controller
    {
        private readonly LibraryContext _context;
        public UsersController(LibraryContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            List<User> users = _context.Users.ToList();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (user != null)
            {
                _context.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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
        public ActionResult Edit(User user)
        {
            if (user != null)
            {
                _context.Update(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
                return View(user);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User user)
        {
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
