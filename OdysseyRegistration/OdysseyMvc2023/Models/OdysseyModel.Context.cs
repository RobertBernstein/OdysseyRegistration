﻿// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Models.OdysseyEntities
// Assembly: OdysseyMvc4, Version=1.0.5460.36587, Culture=neutral, PublicKeyToken=null
// MVID: 7B658547-521F-44CB-80FA-52857CB94B72
// Assembly location: C:\Users\rob\OneDrive\Odyssey\OdysseyProd\registration\bin\OdysseyMvc4.dll

using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace OdysseyMvc2023.Models
{
    public class OdysseyEntities : DbContext
    {
        public OdysseyEntities()
          : base("name=OdysseyEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) => throw new UnintentionalCodeFirstException();

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
    }
}
