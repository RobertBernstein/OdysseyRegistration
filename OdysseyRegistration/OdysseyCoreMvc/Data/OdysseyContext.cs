﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OdysseyCoreMvc.Models;

namespace OdysseyCoreMvc.Data
{
    public partial class OdysseyContext : DbContext
    {
        public OdysseyContext()
        {
        }

        public OdysseyContext(DbContextOptions<OdysseyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CoachesTrainingDivisions> CoachesTrainingDivisions { get; set; }
        public virtual DbSet<CoachesTrainingRegions> CoachesTrainingRegions { get; set; }
        public virtual DbSet<CoachesTrainingRegistrations> CoachesTrainingRegistrations { get; set; }
        public virtual DbSet<CoachesTrainingRoles> CoachesTrainingRoles { get; set; }
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<ContactUsRecipients> ContactUsRecipients { get; set; }
        public virtual DbSet<ContactUsSenderRoles> ContactUsSenderRoles { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Judges> Judges { get; set; }
        public virtual DbSet<Problem> Problem { get; set; }
        public virtual DbSet<Schools> Schools { get; set; }
        public virtual DbSet<TournamentRegistration> TournamentRegistration { get; set; }
        public virtual DbSet<Volunteers> Volunteers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoachesTrainingDivisions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CoachesTrainingRegions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<CoachesTrainingRegistrations>(entity =>
            {
                entity.HasKey(e => e.RegistrationId)
                    .HasName("PK_coaches_training");

                entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");

                entity.Property(e => e.Division).HasMaxLength(100);

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.RegionNumber).HasMaxLength(20);

                entity.Property(e => e.Role).HasMaxLength(100);

                entity.Property(e => e.SchoolName).HasMaxLength(100);

                entity.Property(e => e.SelectedProblem).HasMaxLength(100);

                entity.Property(e => e.TimeRegistered).HasColumnType("datetime");

                entity.Property(e => e.YearsInvolved).HasMaxLength(10);
            });

            modelBuilder.Entity<CoachesTrainingRoles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK_config");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Value).HasMaxLength(800);
            });

