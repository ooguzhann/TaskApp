using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskApp.Areas.Identity.Data;
using TaskApp.Models;

namespace TaskApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<TaskAppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<TaskAppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                ViewData["UserID"] = user.Id;
                // Kullan�c�n�n ad�n� kontrol et ve e�er admin ise Admin sayfas�na y�nlendir
                if (user.FirstName.ToLower() == "admin")
                {
                    return RedirectToAction("Admin");
                }
            }

            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
