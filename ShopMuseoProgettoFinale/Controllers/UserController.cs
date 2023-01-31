﻿using Microsoft.AspNetCore.Mvc;
using ShopMuseoProgettoFinale.Models;
using System.Diagnostics;

namespace ShopMuseoProgettoFinale.Controllers {
    public class UserController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Buy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}