            modelBuilder.Entity<ContactUsRecipients>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("contact_name");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email_address");
            });

            modelBuilder.Entity<ContactUsSenderRoles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EventCoordinatorEmail).HasMaxLength(100);

                entity.Property(e => e.EventCoordinatorName).HasMaxLength(100);

                entity.Property(e => e.EventCoordinatorPhone).HasMaxLength(100);

                entity.Property(e => e.EventCost).HasMaxLength(20);

                entity.Property(e => e.EventMakeChecksOutTo).HasMaxLength(150);

                entity.Property(e => e.EventName).HasMaxLength(200);

                entity.Property(e => e.EventPayeeAddress1).HasMaxLength(100);

                entity.Property(e => e.EventPayeeAddress2).HasMaxLength(100);

                entity.Property(e => e.EventPayeeCity).HasMaxLength(50);

                entity.Property(e => e.EventPayeeEmail1).HasMaxLength(100);

                entity.Property(e => e.EventPayeeName).HasMaxLength(100);

                entity.Property(e => e.EventPayeePhone1).HasMaxLength(20);

                entity.Property(e => e.EventPayeeState).HasMaxLength(30);

                entity.Property(e => e.EventPayeeZipCode).HasMaxLength(15);

                entity.Property(e => e.InformationUrl)
                    .HasMaxLength(100)
                    .HasColumnName("InformationURL");

                entity.Property(e => e.LateEventCost).HasMaxLength(20);

                entity.Property(e => e.LateEventCostStartDate).HasColumnType("date");

                entity.Property(e => e.Location).HasMaxLength(100);

                entity.Property(e => e.LocationAddress).HasMaxLength(100);

                entity.Property(e => e.LocationCity).HasMaxLength(30);

                entity.Property(e => e.LocationMapUrl)
                    .HasMaxLength(100)
                    .HasColumnName("LocationMapURL");

                entity.Property(e => e.LocationPhone).HasMaxLength(50);

                entity.Property(e => e.LocationState).HasMaxLength(5);

                entity.Property(e => e.LocationUrl)
                    .HasMaxLength(200)
                    .HasColumnName("LocationURL");

                entity.Property(e => e.LocationUrlcolor)
                    .HasMaxLength(100)
                    .HasColumnName("LocationURLColor");

                entity.Property(e => e.PaymentDueDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Time).HasMaxLength(100);
            });

            modelBuilder.Entity<Judges>(entity =>
            {
                entity.HasKey(e => e.JudgeId)
                    .HasName("PK_judges");

                entity.Property(e => e.JudgeId).HasColumnName("JudgeID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.AddressLine2).HasMaxLength(255);

                entity.Property(e => e.AttendedJt).HasColumnName("AttendedJT?");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Coi)
                    .HasMaxLength(50)
                    .HasColumnName("COI");

                entity.Property(e => e.DaytimePhone).HasMaxLength(30);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.EveningPhone).HasMaxLength(30);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.HasChildrenCompeting).HasMaxLength(3);

                entity.Property(e => e.InformationMailed).HasColumnName("InformationMailed?");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MobilePhone).HasMaxLength(30);

                entity.Property(e => e.PreviousPositions).HasMaxLength(100);

                entity.Property(e => e.ProblemAssigned).HasMaxLength(50);

                entity.Property(e => e.ProblemChoice1).HasMaxLength(60);

                entity.Property(e => e.ProblemChoice2).HasMaxLength(60);

                entity.Property(e => e.ProblemChoice3).HasMaxLength(60);

                entity.Property(e => e.ProblemCoi1)
                    .HasMaxLength(60)
                    .HasColumnName("ProblemCOI1");

                entity.Property(e => e.ProblemCoi2)
                    .HasMaxLength(60)
                    .HasColumnName("ProblemCOI2");

                entity.Property(e => e.ProblemCoi3)
                    .HasMaxLength(60)
                    .HasColumnName("ProblemCOI3");

                entity.Property(e => e.ProblemId)
                    .HasMaxLength(50)
                    .HasColumnName("ProblemID");

                entity.Property(e => e.State).HasMaxLength(20);

                entity.Property(e => e.TeamId)
                    .HasMaxLength(50)
                    .HasColumnName("TeamID");

                entity.Property(e => e.TimeAssignedToTeam).HasColumnType("datetime");

                entity.Property(e => e.TimeRegistered).HasColumnType("datetime");

                entity.Property(e => e.TimeRegistrationStarted).HasColumnType("datetime");

                entity.Property(e => e.TshirtSize).HasMaxLength(50);

                entity.Property(e => e.WantsCeucredit)
                    .HasMaxLength(3)
                    .HasColumnName("WantsCEUCredit");

                entity.Property(e => e.WillingToBeScorechecker).HasMaxLength(3);

                entity.Property(e => e.YearsOfLongTermJudgingExperience).HasMaxLength(50);

                entity.Property(e => e.YearsOfSpontaneousJudgingExperience).HasMaxLength(50);

                entity.Property(e => e.ZipCode).HasMaxLength(20);
            });

            modelBuilder.Entity<Problem>(entity =>
            {
                entity.Property(e => e.ProblemId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProblemID");

                entity.Property(e => e.CostLimit).HasMaxLength(50);

                entity.Property(e => e.Divisions).HasMaxLength(50);

                entity.Property(e => e.Pcaddress)
                    .HasMaxLength(255)
                    .HasColumnName("PCAddress");

                entity.Property(e => e.Pccity)
                    .HasMaxLength(50)
                    .HasColumnName("PCCity");

                entity.Property(e => e.Pcemail1)
                    .HasMaxLength(50)
                    .HasColumnName("PCEmail1");

                entity.Property(e => e.Pcemail2)
                    .HasMaxLength(50)
                    .HasColumnName("PCEmail2");

                entity.Property(e => e.PcfaxNumber)
                    .HasMaxLength(30)
                    .HasColumnName("PCFaxNumber");

                entity.Property(e => e.PcfirstName)
                    .HasMaxLength(50)
                    .HasColumnName("PCFirstName");

                entity.Property(e => e.PchomePhone)
                    .HasMaxLength(30)
                    .HasColumnName("PCHomePhone");

                entity.Property(e => e.PclastName)
                    .HasMaxLength(50)
                    .HasColumnName("PCLastName");

                entity.Property(e => e.PcmobilePhone)
                    .HasMaxLength(30)
                    .HasColumnName("PCMobilePhone");

                entity.Property(e => e.PcpostalCode)
                    .HasMaxLength(20)
                    .HasColumnName("PCPostalCode");

                entity.Property(e => e.PcstateOrProvince)
                    .HasMaxLength(20)
                    .HasColumnName("PCStateOrProvince");

                entity.Property(e => e.PcworkPhone)
                    .HasMaxLength(30)
                    .HasColumnName("PCWorkPhone");

                entity.Property(e => e.ProblemCaptainId)
                    .HasMaxLength(50)
                    .HasColumnName("ProblemCaptainID");

                entity.Property(e => e.ProblemCategory).HasMaxLength(30);

                entity.Property(e => e.ProblemName).HasMaxLength(50);
            });

            modelBuilder.Entity<Schools>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CoordAddress).HasMaxLength(255);

                entity.Property(e => e.CoordAltPhone).HasMaxLength(30);

                entity.Property(e => e.CoordCity).HasMaxLength(50);

                entity.Property(e => e.CoordEmailName).HasMaxLength(50);

                entity.Property(e => e.CoordFaxNumber).HasMaxLength(30);

                entity.Property(e => e.CoordFirstName).HasMaxLength(50);

                entity.Property(e => e.CoordLastName).HasMaxLength(50);

                entity.Property(e => e.CoordMobilePhone).HasMaxLength(30);

                entity.Property(e => e.CoordNew)
                    .HasMaxLength(50)
                    .HasColumnName("CoordNew?");

                entity.Property(e => e.CoordPhone).HasMaxLength(30);

                entity.Property(e => e.CoordPostalCode).HasMaxLength(20);

                entity.Property(e => e.CoordState).HasMaxLength(20);

                entity.Property(e => e.Membership1)
                    .HasMaxLength(50)
                    .HasColumnName("Membership#1");

                entity.Property(e => e.Membership1seen)
                    .HasMaxLength(50)
                    .HasColumnName("Membership#1seen");

                entity.Property(e => e.Membership2)
                    .HasMaxLength(50)
                    .HasColumnName("Membership#2");

                entity.Property(e => e.Membership2seen)
                    .HasMaxLength(50)
                    .HasColumnName("Membership#2seen");

                entity.Property(e => e.Membership3)
                    .HasMaxLength(50)
                    .HasColumnName("Membership#3");

                entity.Property(e => e.Membership3seen)
                    .HasMaxLength(50)
                    .HasColumnName("Membership#3seen");

                entity.Property(e => e.Membership4)
                    .HasMaxLength(50)
                    .HasColumnName("Membership#4");

                entity.Property(e => e.Membership4seen)
                    .HasMaxLength(50)
                    .HasColumnName("Membership#4seen");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.Share)
                    .HasMaxLength(50)
                    .HasColumnName("Share?");

                entity.Property(e => e.State).HasMaxLength(20);
            });

            modelBuilder.Entity<TournamentRegistration>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.AltCoachDaytimePhone).HasMaxLength(50);

                entity.Property(e => e.AltCoachEmailAddress).HasMaxLength(50);

                entity.Property(e => e.AltCoachEveningPhone).HasMaxLength(50);

                entity.Property(e => e.AltCoachFirstName).HasMaxLength(50);

                entity.Property(e => e.AltCoachLastName).HasMaxLength(50);

                entity.Property(e => e.AltCoachMobilePhone).HasMaxLength(30);

                entity.Property(e => e.CoachAddress).HasMaxLength(255);

                entity.Property(e => e.CoachCity).HasMaxLength(50);

                entity.Property(e => e.CoachDaytimePhone).HasMaxLength(30);

                entity.Property(e => e.CoachEmailAddress).HasMaxLength(50);

                entity.Property(e => e.CoachEveningPhone).HasMaxLength(30);

                entity.Property(e => e.CoachFirstName).HasMaxLength(50);

                entity.Property(e => e.CoachLastName).HasMaxLength(50);

                entity.Property(e => e.CoachMobilePhone).HasMaxLength(30);

                entity.Property(e => e.CoachState).HasMaxLength(20);

                entity.Property(e => e.CoachZipCode).HasMaxLength(20);

                entity.Property(e => e.Division).HasMaxLength(50);

                entity.Property(e => e.JudgeId).HasColumnName("JudgeID");

                entity.Property(e => e.MemberFirstName1).HasMaxLength(50);

                entity.Property(e => e.MemberFirstName2).HasMaxLength(50);

                entity.Property(e => e.MemberFirstName3).HasMaxLength(50);

                entity.Property(e => e.MemberFirstName4).HasMaxLength(50);

                entity.Property(e => e.MemberFirstName5).HasMaxLength(50);

                entity.Property(e => e.MemberFirstName6).HasMaxLength(50);

                entity.Property(e => e.MemberFirstName7).HasMaxLength(50);

                entity.Property(e => e.MemberGrade1).HasMaxLength(50);

                entity.Property(e => e.MemberGrade2).HasMaxLength(50);

                entity.Property(e => e.MemberGrade3).HasMaxLength(50);

                entity.Property(e => e.MemberGrade4).HasMaxLength(50);

                entity.Property(e => e.MemberGrade5).HasMaxLength(50);

                entity.Property(e => e.MemberGrade6).HasMaxLength(50);

                entity.Property(e => e.MemberGrade7).HasMaxLength(50);

                entity.Property(e => e.MemberLastName1).HasMaxLength(50);

                entity.Property(e => e.MemberLastName2).HasMaxLength(50);

                entity.Property(e => e.MemberLastName3).HasMaxLength(50);

                entity.Property(e => e.MemberLastName4).HasMaxLength(50);

                entity.Property(e => e.MemberLastName5).HasMaxLength(50);

                entity.Property(e => e.MemberLastName6).HasMaxLength(50);

                entity.Property(e => e.MemberLastName7).HasMaxLength(50);

                entity.Property(e => e.MembershipName).HasMaxLength(50);

                entity.Property(e => e.MembershipNumber).HasMaxLength(50);

                entity.Property(e => e.ProblemId).HasColumnName("ProblemID");

                entity.Property(e => e.SchoolId).HasColumnName("SchoolID");

                entity.Property(e => e.TeamRegistrationFee).HasMaxLength(20);

                entity.Property(e => e.TimeRegistered).HasColumnType("datetime");

                entity.Property(e => e.TimeRegistrationStarted).HasColumnType("datetime");

                entity.Property(e => e.VolunteerId).HasColumnName("VolunteerID");
            });

            modelBuilder.Entity<Volunteers>(entity =>
            {
                entity.HasKey(e => e.VolunteerId);

                entity.Property(e => e.VolunteerId).HasColumnName("VolunteerID");

                entity.Property(e => e.DaytimePhone).HasMaxLength(30);

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.EveningPhone).HasMaxLength(30);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MobilePhone).HasMaxLength(30);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.TimeAssignedToTeam).HasColumnType("datetime");

                entity.Property(e => e.TimeRegistered).HasColumnType("datetime");

                entity.Property(e => e.TimeRegistrationStarted).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}