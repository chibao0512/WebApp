using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<BookController>_logger; 
        public BookController ( ILogger<BookController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public const string CARTKEY = "cart";
        List<Cart> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(jsoncart);
            }
            return new List<Cart>();
        }
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<Cart> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
        // index
        [Route("/Owner/Book")]
        [Authorize(Roles ="Owner")]
        public IActionResult Index()
        {
            IEnumerable<Book> book = _db.books.Include(b => b.genre).ToList();
            return View(book);
        }
        public IActionResult IndexBookUser()
        {
            IEnumerable<Book> book = _db.books.Include(b => b.genre).ToList();
            return View(book);
        }
        public IActionResult BookDetail(int id)
        {
            IEnumerable<Book> book = _db.books.Include(b => b.genre).ToList();
            //var detail = book.Where(d => d.Book_Id == id).FirstOrDefault(); 
            return View(book);
            //return View(book);
        }

        public IActionResult AddToCart(int id)
        {
            var book = _db.books.Where(p => p.Book_Id == id).FirstOrDefault();
            if (book == null)
                return NotFound("No found products");

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.Book_Id == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Cart_Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new Cart() { Cart_Quantity = 1, book = book });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }
        [Route("/removecart/{id:int}", Name = "removecart")]
        public IActionResult RemoveCart(int id)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.Book_Id == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        [HttpPost]
        [Route("/updatecart", Name = "updatecart")]
        public IActionResult UpdateCart(int id, int quanty)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.Book_Id == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Cart_Quantity = quanty;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng

            return View();
        }

        /// //////////////////////////////////////////////////////////////////////////////////////



        // create
        [Route("/Owner/Book/Create")]
        [Authorize(Roles = "Owner")]
        public IActionResult Create()
        {
            ViewData["Gen_Id"] = new SelectList(_db.genres, "Gen_Id", "Gen_Name");
            return View();
        }
        [Authorize(Roles = "Owner")]
        [Route("/Owner/Book/Create")]
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
        [Authorize(Roles = "Owner")]
        [Route("/Owner/Book/Edit/{id:}")]
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
        [Authorize(Roles = "Owner")]
        [Route("/Owner/Book/Edit/{id:}")]
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
        [Authorize(Roles = "Owner")]
        [Route("/Owner/Book/Delete/{id:}")]
        public IActionResult Delete(int id)
        {
            Book book = _db.books.Find(id);
            _db.books.Remove(book);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
