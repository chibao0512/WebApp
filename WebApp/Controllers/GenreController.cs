using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles ="Owner")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GenreController(ApplicationDbContext db)
        {
            _db = db;
        }

        // index
        [Route("/Owner/Genre")]
        public IActionResult Index()
        {
            IEnumerable<Genre> genre = _db.genres.ToList();
            return View(genre);
        }

        // create
        [Route("/Owner/Genre/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("/Owner/Genre/Create")]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _db.genres.Add(genre);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // edit
        [Route("/Owner/Genre/Edit/{id:}")]
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
        [Route("/Owner/Genre/Edit/{id:}")]
        public IActionResult Edit(Genre genre, int id)
        {
            if (ModelState.IsValid)
            {
                genre.Gen_Id = id;
                _db.genres.Update(genre);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // delete
        [Route("/Owner/Genre/Delete/{id:}")]
        public IActionResult Delete(int id)
        {
            Genre genre = _db.genres.Find(id);
            _db.genres.Remove(genre);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}