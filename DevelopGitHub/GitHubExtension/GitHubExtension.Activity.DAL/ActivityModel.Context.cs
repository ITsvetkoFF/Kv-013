﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GitHubExtension.Activity.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ActivityContext : DbContext
    {
        public ActivityContext()
            : base("name=ActivityContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActivityEvent> Activities { get; set; }
        public virtual DbSet<ActivityType> ActivitiesTypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
