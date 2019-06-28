using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IBS2.Models
{
    public class AdminViewModel
    {
        public int BankaID { get; set; }
        [Required(ErrorMessage = "Unesi naziv")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Unesi sediste banke")]
        public string Sediste { get; set; }

        [Required(ErrorMessage = "Unesi godinu osnivanja")]
        [Range(1, 1000000000, ErrorMessage = "Koja godina pocinje sa nulom zivota ti")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Godina osnivanja mora biti broj")]
        public int GodinaOsnivanja { get; set; }
        public int StatusID { get; set; }
        public int VrstaID { get; set; }
        public int LicencaID { get; set; }
        [Required(ErrorMessage = "Cekiraj vlasnistvo banke")]
        public string Vlasnistvo { get; set; }

        [RegularExpression(@"^\d+\.\d{0,9}$", ErrorMessage = "Morate uneti pravilno ispisani pozitivni decimalni broj")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Godisnji profit ne moze biti negativan")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> Godisnjiprofit { get; set; }

        [RegularExpression(@"^\d+\.\d{0,9}$", ErrorMessage = "Morate uneti pravilno ispisani pozitivni decimalni broj")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Godisnji profit ne moze biti negativan")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> UkupnaAktivaiUkupniDug { get; set; }

        public virtual Licenca Licenca { get; set; }
        public virtual StatusBanke StatusBanke { get; set; }
        public virtual VrstaBanke VrstaBanke { get; set; }
        public virtual VlasnickaStrukturaBanke VlasnickaStrukturaBanke { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zahtevi> Zahtevi { get; set; }
        [NotMapped]
        public List<StatusBanke> statusbanke1 { get; set; }
        [NotMapped]
        public List<VrstaBanke> vrstabanke1 { get; set; }
        [NotMapped]
        public List<Banka> banke { get; set; }
        public int? VlasniciBanke { get; set; }
        
        public float? Procenat { get; set; }
        public virtual Banka Banka { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VlasnickaStrukturaBanke> VlasnickaStrukturaBanke1 { get; set; }
        public virtual VlasnickaStrukturaBanke VlasnickaStrukturaBanke2 { get; set; }
    }
}