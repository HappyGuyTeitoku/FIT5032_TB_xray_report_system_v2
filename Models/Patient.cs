//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FIT5032_TB_xray_report_system_v2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient : User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            this.DeletionRequest = new HashSet<DeletionRequest>();
            this.ScreeningHistory = new HashSet<ScreeningHistory>();
        }
    
        public int patient_id { get; set; }
        public string patient_address { get; set; }
        public string patient_phone { get; set; }
        public string patient_medical_history { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeletionRequest> DeletionRequest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreeningHistory> ScreeningHistory { get; set; }
    }
}
