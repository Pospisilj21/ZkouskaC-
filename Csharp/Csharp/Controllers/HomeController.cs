﻿using Microsoft.AspNetCore.Mvc;

namespace _10_ASP_NET_Core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Titulek"] = "Hlavní stránka";

            return View();
        }
    }
}
