using CarModelMgmt.Services.Interfaces;
using CarModelMgmt.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarModelMgmt.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuService _menuService;
        public HomeController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            //var roleName = User.IsInRole("Admin") ? "Admin" : "User";
            string receivedData = TempData["data"] as string;
            var menus = await _menuService.GetMenusForUser(receivedData);
            return View(menus);
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}