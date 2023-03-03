using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;


namespace ASM_DEMO_1670.Controllers
{
    public class ShoppingCartController : Controller
    {
        public ShoppingCartController(ILogger<ShoppingCartController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        private ILogger<ShoppingCartController> _logger;
        private ApplicationDbContext _db;
        public const string CARTKEY = "cart";
        List<CartDetail> GetCartItems()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             var KEY = CARTKEY + userId.ToString();
            var session = HttpContext.Session;
            string jsoncart = session.GetString(KEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartDetail>>(jsoncart);
            }
            return new List<CartDetail>();
        }
        void ClearCart()
        {
            List<CartDetail> cart = GetCartItems();
            cart.Clear();
            SaveCartSession(cart);
        }

        void SaveCartSession(List<CartDetail> ls)
        {
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             var KEY = CARTKEY + userId.ToString();
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(KEY, jsoncart);
        }
        public IActionResult AddToCart(int bookid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = _db.Books.Where(b => b.Book_Id == bookid).FirstOrDefault();
            if (book == null)
            {
                return NotFound("No books");
            }
            var cart = GetCartItems();
            var cartItems = cart.Find(b => b.Book.Book_Id == bookid);
            if (cartItems != null)
            {
                cartItems.Quantity++;
            }
            else
            {
                cart.Add(new CartDetail() { Quantity = 1, Book = book });
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        [Route("/removecart/{bookid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int bookid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Book.Book_Id == bookid);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int bookid, [FromForm] int quantity)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Book.Book_Id == bookid);
            if (cartitem != null)
            {
                if (quantity < 1)
                {

                    return RedirectToAction(nameof(Cart));
                }
                else
                {
                    cartitem.Quantity = quantity;
                }

            }
            SaveCartSession(cart);
            return Ok();
        }
        [Route("/Orders", Name = "orders")]
        public async Task<IActionResult> CheckOut(string userID)
        
            //kiem tra user id
            //giỏ hàng có trống
            //    kiem  tra so luong item có dủ sô lương
            {
                var orderDetails = new OrderDetail();
                var cart = GetCartItems();
                Order or = new Order();
                or.totalPrice = 0;
                or.Cus_ID = userID;
                or.CreateDate = DateTime.Now;
                or.OrderDetails = new List<OrderDetail>();
            foreach (var item in cart)
            {
                OrderDetail od = new OrderDetail();
                od.BookId = item.Book.Book_Id;
                od.Quantity = item.Quantity;
                or.totalPrice += item.Quantity * item.Book.Book_Price;

                od.order = or;
                od.Book = item.Book;
                or.OrderDetails.Add(od);

            }
                   return RedirectToAction("Index", "Order");

        }
    }
}
