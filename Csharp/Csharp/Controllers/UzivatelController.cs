using Csharp.Data;
using Csharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

namespace maturitaZkouska.Controllers
{
    public class UzivatelController : Controller
    {
        private MujDbContext _mojeDatabaze;

        public UzivatelController(MujDbContext dbContext)
        {
            _mojeDatabaze = dbContext;
        }

        [HttpGet]
        public IActionResult Registrovat()
        {
            ViewData["Titulek"] = "Registrace";

            return View();
        }

        [HttpPost]
        public IActionResult Registrovat(string jmeno, string heslo, string hesloKontrola, bool souhlas)
        {
            // chybne vyplneni formulare
            if (jmeno == null || jmeno.Trim() == "")
                return RedirectToAction("Registrovat");
            if (heslo == null || heslo.Trim() == "")
                return RedirectToAction("Registrovat");
            if (hesloKontrola == null || heslo.Trim() != hesloKontrola.Trim())
                return RedirectToAction("Registrovat");
            if (!souhlas)
                return RedirectToAction("Registrovat");

            // priprava dat z formulare
            jmeno = jmeno.Trim();
            heslo = heslo.Trim();

            // kontrola existence uzivatele s danym jmenem
            if (ExistujeUzivatel(jmeno))
                return RedirectToAction("Registrovat");

            // vytvoreni noveho uzivatele
            Uzivatel novyUzivatel = new Uzivatel() { Jmeno = jmeno, Heslo = BCrypt.Net.BCrypt.HashPassword(heslo) };

            // zapsani uzivatele do databaze
            _mojeDatabaze.Add(novyUzivatel);
            _mojeDatabaze.SaveChanges();

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

            jmeno = jmeno.Trim();
            heslo = heslo.Trim();

            if (!ExistujeUzivatel(jmeno))
                return RedirectToAction("Prihlasit");

            Uzivatel prihlasovanyUzivatel = _mojeDatabaze.Uzivatele
                .Where(u => u.Jmeno == jmeno)
                .First();

            if (!BCrypt.Net.BCrypt.Verify(heslo, prihlasovanyUzivatel.Heslo))
                return RedirectToAction("Prihlasit");

            HttpContext.Session.SetString("prihlasenyUzivatel", prihlasovanyUzivatel.Jmeno);

            return RedirectToAction("Profil");
        }

        public IActionResult Profil()
        {
            if (HttpContext.Session.GetString("prihlasenyUzivatel") == null)
                return RedirectToAction("Prihlasit");

            ViewData["Titulek"] = "Profil";
            ViewData["Uzivatel"] = HttpContext.Session.GetString("prihlasenyUzivatel");

            return View();
        }

        public IActionResult Odhlasit()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        private bool ExistujeUzivatel(string jmeno)
        {
            return _mojeDatabaze.Uzivatele
                .Where(uzivatel => uzivatel.Jmeno == jmeno)
                .FirstOrDefault() != null;
        }
    }
}
