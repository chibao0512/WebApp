using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PublisherController : Controller
    {
            private readonly ApplicationDbContext _db;
            public PublisherController(ApplicationDbContext db)
            {
                _db = db;
            }

            // index
            public IActionResult Index()
            {
                IEnumerable<Publisher> Publisher = _db.publishers.ToList();
                return View(Publisher);
            }

            // create
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Create(Publisher Publisher)
            {
                if (ModelState.IsValid)
                {
                    _db.publishers.Add(Publisher);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(Publisher);
            }

            // edit
            public IActionResult Edit(int id)
            {
            Publisher Publisher = _db.publishers.Find(id);
                if (Publisher == null)
                {
                    return RedirectToAction("Index");
                }
                return View(Publisher);
            }
            [HttpPost]
            public IActionResult Edit(Publisher Publisher, int id)
            {
                if (ModelState.IsValid)
                {
                Publisher.Publisher_Id = id;
                    _db.publishers.Update(Publisher);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(Publisher);
            }

            // delete
            public IActionResult Delete(int id)
            {
            Publisher Publisher = _db.publishers.Find(id);
                _db.publishers.Remove(Publisher);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
    
    }
}
