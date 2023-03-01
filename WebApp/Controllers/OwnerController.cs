using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class OwnerController : Controller
    {
        [Authorize(Roles ="Owner")]
        [Route("/Owner/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
