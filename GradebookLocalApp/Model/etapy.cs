//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GradebookLocalApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class etapy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public etapy()
        {
            this.oceny_etapow = new HashSet<oceny_etapow>();
        }
    
        public int id_etap { get; set; }
        public Nullable<int> id_proj { get; set; }
        public string nazwa_etapu { get; set; }
        public Nullable<System.DateTime> termin_etapu { get; set; }
    
        public virtual projekty projekty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<oceny_etapow> oceny_etapow { get; set; }
    }
}