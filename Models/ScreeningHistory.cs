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
    
    public partial class ScreeningHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScreeningHistory()
        {
            this.ScreeningImage = new HashSet<ScreeningImage>();
        }
    
        public int sh_id { get; set; }
        public System.DateTime sh_datetime { get; set; }
        public string sh_additional { get; set; }
        public int MedicalProfessional_user_id { get; set; }
        public int Patient_user_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreeningImage> ScreeningImage { get; set; }
        public virtual MedicalProfessional MedicalProfessional { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Report Report { get; set; }
    }
}
