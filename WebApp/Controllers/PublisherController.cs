using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PublisherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
