using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
namespace ASM_DEMO_1670.Controllers
{
    public class OrderController:Controller
	{
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
           

        
        [Route("/Orders")]
       public async Task<IActionResult> Index()
    {     
        var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
        List<Order> filteredOrders = _db.Orders.Where(order => order.Cus_ID == userID).ToList();
        return View(filteredOrders);

    }
	}
   
}
