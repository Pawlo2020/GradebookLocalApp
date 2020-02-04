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
    
    public partial class prowadzacy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public prowadzacy()
        {
            this.projekty = new HashSet<projekty>();
            this.oceny_etapow = new HashSet<oceny_etapow>();
            this.oceny_projektow = new HashSet<oceny_projektow>();
            this.przedmioty = new HashSet<przedmioty>();
            this.zajecia = new HashSet<zajecia>();
        }
    
        public int id_prow { get; set; }
        public Nullable<int> id_typ_prowadzacy { get; set; }
        public string nr_pok { get; set; }
        public string haslo { get; set; }
        public string imie_prowadzacy { get; set; }
        public string drugie_imie_prowadzacy { get; set; }
        public string nazwisko_prowadzacy { get; set; }
        public System.DateTime data_urodzenia_prowadzacy { get; set; }
        public string miejsce_urodzenia_prowadzacy { get; set; }
        public string pesel_prowadzacy { get; set; }
        public string email_prowadzacy { get; set; }
        public string telefon_prowadzacy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<projekty> projekty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<oceny_etapow> oceny_etapow { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<oceny_projektow> oceny_projektow { get; set; }
        public virtual typy_prowadzacych typy_prowadzacych { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<przedmioty> przedmioty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zajecia> zajecia { get; set; }
    }
}