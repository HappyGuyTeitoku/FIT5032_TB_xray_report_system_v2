﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TB_xray_systemsContainer : DbContext
    {
        public TB_xray_systemsContainer()
            : base("name=TB_xray_systemsContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> UserSet { get; set; }
        public virtual DbSet<DeletionRequest> DeletionRequestSet { get; set; }
        public virtual DbSet<Report> ReportSet { get; set; }
        public virtual DbSet<ScreeningHistory> ScreeningHistorySet { get; set; }
        public virtual DbSet<ScreeningImage> ScreeningImageSet { get; set; }
        
        // Manual
        public virtual DbSet<Administrator> UserSet_Administrator { get; set; }
        public virtual DbSet<MedicalProfessional> UserSet_MedicalProfessional { get; set; }
        public virtual DbSet<Patient> UserSet_Patient { get; set; }

    }
}
