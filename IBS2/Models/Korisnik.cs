using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBS2.Models
{
    public class Korisnik
    {
        #region Select-Liste
        public static int Sifra { get; set; }
        
        public static SelectList PopuniSifreKorisnika()
        {
            InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();
            IEnumerable<SelectListItem> sifrekorisnika = (from s in db.Korisnici select s).AsEnumerable().Select(s => new SelectListItem() { Text = s.NazivKorisnika, Value = s.KorisnikId.ToString() });
            return new SelectList(sifrekorisnika, "Value", "Text", Sifra);
            //dbEntitet.Dispose();
        }
        public static SelectList PopuniBankeID()
        {
            InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();
            IEnumerable<SelectListItem> banke = (from b in db.Banka select b).AsEnumerable().Select(b => new SelectListItem() { Text = b.Naziv, Value = b.BankaID.ToString() });
            return new SelectList(banke, "Value", "Text", Sifra);
        }
        #endregion
#region Korisničke-metode
        public static void UbaciZahtev(Zahtevi zahtev)
        {
            using (InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities())
            {
                try
                {
                    db.Zahtevi.Add(zahtev);
                    db.SaveChanges();
                }
                catch
                {

                }
            }
        }
        #endregion
#region Korisničke-Liste
        public static List<Zahtevi> PrikaziZahtevePoKorisniku(int sifra)
        {
            InformacioniSistemBanakaEntities db = new InformacioniSistemBanakaEntities();

            List<Zahtevi> zahtevi = (from z in db.Zahtevi where z.KorisnikID == sifra select z).ToList();
            return zahtevi;
        }
   
        public static List<Banka> ListaBanaka()
        {
            InformacioniSistemBanakaEntities banakaEntities = new InformacioniSistemBanakaEntities();

            List<Banka> banke = (from b in banakaEntities.Banka
                                 select b).ToList();
            return banke;

        }
        public static List<Zahtevi> ListaZahteva()
        {
            InformacioniSistemBanakaEntities banakaEntities = new InformacioniSistemBanakaEntities();
            List<Zahtevi> zahtevi = (from z in banakaEntities.Zahtevi select z).ToList();
            return zahtevi;
        }
    }
}
#endregion