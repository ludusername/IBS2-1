using IBS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; //dodato zbog FormsAuthentication.SignOut()(zbog odjave);

namespace IBS2.Controllers
{
    public class LoginController : Controller
    {
        #region Funkcije za popunjavenje sifara
        private void PopuniSifreKorisnikafunkcija()
        {
            SelectList items = Korisnik.PopuniSifreKorisnika();
            if (items != null)
            {
                ViewBag.korisnickesifre = items;
            }
        }
        private void PopuniSifreBanaka()
        {
            SelectList items = Korisnik.PopuniBankeID();
            if(items!=null)
            {
                ViewBag.listabanaka = items;
            }
        }
        #endregion
        #region Lista Banaka
        public static List<Banka> ListaBanaka()
        {
            InformacioniSistemBanakaEntities banakaEntities = new InformacioniSistemBanakaEntities();

            List<Banka> banke = (from b in banakaEntities.Banka
                                 select b).ToList();
            return banke;

        }
        #endregion
        #region LogIn AkcioneMetode
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogInView()
        {
            return View();
        }
        //Post:LogIn metoda

        [HttpPost]

        public ActionResult LogInView(LogInView korisnik)
        {

            if (ModelState.IsValid)
            {
                using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
                {
                    var obj = db.Korisnici.Where(a => a.NazivKorisnika.Equals(korisnik.NazivKorisnika) && a.Lozinka.Equals(korisnik.Lozinka)).FirstOrDefault();//kad ne nadje firstordefault vraca null
                    if (obj == null)
                    {
                        korisnik.LoginError = "Uneti pogrešni podaci";
                        return View("LogInView",korisnik);
                    }
                    else
                    {
                        if (obj.UlogaID == 1)
                        {
                            Session["korisnik"] = obj.NazivKorisnika;
                            return RedirectToAction("AdminView", "Admin");
                        }
                        else if (obj.UlogaID == 2)
                        {
                            Session["korisnik"] = obj.NazivKorisnika;
                            Session["korisnikId"] = obj.KorisnikId;
                           return RedirectToAction("KorisnikView");

                        }
                        else
                        {
                            Session["korisnik"] = obj.NazivKorisnika;
                            return RedirectToAction("About", "Home");
                        }

                    }

                }
            }
            return View();
        }
        #endregion
        #region Korisnik AkcioneMetode
        public ActionResult KorisnikView()
        {
            PopuniSifreKorisnikafunkcija();
            PopuniSifreBanaka();
            
            return View();
        }
        [HttpPost]
        public ActionResult KorisnikView(FormCollection korisnickeusluge)
        {
            PopuniSifreKorisnikafunkcija();
            PopuniSifreBanaka();
            if(ModelState.IsValid)
            {
                Zahtevi zahtevforma = new Zahtevi();
                Random r = new Random();
                zahtevforma.KorisnikID = int.Parse(korisnickeusluge["KorisnikID"]);
                zahtevforma.ZahtevID = r.Next();
                zahtevforma.BankaID = int.Parse(korisnickeusluge["listaBanaka12"]);
                zahtevforma.OpisZahteva = korisnickeusluge["OpisZahteva"];
                Korisnik.UbaciZahtev(zahtevforma);
                Korisnik.PrikaziZahtevePoKorisniku(zahtevforma.KorisnikID);
                TempData["brojZahteva"] = zahtevforma.ZahtevID.ToString();
                
                using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
                {
                    zahtevforma.nazivibanaka = (from b in db.Banka select b).ToList();

                    return View("KorisnikView", zahtevforma);
                }
            }
            return View();
        }
        
        public ActionResult ListaZahteva(int sifra)

        {
            PopuniSifreKorisnikafunkcija();
            List<Zahtevi> zahtevikorisnika = Korisnik.PrikaziZahtevePoKorisniku(sifra);
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {

                return View(zahtevikorisnika);
            }
        }
        
        public ActionResult ListaBanaka1()
        {
            return View();
        }
        public ActionResult ListaBanaka2()
        {
            return View();
        }
        public ActionResult ListaAktivnihBanaka()
        {
            return View();
        }
        public ActionResult ListaNEAktivnihBanaka()
        {
            return View();
        }
        public ActionResult ListaVrstahBanaka()
        {
            return View();
        }

        public ActionResult ListaStatusaBanaka()
        {
            return View();
        }
        public ActionResult ListaVlasnickeStruktureBanaka()
        {
            return View();
        }


        public ActionResult Odjava()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
#endregion
