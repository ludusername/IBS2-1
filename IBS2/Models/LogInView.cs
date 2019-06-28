using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IBS2.Models
{
    public class LogInView
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unesi naziv korisnika")]
        [Display(Name = "Korisničko ime")]
        public string NazivKorisnika { get; set; }
        [Required(ErrorMessage = "Unesi lozinku")]
        public string Lozinka { get; set; }

        public string LoginError { get; set; }//ovaj properti nam sluzi za ispisivanje greske u logovanju
    }
}