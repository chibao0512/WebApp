using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        // index
        public IActionResult Index()
        {
            IEnumerable<Book> book = _db.books.Include(b => b.genre).ToList();
            return View(book);
        }

        // create
        public IActionResult Create()
        {
            ViewData["Gen_Id"] = new SelectList(_db.genres, "Gen_Id", "Gen_Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.books.Add(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // edit
        public IActionResult Edit(int id)
        {
           ViewData["Gen_Id"] = new SelectList(_db.genres, "Gen_Id", "Gen_Name");
           Book book = _db.books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book, int id)
        {
            if (ModelState.IsValid)
            {
                book.Book_Id = id;
                _db.books.Update(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // delete
        public IActionResult Delete(int id)
        {
            Book book = _db.books.Find(id);
            _db.books.Remove(book);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
