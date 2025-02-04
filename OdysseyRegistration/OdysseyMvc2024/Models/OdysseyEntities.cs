﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.OdysseyEntities
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using Microsoft.EntityFrameworkCore;

namespace OdysseyMvc2024.Models
{
    public class OdysseyEntities : DbContext, IOdysseyEntities
    {
        public OdysseyEntities(DbContextOptions<OdysseyEntities> options)
            //: base("name=OdysseyEntities")
            : base(options)
        {
        }

        //public DbSet<CoachesTrainingDivision> CoachesTrainingDivisions { get; set; }
        //public DbSet<CoachesTrainingRegion> CoachesTrainingRegions { get; set; }
        //public DbSet<CoachesTrainingRegistration> CoachesTrainingRegistrations { get; set; }
        //public DbSet<CoachesTrainingRole> CoachesTrainingRoles { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<ContactUsRecipient> ContactUsRecipients { get; set; }
        public DbSet<ContactUsSenderRole> ContactUsSenderRoles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<TournamentRegistration> TournamentRegistrations { get; set; }
        //public DbSet<Volunteer> Volunteers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Config>().ToTable("Config").HasKey(c => c.Name);
            modelBuilder.Entity<ContactUsRecipient>().ToTable("ContactUsRecipient");
            modelBuilder.Entity<ContactUsSenderRole>().ToTable("ContactUsSenderRole");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Judge>().ToTable("Judge");
            modelBuilder.Entity<Problem>().ToTable("Problem");
            modelBuilder.Entity<School>().ToTable("School");
            modelBuilder.Entity<TournamentRegistration>().ToTable("TournamentRegistration");

            // Other configurations...
        }
    }
}
