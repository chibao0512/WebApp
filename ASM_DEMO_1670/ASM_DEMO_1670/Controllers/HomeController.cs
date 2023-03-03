using ASM_DEMO_1670.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASM_DEMO_1670.Controllers
{
    public class HomeController : Controller

    {
        // private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult ShowBookUser()
        {
            IEnumerable<Book> book = _db.Books.Include(b => b.genre).ToList();
            return View(book);

        }
        //public IActionResult ShowDetailBook(int id)
        //{
        //    IEnumerable<Book> book = _db.Books.Include(b => b.genre).ToList();   
               
        //        var detailBook = book.Where(b => b.Book_Id == id).FirstOrDefault();
        //        return View(detailBook);
        //    }
        //}
        public async Task<IActionResult>Details(int? id)
        {
            if (id == null || _db.Books == null)
            {
                return NotFound();
            }

            var book = await _db.Books
                .Include(b => b.genre)
                .FirstOrDefaultAsync(m => m.Book_Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
