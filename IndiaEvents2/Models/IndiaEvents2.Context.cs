﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IndiaEvents2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IndiaEvents2Entities : DbContext
    {
        public IndiaEvents2Entities()
            : base("name=IndiaEvents2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<LookupType> LookupTypes { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<loginHistory> loginHistories { get; set; }
    }
}
