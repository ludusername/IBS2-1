using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBS2.Models
{
    public class KombinovaniViewModel
    {
        public string Naziv { get; set; }
        public int ZahtevID { get; set; }
        public int KorisnikID { get; set; }
        public int BankaID { get; set; }
        public string OpisZahteva { get; set; }
        public string NazivKorisnika { get; set; }
        public string Email { get; set; }
    }
}