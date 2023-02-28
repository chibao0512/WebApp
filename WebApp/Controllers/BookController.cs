using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db; // đối tượng _db do mình khởi tạo 
        public BookController(ApplicationDbContext db)
        {
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
        public IActionResult Index()
        {
            //IEnumerable<Book> ds = _db.books.Join(_db.genres, b => b.Genre_Id,

            //    g => g.Gen_Id).where
            //var ds = from b in _db.books
            //         join g in _db.genres on b.Genre_Id equals g.Gen_Id
            //         join p in _db.publishers on b.Publisher_Id equals p.Publisher_Id
            //         select new { b.Book_Id, b.Book_Title, b.Book_Image, g.Gen_Name };                    

            // muoon lay nao thi lay do ra het 

            //IEnumerable<Book> books =_db.books.Join(_db.genres, b=>b.Genre_Id, g=b=>g.Genre_Name) where 
            //    join g in _db.genres on b.Genre_Id equals g.Genre_Id
            //             select new { b.Book_Id, b.Book_Title, b.Book_Image, b.Book_Author, b.Book_Price, b.Book_Description };
            //return Ok(ds);


            IEnumerable<Book> books = _db.books.Include(p => p.genre).ToList();
            return View(books);
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
        public IActionResult UpdateCart(int id, int quantities)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.Book_Id == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Cart_Quantity = quantities;
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
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

       
           

        public IActionResult Create()
            {
                ViewData["Genre_Id"] = new SelectList(_db.genres, "Genre_Id", "Genre_Name");
                return View();
            }
            [HttpPost]
            public IActionResult Create(Book obj)
            {
                if (ModelState.IsValid) // nó check toàn bộ thông tin nếu đúng mới chạy còn không thì thôi,
                                        // nó chặn lại ngay chỗ bakcend. Nếu như trình duyệt nó tắt javascrip không có cái này thì 
                                        // sẽ không an toàn 
                {
                    _db.books.Add(obj); // add to database
                    _db.SaveChanges(); // luuw lai 
                    return RedirectToAction("Index"); // quay laij trang index
                }
                return View(obj);

            }
        public IActionResult Edit(int id)
        {
            ViewData["Genre_Id"] = new SelectList(_db.genres, "Genre_Id", "Genre_Name");
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
        public string UploadFile(Book book)
        {
            string uniqueFileName = null;

            if (book.Book_Image != null)
            {
                string uploadsFoder = Path.Combine("wwwroot", "uploads");
                uniqueFileName = Guid.NewGuid().ToString() + book.Book_Image.FileName;
                string filePath = Path.Combine(uploadsFoder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    book.Book_Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public async Task<IEnumerable<Book>> GetBooks()
        {
            IEnumerable<Book> books = await (from book in _db.books
                                             join genre in _db.genres
                                             on book.Genre_Id equals genre.Genre_Id
                                             select new Book
                                             {
                                                 Book_Id = book.Book_Id,
                                                 Book_Title = book.Book_Title,
                                                 Book_Image = book.Book_Image,
                                                 genre = book.genre,
                                                 Book_Author= book.Book_Author,
                                                 Book_Price = book.Book_Price,
                                                 Book_Description = book.Book_Description,
                                                
                                             }
                         ).ToListAsync();
            return books;

        }
      
        public IActionResult Edit(int id)
        {
            Book book = _db.books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["Genre_Id"] = new SelectList(_db.genres, "Genre_Id", "Genre_Name");
            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(int id, Book book, string img)
        {
            book.Book_Id = id;
            if (ModelState.IsValid)
            {
                  string uniqueFileName = UploadFile(book);
                              _db.books.Update(book);
                _db.SaveChanges();

                img = Path.Combine("wwwroot", "uploads", img);
                FileInfo infor = new FileInfo(img);
                if (infor != null)
                {
                    System.IO.File.Delete(img);
                    infor.Delete();
                }

                return RedirectToAction("Index");
            }

            return View(book);
        }

      
        public IActionResult Delete(int id)
        {
            Book book = _db.books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _db.books.Remove(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }



        public ActionResult Delete(int id, string img)
        {
            Book book = _db.books.Find(id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                img = Path.Combine("wwwroot", "uploads", img);
                FileInfo infor = new FileInfo(img);
                if (infor != null)
                {
                    System.IO.File.Delete(img);
                    infor.Delete();
                }
                _db.books.Remove(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }

}
