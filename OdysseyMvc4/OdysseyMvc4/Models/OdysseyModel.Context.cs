﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OdysseyMvc4.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OdysseyEntities : DbContext
    {
        public OdysseyEntities()
            : base("name=OdysseyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CoachesTrainingDivision> CoachesTrainingDivisions { get; set; }
        public DbSet<CoachesTrainingRegion> CoachesTrainingRegions { get; set; }
        public DbSet<CoachesTrainingRegistration> CoachesTrainingRegistrations { get; set; }
        public DbSet<CoachesTrainingRole> CoachesTrainingRoles { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<ContactUsRecipient> ContactUsRecipients { get; set; }
        public DbSet<ContactUsSenderRole> ContactUsSenderRoles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<TournamentRegistration> TournamentRegistrations { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
    }
}
