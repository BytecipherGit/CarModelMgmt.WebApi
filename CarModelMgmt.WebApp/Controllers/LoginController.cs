using CarModelMgmt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarModelMgmt.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMenuService _menuService;
        public LoginController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public IActionResult Index()
        {
            var roleName = User.IsInRole("Admin") ? "Admin" : "User";
            var menus = _menuService.GetMenusForUser(roleName);
            return View(menus);
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}
