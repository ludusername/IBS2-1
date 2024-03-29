//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IBS2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public partial class Zahtevi
    {
        public int ZahtevID { get; set; }
        public int KorisnikID { get; set; }
        public int BankaID { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Morate uneti zahtev")]
        
        public string OpisZahteva { get; set; }
        [NotMapped]
        public List<Banka> nazivibanaka { get; set; }
        public virtual Banka Banka { get; set; }
        public virtual Korisnici Korisnici { get; set; }

        [NotMapped]
        public List<Banka> banke { get; set; }
        [NotMapped]
        public List<Zahtevi> zahtevikorisnika { get; set; }
            [NotMapped]
            public List<Zahtevi> zahtevi { get; set; }
    }
}
