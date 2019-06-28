using IBS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBS2.Controllers
{
    public class RegisterController : Controller
    {
        #region Register AkcioneMetode
        private InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();
        // GET: Register
        public ActionResult Register()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult Register(Korisnici korisnik)
        {
            if (ModelState.IsValid)
            {
                if (db.Korisnici.Any(x => x.NazivKorisnika == korisnik.NazivKorisnika || x.Email == korisnik.Email))//lamda zapis koji proverava da li postoji vec username ili email
                {
                    ViewBag.DuplicateMessage = "Korisničko ime ili e-mail su zauzeti";
                    return View();
                }
                else
                {
                    korisnik.UlogaID = 2;
                    Random r = new Random();//klasa koja generise random broj
                    korisnik.KorisnikId = r.Next();//tako generisemo id korisnika


                    db.Korisnici.Add(korisnik);
                    db.SaveChanges();
                    ViewBag.SuccessMessage = "Registracija je uspela. ";
                    return View("Register");
                }

            }
            return View();
        }
    }

}
#endregion
