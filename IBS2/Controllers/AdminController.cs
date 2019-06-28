using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBS2.Models;
using System.Globalization;//dodato zbog parsiranja pravilnog

namespace IBS2.Controllers
{
    public class AdminController : Controller
    {
        #region Funkcije za popunusifara
        private void PopuniSifreBanaka()
        {
            SelectList items = Admin.PopuniBankeID();
            if (items != null)
            {
                ViewBag.banke = items;
            }
        }

        private void PopuniStatuseBanaka()
        {
            SelectList items = Admin.PopuniStatusBanke();
            if (items != null)
            {
                ViewBag.statusibanke = items;
            }
        }
        private void PopuniVrsteBanaka()
        {
            SelectList items = Admin.PopuniVrsteBanaka();
            if (items != null)
            {
                ViewBag.vrstebanaka = items;
            }
        }
        private void PopuniLicence()
        {
            SelectList items = Admin.PopuniStatusLicence();
            {
                ViewBag.licencebanke = items;
            }
        }
        private void PopuniSifreBanaka1()
        {
            SelectList items = Admin.PopuniBankeID();
            if (items != null)
            {
                ViewBag.banke1 = items;
            }
        }
        private void PopuniSifrebanakaVlasnickeStrukture()
        {
            SelectList items = Admin.PopuniBankeIDVlasnickaStruktura();
            if (items != null)
            {
                ViewBag.bankevlasnickastruktura = items;
            }
        }
        #endregion
        #region Akcione metode
        // GET: Admin
        public ActionResult AdminView()
        {
            PopuniSifreBanaka();
            PopuniStatuseBanaka();
            PopuniVrsteBanaka();
            PopuniLicence();
            PopuniSifreBanaka1();
            PopuniSifrebanakaVlasnickeStrukture();
            return View();
        }
        [HttpPost]
        public ActionResult AdminView(FormCollection banka)
        {
            PopuniSifreBanaka1();
            PopuniSifreBanaka();
            PopuniStatuseBanaka();
            PopuniVrsteBanaka();
            PopuniLicence();
            PopuniSifrebanakaVlasnickeStrukture();

            if (ModelState.IsValid)
            {
                Licenca l = new Licenca();
                l.DatumLicence = DateTime.Now;
                l.StatusLicence = 1;
                Random r = new Random();
                l.LicencaID = r.Next();
                Admin.GenerisiLicencuId(l);

                Banka nov = new Banka();
                nov.Naziv = banka["Naziv"];
                nov.Sediste = banka["Sediste"];
                nov.GodinaOsnivanja = int.Parse(banka["GodinaOsnivanja"]);
                if (String.IsNullOrEmpty(banka["GodisnjiProfit"]))
                {
                    nov.Godisnjiprofit = null;

                }
                else
                {

                    nov.Godisnjiprofit = decimal.Parse(banka["GodisnjiProfit"], System.Globalization.CultureInfo.InvariantCulture);//dodato da bi parsirao kako treba jer inace je brojeve predstavljao kao int
                }
                if (String.IsNullOrEmpty(banka["UkupnaAktivaiUkupniDug"]))
                {
                    nov.UkupnaAktivaiUkupniDug = null;
                }
                else
                {
                    nov.Godisnjiprofit = decimal.Parse(banka["UkupnaAktivaiUkupniDug"], System.Globalization.CultureInfo.InvariantCulture);
                }
                nov.LicencaID = l.LicencaID;
                nov.Vlasnistvo = banka["Vlasnistvo"];
                nov.VrstaID = int.Parse(banka["VrstaBanke"]);
                nov.StatusID = int.Parse(banka["StatusBanke"]);
                nov.BankaID = r.Next();
                Admin.DodajBanku(nov);
                VlasnickaStrukturaBanke vlasnistvo = new VlasnickaStrukturaBanke();
                vlasnistvo.BankaID = nov.BankaID;
                vlasnistvo.VlasniciBanke = null;
                vlasnistvo.Procenat = null;
                Admin.DodajVlasnistvo(vlasnistvo);
                TempData["Naziv"] = nov.Naziv;
                TempData["Vlasnistvo"] = nov.Vlasnistvo;





                return RedirectToAction("AdminView");
            }
            return View();
        }
        [HttpPost]
        public ActionResult PrikaziBanku(FormCollection banka)
        {
            if (ModelState.IsValid)
            {
                switch (banka["BtnSubmit"])
                {
                    case "Promeni status licence":
                        {
                            bool uspeh = Admin.BrisiBanku(int.Parse(banka["listaBanaka"]));
                            int status = Admin.VratiStatusLicence(int.Parse(banka["listaBanaka"]));
                            if (status == 1)
                            {
                                TempData["Sifra"] = "aktivna";
                            }
                            if (status == 0)
                            {
                                TempData["Sifra"] = "neaktivna";
                            }

                            return RedirectToAction("AdminView");
                        }
                    case "Prikaži":
                        {
                            Banka prikazi = new Banka();
                            prikazi = Admin.PrikaziBankupoId(int.Parse(banka["listaBanaka"]));
                            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
                            {
                                prikazi.vrstabanke1 = (from b in db.VrstaBanke select b).ToList();
                                prikazi.statusbanke1 = (from b in db.StatusBanke select b).ToList();

                                return View("AdminPrikaziView", prikazi);
                            }
                        }
                    default: return View("AdminView");
                }


            }
            else
            {
                return View("AdminView");
            }
        }
        [HttpPost]
        public ActionResult AdminPrikaziView(Banka banka)//akciona metoda za izmenu podataka banke
        {

            if (ModelState.IsValid)
            {
                Admin.IzmeniBanku(banka);
                TempData["Izmena"] = banka.Naziv;

                return RedirectToAction("AdminView", "Admin");
            }
            else
            {
                using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
                {
                    banka.statusbanke1 = (from b in db.StatusBanke select b).ToList();
                    banka.vrstabanke1 = (from b in db.VrstaBanke select b).ToList();

                }
                return View(banka);
            }

        }
        [HttpPost]
        public ActionResult PrikaziVlasnistvoView(FormCollection vlasnistvo1)
        {


            VlasnickaStrukturaBanakaViewModel vlasnici = new VlasnickaStrukturaBanakaViewModel();
            vlasnici = Admin.VlasnistvoBanke(int.Parse(vlasnistvo1["listaBanaka1"]));
            int a = (int.Parse(vlasnistvo1["listaBanaka1"]));
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {
                vlasnici.nazivibanaka = (from b in db.Banka where b.BankaID != a select b).ToList();

                return View("AdminPrikaziVlasnistvoView", vlasnici);
            }



        }
        [HttpPost]
        public ActionResult AdminPrikaziVlasnistvoView(VlasnickaStrukturaBanke vlasnici)//akciona metoda za promenu vlasnicke strukture banka
        {

            if (ModelState.IsValid)
            {
                Admin.IzmeniVlasnistvo1(vlasnici);
                TempData["IzmenaVlasnici"] = vlasnici.BankaID;

                return RedirectToAction("AdminView", "Admin");
            }
            else
            {
                using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
                {
                    vlasnici.nazivibanaka = (from b in db.Banka select b).ToList();
                    
                }
                return View(vlasnici);
            }
        }
        public ActionResult PregledZahteva()
        {
            return View();
}
        
        public  ActionResult PrikaziZahtevPoSifri(int sifra)//Pregled zahteva u pogledu pregled zahteva
        {
            KombinovaniViewModel prikazizahtevposifri = Admin.PrikaziZahtevPoSifri(sifra);
            TempData["Provera"] = 1;
            TempData["ZahtevID"] = prikazizahtevposifri.ZahtevID.ToString();
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
                return View("PregledZahteva",prikazizahtevposifri);
        }
    }
}
#endregion

