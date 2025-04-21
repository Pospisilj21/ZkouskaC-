using Microsoft.AspNetCore.Mvc;

namespace _10_ASP_NET_Core.Controllers
{
    public class UzivatelController : Controller
    {
        [HttpGet]
        public IActionResult Registrovat()
        {
            ViewData["Titulek"] = "Registrace";

            return View();
        }

        [HttpPost]
        public IActionResult Registrovat(string jmeno, string heslo, string hesloKontrola)
        {
            if (jmeno == null || jmeno.Trim() == "")
                return RedirectToAction("Registrovat");
            if (heslo == null || heslo.Trim() == "")
                return RedirectToAction("Registrovat");
            if (heslo != hesloKontrola)
                return RedirectToAction("Registrovat");

            // prace s databazi

            return RedirectToAction("Prihlasit");
        }

        [HttpGet]
        public IActionResult Prihlasit()
        {
            ViewData["Titulek"] = "Přihlášení";

            return View();
        }

        [HttpPost]
        public IActionResult Prihlasit(string jmeno, string heslo)
        {
            if (jmeno == null || jmeno.Trim() == "")
                return RedirectToAction("Prihlasit");
            if (heslo == null || heslo.Trim() == "")
                return RedirectToAction("Prihlasit");

            return RedirectToAction("Profil");
        }

        public IActionResult Profil()
        {
            ViewData["Titulek"] = "Profil";
            ViewData["Uzivatel"] = "abcd";

            return View();
        }
    }
}
