using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Constants;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {

            return View();
        }
        [Route("Admin/ShowUser")]
        public async Task<IActionResult> ShowUser()
        {
            var user = await (from users in _db.Users
                              join UserRole in _db.UserRoles
                              on users.Id equals UserRole.UserId
                              join role in _db.Roles
                              on UserRole.RoleId equals role.Id
                              where role.Name == "User"
                              select users).ToListAsync();
            return View(user);
        }

        [Route("Admin/ShowOwner")]
        public async Task<IActionResult> ShowOwner()
        {
            var owner = await (from users in _db.Users
                               join UserRole in _db.UserRoles
                               on users.Id equals UserRole.UserId
                               join role in _db.Roles
                               on UserRole.RoleId equals role.Id
                               where role.Name == "Owner"
                               select users).ToListAsync();
            return View(owner);
        }
        [Route("Admin/RegisterOwner")]
        public async Task<IActionResult> RegisterOwner()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/RegisterOwner")]
        public async Task<IActionResult> RegisterOwner(Owner owners)
        {
            if (ModelState.IsValid)
            {
                var owner = new ApplicationUser
                {
                    UserName = owners.Email,
                    Acc_Name = owners.Name,
                    Email = owners.Email,

                };
                var result = await _userManager.CreateAsync(owner, owners.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(owner, Role.Owner.ToString());
                }
                else
                {
                    return RedirectToAction("Register");
                }
            }
            return RedirectToAction("ShowOwner");
        }
    }
}
