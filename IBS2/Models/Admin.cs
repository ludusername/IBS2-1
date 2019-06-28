using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBS2.Models
{
    public class Admin
    {
        #region Admin-Metode
        public static void GenerisiLicencuId(Licenca licenca)
        {
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {
                try
                {

                    db.Licenca.Add(licenca);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static void DodajBanku(Banka banka)
        {
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {
                try
                {
                    db.Banka.Add(banka);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
            }
        }
        public static bool BrisiBanku(int id)
        {
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
                try
                {
                    Banka banka = (from a in db.Banka
                                   where a.BankaID == id
                                   select a).Single();
                    if (banka != null)
                    {

                        var a = banka.Licenca.StatusLicence;
                        if (a == 0)
                        {
                            banka.Licenca.StatusLicence = 1;
                        }
                        if (a == 1)
                        {
                            banka.Licenca.StatusLicence = 0;
                        }
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }


        }
        public static int VratiStatusLicence(int id)
        {
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
                try
                {
                    Banka banka = (from a in db.Banka
                                   where a.BankaID == id
                                   select a).Single();
                    if (banka != null)
                    {
                        if (banka.Licenca.StatusLicence == 0)
                        {
                            return 0;
                        }
                        if (banka.Licenca.StatusLicence == 1)
                        {
                            return 1;
                        }
                    }
                    return 1;

                }
                catch (Exception)
                {
                    return 0;
                }

        }
        public static Banka PrikaziBankupoId(int id)
        {
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {

                Banka banka = (from a in db.Banka
                               where a.BankaID == id
                               select a).Single();


                return banka;

            }
        }
        public static KombinovaniViewModel PrikaziZahtevPoSifri(int sifra)
        {
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {
                KombinovaniViewModel zahteviposifri = (from z in db.Zahtevi where z.ZahtevID == sifra select new KombinovaniViewModel{Email=z.Korisnici.Email,OpisZahteva=z.OpisZahteva,KorisnikID=z.KorisnikID,Naziv=z.Banka.Naziv,NazivKorisnika=z.Korisnici.NazivKorisnika,ZahtevID=z.ZahtevID,BankaID=z.BankaID }).Single();
            return zahteviposifri;
            }

        }
        public static void IzmeniBanku(Banka banka)
        {
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {
                Banka izmena = (from a in db.Banka
                                where a.BankaID == banka.BankaID
                                select a).Single();
                izmena.Naziv = banka.Naziv;
                izmena.Sediste = banka.Sediste;
                izmena.GodinaOsnivanja = banka.GodinaOsnivanja;
                izmena.StatusID = banka.StatusID;
                izmena.VrstaID = banka.VrstaID;
                izmena.Vlasnistvo = banka.Vlasnistvo;
                izmena.Godisnjiprofit = banka.Godisnjiprofit;
                izmena.UkupnaAktivaiUkupniDug = banka.UkupnaAktivaiUkupniDug;
                try
                {
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }
        public static void DodajVlasnistvo(VlasnickaStrukturaBanke vlasnistvobanke)
        {
            {
                using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
                    try
                    {
                        db.VlasnickaStrukturaBanke.Add(vlasnistvobanke);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {

                    }
            }
        }
        
        public static void IzmeniVlasnistvo1(VlasnickaStrukturaBanke novovlasnistvo)
        {

            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {
                VlasnickaStrukturaBanke izmenav = (from v in db.VlasnickaStrukturaBanke
                                                   where v.BankaID == novovlasnistvo.BankaID
                                                   select v).Single();

                izmenav.VlasniciBanke = novovlasnistvo.VlasniciBanke;
                izmenav.Procenat = novovlasnistvo.Procenat;

                try
                {

                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }
        public static VlasnickaStrukturaBanakaViewModel VlasnistvoBanke(int id)
        {
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {

                VlasnickaStrukturaBanakaViewModel vlasnistvo = (from v in db.VlasnickaStrukturaBanke
                                                                where v.BankaID == id
                                                                select new VlasnickaStrukturaBanakaViewModel { BankaID = v.BankaID, NazivBanaka = v.Banka.Naziv, NazivVlasnika = "", Procenat = v.Procenat, Sediste = v.Banka.Sediste, VlasniciBanke = v.VlasniciBanke }).Single();
                if (vlasnistvo.VlasniciBanke != null)
                {
                    vlasnistvo.NazivVlasnika = (from n in db.Banka where vlasnistvo.VlasniciBanke == n.BankaID select n.Naziv).FirstOrDefault();
                }

                return vlasnistvo;

            }
        }
        #endregion
        #region Liste
        public static List<Banka> ListaBanaka()
        {
            InformacioniSistemBanakaEntities banakaEntities = new InformacioniSistemBanakaEntities();

            List<Banka> banke = (from b in banakaEntities.Banka
                                 select b).ToList();
            return banke;

        }
        public static List<Banka> ListaAktivnihBanaka()
        {
            InformacioniSistemBanakaEntities banakaEntities = new InformacioniSistemBanakaEntities();

            List<Banka> banke = (from b in banakaEntities.Banka where b.Licenca.StatusLicence==1
                                 select b).ToList();
            return banke;

        }
        public static List<Banka> ListaNEAktivnihBanaka()
        {
            InformacioniSistemBanakaEntities banakaEntities = new InformacioniSistemBanakaEntities();

            List<Banka> banke = (from b in banakaEntities.Banka
                                 where b.Licenca.StatusLicence == 0
                                 select b).ToList();
            return banke;

        }

        public static List<KombinovaniViewModel> ListaZahteva()
        {
            InformacioniSistemBanakaEntities banakaEntities = new InformacioniSistemBanakaEntities();

            List<KombinovaniViewModel> zahtevi = (from b in banakaEntities.Zahtevi

                                                  select new KombinovaniViewModel { ZahtevID = b.ZahtevID, BankaID = b.BankaID, KorisnikID = b.KorisnikID, OpisZahteva = b.OpisZahteva, Naziv = b.Banka.Naziv, NazivKorisnika = b.Korisnici.NazivKorisnika, Email = (from k in banakaEntities.Korisnici where b.KorisnikID == k.KorisnikId select k.Email).FirstOrDefault() }).ToList();
           
            return zahtevi;
        }
        public static List<VlasnickaStrukturaBanke> ListaVlasnika()
        {
            InformacioniSistemBanakaEntities banakaEntities = new InformacioniSistemBanakaEntities();

            List<VlasnickaStrukturaBanke> vlasnistvobanaka = (from b in banakaEntities.VlasnickaStrukturaBanke select b).ToList();
            return vlasnistvobanaka;
        }
        public static List<VlasnickaStrukturaBanakaViewModel> ListaVlasnika1()
        {
            InformacioniSistemBanakaEntities banakaEntities = new InformacioniSistemBanakaEntities();
            List<VlasnickaStrukturaBanakaViewModel> vlasnickaStrukturaBanakaViewModels = (from b in banakaEntities.VlasnickaStrukturaBanke select new VlasnickaStrukturaBanakaViewModel { BankaID = b.BankaID, VlasniciBanke = b.VlasniciBanke, Procenat = b.Procenat, NazivBanaka = b.Banka.Naziv, NazivVlasnika = "" ,Sediste=b.Banka.Sediste}).ToList();

            foreach (var i in vlasnickaStrukturaBanakaViewModels)
            {
                if (i.VlasniciBanke != null)
                {
                    i.NazivVlasnika = (from k in banakaEntities.Banka where  i.VlasniciBanke==k.BankaID select k.Naziv).FirstOrDefault();
                    
                }
                
                

            }

            return vlasnickaStrukturaBanakaViewModels;
        }
        #endregion

        #region Select-Liste

        public static int Sifra { get; set; }

        public static SelectList PopuniStatusBanke()
        {
            InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();
            IEnumerable<SelectListItem> statusibanke = (from s in db.StatusBanke select s).AsEnumerable().Select(s => new SelectListItem() { Text = s.Status, Value = s.StatusID.ToString() });
            return new SelectList(statusibanke, "Value", "Text", Sifra);
            //dbEntitet.Dispose();
        }
        public static SelectList PopuniVrsteBanaka()
        {
            InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();
            IEnumerable<SelectListItem> vrstebanaka = (from s in db.VrstaBanke select s).AsEnumerable().Select(s => new SelectListItem() { Text = s.Tip, Value = s.VrstaID.ToString() });
            return new SelectList(vrstebanaka, "Value", "Text", Sifra);
            //dbEntitet.Dispose();
        }
        public static SelectList PopuniBankeID()
        {
            InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();
            IEnumerable<SelectListItem> banke = (from b in db.Banka select b).AsEnumerable().Select(b => new SelectListItem() { Text = b.Naziv, Value = b.BankaID.ToString() });
            return new SelectList(banke, "Value", "Text", Sifra);
        }
        public static SelectList PopuniStatusLicence()
        {
            InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();
            IEnumerable<SelectListItem> licence = (from l in db.Licenca select l).AsEnumerable().Select(l => new SelectListItem() { Text = l.StatusLicence.ToString(), Value = l.LicencaID.ToString() });
            return new SelectList(licence, "Value", "Text", Sifra);
        }
        public static SelectList PopuniBankeID1()
        {
            InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();
            IEnumerable<SelectListItem> banke = (from b in db.Banka where b.BankaID != (from a in db.VlasnickaStrukturaBanke select a.BankaID).FirstOrDefault() select b).AsEnumerable().Select(b => new SelectListItem() { Text = b.Naziv, Value = b.BankaID.ToString() });
            return new SelectList(banke, "Value", "Text", Sifra);
        }

        public static SelectList PopuniBankeIDVlasnickaStruktura()
        {
            InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();
            IEnumerable<SelectListItem> banke = (from b in db.VlasnickaStrukturaBanke select b).AsEnumerable().Select(b => new SelectListItem() { Text = b.Banka.Naziv, Value = b.BankaID.ToString() });
            return new SelectList(banke, "Value", "Text", Sifra);
        }
    }
}
#endregion
