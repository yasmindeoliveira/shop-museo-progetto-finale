using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Models;

namespace ShopMuseoProgettoFinale.Controllers {
    public class UserController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Buy() {
            return View();
        }

        public IActionResult LikesTest() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}