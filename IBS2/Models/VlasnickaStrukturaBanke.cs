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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class VlasnickaStrukturaBanke
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VlasnickaStrukturaBanke()
        {
            this.VlasnickaStrukturaBanke1 = new HashSet<VlasnickaStrukturaBanke>();
        }
    
        public Nullable<int> VlasniciBanke { get; set; }
        public int BankaID { get; set; }
        public Nullable<float> Procenat { get; set; }
        [NotMapped]
        public List<Banka> nazivibanaka { get; set; }
        public virtual Banka Banka { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VlasnickaStrukturaBanke> VlasnickaStrukturaBanke1 { get; set; }
        public virtual VlasnickaStrukturaBanke VlasnickaStrukturaBanke2 { get; set; }
    }
}
