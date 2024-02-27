using CarModelMgmt.Core.Entities;
using CarModelMgmt.Services.Interfaces;
using CarModelMgmt.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarModelMgmt.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMenuService _menuService;
        public AccountController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            // Your existing login logic to verify credentials

            var users = await _menuService.Login(model);

            foreach (var user in users)
            {
                string userRole = user.Role;
               

                if (user != null && user.Role != null)
                {
                    string roles = user.Role;
                    if (roles.Contains("Admin"))
                    {
                        // Redirect to Admin Dashboard
                        TempData["data"] = "Admin";
                        TempData["Sucess"] = "Admin Login Sucessfully";
                        return RedirectToAction("Index", "Home");
                    }
                    else if (roles.Contains("User"))
                    {
                        TempData["data"] = "User";
                        // Redirect to User Dashboard
                        TempData["Sucess"] = "User Login Sucessfully";
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View();
            }

            return View();

            // If role not found or any other condition, return to the default returnUrl

        }
    }
}
