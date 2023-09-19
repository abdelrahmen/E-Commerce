using E_Commerce.Models;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(CreateUserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Fullname = userVM.Fullname,
                    UserName = userVM.Username,
                    Email = userVM.Email,
                    Address = userVM.Address,
                    CreatedAt = DateTime.Now,
                };
                var userCreated = await userManager.CreateAsync(user, userVM.Password);
                if (userCreated.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, "Customer");
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
                    }
                }
                else
                {
                    foreach (var err in userCreated.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View();
        }

        //GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(userVM.Username);
                if (user != null)
                {
                    var matched = await userManager.CheckPasswordAsync(user, userVM.Password);
                    if (matched)
                    {
                        await signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        ModelState.AddModelError("", "invalid login info");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "user not found");
                }
            }
            return View(userVM);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AddAdmin()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddAdminAsync(CreateUserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Fullname = userVM.Fullname,
                    UserName = userVM.Username,
                    Email = userVM.Email,
                    Address = userVM.Address,
                    CreatedAt = DateTime.Now,
                };
                var userCreated = await userManager.CreateAsync(user, userVM.Password);
                if (userCreated.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, "Admin");
                    if (result.Succeeded)
                    {
                        TempData["msg"] = $"user [{user.UserName}] is created Successfully with the role [admin]";
                    }
                    else
                    {
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
                    }
                }
                else
                {
                    foreach (var err in userCreated.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return View(userVM);
        }
    }
}
