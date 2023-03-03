using ASM_DEMO_1670.ListRole;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM_DEMO_1670.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public AdminController(RoleManager<IdentityRole> roleManager, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
        }
        [Route("Admin/DSRole")]
        //list all role create by user
        public IActionResult DSRole()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole model)
        {
            // avoid dupliacte role
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("DSRole");

        }
        [Route("Admin/DSUser")]
        public async Task<IActionResult> DSUser()
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
        [Route("Admin/DSOwner")]
        public async Task<IActionResult> DSOwner()
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
        [Route("Admin/DSOwner/EditOwner/{id:}")]
        public IActionResult EditOwner(string id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("DSOwner");
            }
            ViewData["id"] = id;
            return View();
        }

        [HttpPost]
        [Route("Admin/DSOwner/EditOwner/{id:}")]
        public async Task<IActionResult> EditOwner(string ownerid, ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var owner = await _userManager.FindByIdAsync(ownerid);
                var result = await _userManager.ChangePasswordAsync(owner, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    TempData["Password"] = "Invalid password!";
                    return View(model);
                }

                return RedirectToAction("DSOwner");
            }
            else
            {
                return View(model);
            }

        }

        [Route("Admin/DSUser/EditUser/{id:}")]
        public IActionResult EditUser(string id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("DSUser");
            }
            ViewData["id"] = id;
            return View();
        }


        [HttpPost]
        [Route("Admin/DSUser/EditUser/{id:}")]
        public async Task<IActionResult> EditUser(string userid, ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userid);
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    TempData["Password"] = "Invalid password!";
                    return View(model);
                }
                return RedirectToAction("DSUser");

            }
            else
            {
                return View(model);
            }

        }
        public IActionResult Index()
        {
            return View();
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
                    PhoneNumber = owners.Phone,


                };

                var result = await _userManager.CreateAsync(owner, owners.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(owner, Role.Owner.ToString());
                    return RedirectToAction("ShowOwner");
                }
                else
                {
                    TempData["Fail"] = "RegisterOwner Fail!";
                    return RedirectToAction("RegisterOwner");
                }
            }
            return RedirectToAction("RegisterOwner");
        }
    }
}

