using Microsoft.AspNetCore.Mvc;

namespace maturitaZkouska.Controllers
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
            return RedirectToAction("Prihlasit");
        }

        public IActionResult Prihlasit()
        {
            ViewData["Titulek"] = "Přihlášení";

            return View();
        }
    }
}
