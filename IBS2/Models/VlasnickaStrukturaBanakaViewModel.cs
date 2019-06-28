using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IBS2.Models
{
    public class VlasnickaStrukturaBanakaViewModel
    {
        public Nullable<int> VlasniciBanke { get; set; }
        public int BankaID { get; set; }
        [Required(ErrorMessage = "Unesite procenat")]
        [Range(float.Epsilon,100,ErrorMessage ="Unesite validan procenat")]
        public Nullable<float> Procenat { get; set; }
       
        public string NazivVlasnika { get; set; }

        public string NazivBanaka { get; set; }
        
        public string Sediste { get; set; }
        [NotMapped]
        public List<Banka> nazivibanaka { get; set; }
    }
}
