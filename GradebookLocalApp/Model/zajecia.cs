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
    
    public partial class zajecia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public zajecia()
        {
            this.projekty = new HashSet<projekty>();
            this.studenci = new HashSet<studenci>();
            this.grupy_dziekanskie = new HashSet<grupy_dziekanskie>();
        }
    
        public int id_zajec { get; set; }
        public Nullable<int> id_przed { get; set; }
        public Nullable<int> id_prow { get; set; }
        public Nullable<int> id_typ { get; set; }
        public Nullable<System.DateTime> data_zaj { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<projekty> projekty { get; set; }
        public virtual prowadzacy prowadzacy { get; set; }
        public virtual przedmioty przedmioty { get; set; }
        public virtual typy_zajec typy_zajec { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<studenci> studenci { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<grupy_dziekanskie> grupy_dziekanskie { get; set; }
    }
}