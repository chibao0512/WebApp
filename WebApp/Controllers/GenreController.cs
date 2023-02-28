using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GenreController(ApplicationDbContext db)
        {
            _db = db;
        }

        // index
        public IActionResult Index()
        {
            IEnumerable<Genre> genres =_db.genres.ToList();
            return View(genres);
        }

        // create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre gen)
        {
            if (ModelState.IsValid)
            {
                _db.genres.Add(gen);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gen);
        }

        // edit
        public IActionResult Edit(int id)
        {
            Genre genre = _db.genres.Find(id);
            if (genre == null)
            {
                return RedirectToAction("Index");
            }
            return View(genre);
        }
        [HttpPost]
        public IActionResult Edit(Genre genre, int id)
        {
            if (ModelState.IsValid)
            {
                genre.Genre_Id = id;
                _db.genres.Update(genre);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // delete
        public IActionResult Delete(int id)
        {
            Genre genre = _db.genres.Find(id);
            _db.genres.Remove(genre);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
