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
    
    public partial class ScreeningImage
    {
        public int si_id { get; set; }
        public string si_file { get; set; }
        public string si_additional { get; set; }
        public int ScreeningHistory_sh_id { get; set; }
    
        public virtual ScreeningHistory ScreeningHistory { get; set; }
    }
}
