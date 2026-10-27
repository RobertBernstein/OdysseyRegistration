USE [DB_12824_registration]
GO
/****** Object:  StoredProcedure [dbo].[ListAllTeamsRegisteredForTournament]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[ListAllTeamsRegisteredForTournament]
GO
/****** Object:  StoredProcedure [dbo].[GenerateImportFileForScoringProgram]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[GenerateImportFileForScoringProgram]
GO
/****** Object:  StoredProcedure [dbo].[FindNullVolunteerRegistrations]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[FindNullVolunteerRegistrations]
GO
/****** Object:  StoredProcedure [dbo].[FindNullTournamentRegistrations]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[FindNullTournamentRegistrations]
GO
/****** Object:  StoredProcedure [dbo].[FindNullJudgeRegistrations]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[FindNullJudgeRegistrations]
GO
/****** Object:  StoredProcedure [dbo].[FindAllJudgesAssignedToMoreThanOneTeam]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[FindAllJudgesAssignedToMoreThanOneTeam]
GO
/****** Object:  StoredProcedure [dbo].[DropAndRecreateTournamentRegistrationTable]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[DropAndRecreateTournamentRegistrationTable]
GO
/****** Object:  StoredProcedure [dbo].[DeleteNullVolunteerRegistrations]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[DeleteNullVolunteerRegistrations]
GO
/****** Object:  StoredProcedure [dbo].[DeleteNullTournamentRegistrations]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[DeleteNullTournamentRegistrations]
GO
/****** Object:  StoredProcedure [dbo].[DeleteNullJudgeRegistrations]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[DeleteNullJudgeRegistrations]
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllTournamentRegistrationRecordsAndResetIdToZero]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[DeleteAllTournamentRegistrationRecordsAndResetIdToZero]
GO
/****** Object:  Table [dbo].[Volunteers]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Volunteers]') AND type in (N'U'))
DROP TABLE [dbo].[Volunteers]
GO
/****** Object:  Table [dbo].[TournamentRegistration]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TournamentRegistration]') AND type in (N'U'))
DROP TABLE [dbo].[TournamentRegistration]
GO
/****** Object:  Table [dbo].[Schools]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Schools]') AND type in (N'U'))
DROP TABLE [dbo].[Schools]
GO
/****** Object:  Table [dbo].[Problem]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Problem]') AND type in (N'U'))
DROP TABLE [dbo].[Problem]
GO
/****** Object:  Table [dbo].[Judges]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Judges]') AND type in (N'U'))
DROP TABLE [dbo].[Judges]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND type in (N'U'))
DROP TABLE [dbo].[Events]
GO
/****** Object:  Table [dbo].[ContactUsSenderRoles]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactUsSenderRoles]') AND type in (N'U'))
DROP TABLE [dbo].[ContactUsSenderRoles]
GO
/****** Object:  Table [dbo].[ContactUsRecipients]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactUsRecipients]') AND type in (N'U'))
DROP TABLE [dbo].[ContactUsRecipients]
GO
/****** Object:  Table [dbo].[Config]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Config]') AND type in (N'U'))
DROP TABLE [dbo].[Config]
GO
/****** Object:  Table [dbo].[CoachesTrainingRoles]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CoachesTrainingRoles]') AND type in (N'U'))
DROP TABLE [dbo].[CoachesTrainingRoles]
GO
/****** Object:  Table [dbo].[CoachesTrainingRegistrations]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CoachesTrainingRegistrations]') AND type in (N'U'))
DROP TABLE [dbo].[CoachesTrainingRegistrations]
GO
/****** Object:  Table [dbo].[CoachesTrainingRegions]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CoachesTrainingRegions]') AND type in (N'U'))
DROP TABLE [dbo].[CoachesTrainingRegions]
GO
/****** Object:  Table [dbo].[CoachesTrainingDivisions]    Script Date: 12/2/2023 8:02:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CoachesTrainingDivisions]') AND type in (N'U'))
DROP TABLE [dbo].[CoachesTrainingDivisions]
GO
USE [master]
GO
/****** Object:  Database [DB_12824_registration]    Script Date: 12/2/2023 8:02:31 PM ******/
DROP DATABASE [DB_12824_registration]
GO
/****** Object:  Database [DB_12824_registration]    Script Date: 12/2/2023 8:02:31 PM ******/
CREATE DATABASE [DB_12824_registration] ON  PRIMARY 
( NAME = N'DB_12824_registration_data', FILENAME = N'/var/opt/mssql/DB_12824_registration_data.mdf' , SIZE = 3520KB , MAXSIZE = 25600KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DB_12824_registration_log', FILENAME = N'/var/opt/mssql/DB_12824_registration_log.ldf' , SIZE = 1024KB , MAXSIZE = 1024000KB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DB_12824_registration] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_12824_registration].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_12824_registration] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_12824_registration] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_12824_registration] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_12824_registration] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_12824_registration] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_12824_registration] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_12824_registration] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_12824_registration] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_12824_registration] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_12824_registration] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_12824_registration] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_12824_registration] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_12824_registration] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_12824_registration] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_12824_registration] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_12824_registration] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_12824_registration] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_12824_registration] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_12824_registration] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_12824_registration] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_12824_registration] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_12824_registration] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_12824_registration] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_12824_registration] SET  MULTI_USER 
GO
ALTER DATABASE [DB_12824_registration] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_12824_registration] SET DB_CHAINING OFF 
GO
USE [DB_12824_registration]
GO
/****** Object:  Table [dbo].[CoachesTrainingDivisions]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachesTrainingDivisions](
	[ID] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_coaches_training_divisions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoachesTrainingRegions]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachesTrainingRegions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_coaches_training_regions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoachesTrainingRegistrations]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachesTrainingRegistrations](
	[RegistrationID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](100) NULL,
	[SchoolName] [nvarchar](100) NULL,
	[Role] [nvarchar](100) NULL,
	[Division] [nvarchar](100) NULL,
	[SelectedProblem] [nvarchar](100) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[YearsInvolved] [nvarchar](10) NULL,
	[RegionNumber] [nvarchar](20) NULL,
	[TimeRegistered] [datetime] NULL,
	[UserAgent] [nvarchar](max) NULL,
 CONSTRAINT [PK_coaches_training] PRIMARY KEY CLUSTERED 
(
	[RegistrationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoachesTrainingRoles]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachesTrainingRoles](
	[ID] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_coaches_training_roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Config]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config](
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](800) NULL,
 CONSTRAINT [PK_config] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactUsRecipients]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUsRecipients](
	[ID] [tinyint] NOT NULL,
	[contact_name] [nvarchar](100) NOT NULL,
	[email_address] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_contact_us_recipients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactUsSenderRoles]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUsSenderRoles](
	[ID] [tinyint] NOT NULL,
	[role_name] [nvarchar](100) NULL,
 CONSTRAINT [PK_contact_us_sender_roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[ID] [int] NOT NULL,
	[EventName] [nvarchar](200) NULL,
	[LocationURL] [nvarchar](200) NULL,
	[LocationURLColor] [nvarchar](100) NULL,
	[LocationAddress] [nvarchar](100) NULL,
	[LocationCity] [nvarchar](30) NULL,
	[LocationState] [nvarchar](5) NULL,
	[LocationPhone] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Location] [nvarchar](100) NULL,
	[Time] [nvarchar](100) NULL,
	[EventCoordinatorName] [nvarchar](100) NULL,
	[EventCoordinatorEmail] [nvarchar](100) NULL,
	[EventCoordinatorPhone] [nvarchar](100) NULL,
	[InformationURL] [nvarchar](100) NULL,
	[LocationMapURL] [nvarchar](100) NULL,
	[EventPayeeName] [nvarchar](100) NULL,
	[EventPayeeAddress1] [nvarchar](100) NULL,
	[EventPayeeAddress2] [nvarchar](100) NULL,
	[EventPayeeCity] [nvarchar](50) NULL,
	[EventPayeeState] [nvarchar](30) NULL,
	[EventPayeeZipCode] [nvarchar](15) NULL,
	[EventPayeePhone1] [nvarchar](20) NULL,
	[EventPayeeEmail1] [nvarchar](100) NULL,
	[EventCost] [nvarchar](20) NULL,
	[LateEventCost] [nvarchar](20) NULL,
	[LateEventCostStartDate] [date] NULL,
	[PaymentDueDate] [date] NULL,
	[EventMakeChecksOutTo] [nvarchar](150) NULL,
	[EventVolunteerInformationMessage] [nvarchar](max) NULL,
	[TeamsVolunteerWantsToSeeMessage] [nvarchar](max) NULL,
	[EventMailBody] [nvarchar](max) NULL,
 CONSTRAINT [PK_events] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Judges]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Judges](
	[JudgeID] [int] IDENTITY(1,1) NOT NULL,
	[TeamID] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[AddressLine2] [nvarchar](255) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](20) NULL,
	[ZipCode] [nvarchar](20) NULL,
	[DaytimePhone] [nvarchar](30) NULL,
	[EveningPhone] [nvarchar](30) NULL,
	[MobilePhone] [nvarchar](30) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
	[ProblemChoice1] [nvarchar](60) NULL,
	[ProblemChoice2] [nvarchar](60) NULL,
	[ProblemChoice3] [nvarchar](60) NULL,
	[HasChildrenCompeting] [nvarchar](3) NULL,
	[COI] [nvarchar](50) NULL,
	[ProblemCOI1] [nvarchar](60) NULL,
	[ProblemCOI2] [nvarchar](60) NULL,
	[ProblemCOI3] [nvarchar](60) NULL,
	[ProblemAssigned] [nvarchar](50) NULL,
	[InformationMailed?] [bit] NULL,
	[AttendedJT?] [bit] NULL,
	[Active] [bit] NULL,
	[WillingToBeScorechecker] [nvarchar](3) NULL,
	[TshirtSize] [nvarchar](50) NULL,
	[WantsCEUCredit] [nvarchar](3) NULL,
	[YearsOfLongTermJudgingExperience] [nvarchar](50) NULL,
	[YearsOfSpontaneousJudgingExperience] [nvarchar](50) NULL,
	[PreviousPositions] [nvarchar](100) NULL,
	[ProblemID] [nvarchar](50) NULL,
	[TimeRegistered] [datetime] NULL,
	[TimeAssignedToTeam] [datetime] NULL,
	[TimeRegistrationStarted] [datetime] NULL,
	[UserAgent] [nvarchar](max) NULL,
 CONSTRAINT [PK_judges] PRIMARY KEY CLUSTERED 
(
	[JudgeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Problem]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Problem](
	[ProblemID] [int] NOT NULL,
	[ProblemCategory] [nvarchar](30) NULL,
	[ProblemName] [nvarchar](50) NULL,
	[ProblemDescription] [nvarchar](max) NULL,
	[Divisions] [nvarchar](50) NULL,
	[CostLimit] [nvarchar](50) NULL,
	[ProblemCaptainID] [nvarchar](50) NULL,
	[PCFirstName] [nvarchar](50) NULL,
	[PCLastName] [nvarchar](50) NULL,
	[PCAddress] [nvarchar](255) NULL,
	[PCCity] [nvarchar](50) NULL,
	[PCStateOrProvince] [nvarchar](20) NULL,
	[PCPostalCode] [nvarchar](20) NULL,
	[PCWorkPhone] [nvarchar](30) NULL,
	[PCHomePhone] [nvarchar](30) NULL,
	[PCMobilePhone] [nvarchar](30) NULL,
	[PCFaxNumber] [nvarchar](30) NULL,
	[PCEmail1] [nvarchar](50) NULL,
	[PCEmail2] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_problem] PRIMARY KEY CLUSTERED 
(
	[ProblemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schools]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schools](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[RegionNumber] [smallint] NULL,
	[Address] [nvarchar](255) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](20) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[Phone] [nvarchar](30) NULL,
	[Membership#1] [nvarchar](50) NULL,
	[Membership#1seen] [nvarchar](50) NULL,
	[Membership#2] [nvarchar](50) NULL,
	[Membership#2seen] [nvarchar](50) NULL,
	[Membership#3] [nvarchar](50) NULL,
	[Membership#3seen] [nvarchar](50) NULL,
	[Membership#4] [nvarchar](50) NULL,
	[Membership#4seen] [nvarchar](50) NULL,
	[CoordNew?] [nvarchar](50) NULL,
	[CoordFirstName] [nvarchar](50) NULL,
	[CoordLastName] [nvarchar](50) NULL,
	[CoordAddress] [nvarchar](255) NULL,
	[CoordCity] [nvarchar](50) NULL,
	[CoordState] [nvarchar](20) NULL,
	[CoordPostalCode] [nvarchar](20) NULL,
	[CoordPhone] [nvarchar](30) NULL,
	[CoordAltPhone] [nvarchar](30) NULL,
	[CoordMobilePhone] [nvarchar](30) NULL,
	[CoordFaxNumber] [nvarchar](30) NULL,
	[CoordEmailName] [nvarchar](50) NULL,
	[Share?] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Schools] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TournamentRegistration]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TournamentRegistration](
	[TeamID] [int] IDENTITY(1,1) NOT NULL,
	[MembershipName] [nvarchar](50) NULL,
	[MembershipNumber] [nvarchar](50) NULL,
	[ProblemID] [int] NULL,
	[Division] [nvarchar](50) NULL,
	[SchoolID] [int] NULL,
	[CoachFirstName] [nvarchar](50) NULL,
	[CoachLastName] [nvarchar](50) NULL,
	[CoachAddress] [nvarchar](255) NULL,
	[CoachCity] [nvarchar](50) NULL,
	[CoachState] [nvarchar](20) NULL,
	[CoachZipCode] [nvarchar](20) NULL,
	[CoachEveningPhone] [nvarchar](30) NULL,
	[CoachDaytimePhone] [nvarchar](30) NULL,
	[CoachMobilePhone] [nvarchar](30) NULL,
	[CoachEmailAddress] [nvarchar](50) NULL,
	[AltCoachFirstName] [nvarchar](50) NULL,
	[AltCoachLastName] [nvarchar](50) NULL,
	[AltCoachEveningPhone] [nvarchar](50) NULL,
	[AltCoachDaytimePhone] [nvarchar](50) NULL,
	[AltCoachMobilePhone] [nvarchar](30) NULL,
	[AltCoachEmailAddress] [nvarchar](50) NULL,
	[MemberFirstName1] [nvarchar](50) NULL,
	[MemberLastName1] [nvarchar](50) NULL,
	[MemberGrade1] [nvarchar](50) NULL,
	[MemberFirstName2] [nvarchar](50) NULL,
	[MemberLastName2] [nvarchar](50) NULL,
	[MemberGrade2] [nvarchar](50) NULL,
	[MemberFirstName3] [nvarchar](50) NULL,
	[MemberLastName3] [nvarchar](50) NULL,
	[MemberGrade3] [nvarchar](50) NULL,
	[MemberFirstName4] [nvarchar](50) NULL,
	[MemberLastName4] [nvarchar](50) NULL,
	[MemberGrade4] [nvarchar](50) NULL,
	[MemberFirstName5] [nvarchar](50) NULL,
	[MemberLastName5] [nvarchar](50) NULL,
	[MemberGrade5] [nvarchar](50) NULL,
	[MemberFirstName6] [nvarchar](50) NULL,
	[MemberLastName6] [nvarchar](50) NULL,
	[MemberGrade6] [nvarchar](50) NULL,
	[MemberFirstName7] [nvarchar](50) NULL,
	[MemberLastName7] [nvarchar](50) NULL,
	[MemberGrade7] [nvarchar](50) NULL,
	[Spontaneous] [bit] NULL,
	[Notes] [nvarchar](max) NULL,
	[SpecialConsiderations] [nvarchar](max) NULL,
	[SchedulingIssues] [nvarchar](max) NULL,
	[Paid] [smallint] NULL,
	[JudgeID] [smallint] NULL,
	[TeamRegistrationFee] [nvarchar](20) NULL,
	[VolunteerID] [int] NULL,
	[TimeRegistrationStarted] [datetime] NULL,
	[TimeRegistered] [datetime] NULL,
	[UserAgent] [nvarchar](max) NULL,
 CONSTRAINT [PK_TournamentRegistration] PRIMARY KEY CLUSTERED 
(
	[TeamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Volunteers]    Script Date: 12/2/2023 8:02:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Volunteers](
	[VolunteerID] [int] IDENTITY(1,1) NOT NULL,
	[TeamID] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[DaytimePhone] [nvarchar](30) NULL,
	[EveningPhone] [nvarchar](30) NULL,
	[MobilePhone] [nvarchar](30) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
	[VolunteerWantsToSee] [nvarchar](max) NULL,
	[TimeRegistrationStarted] [datetime] NULL,
	[TimeRegistered] [datetime] NULL,
	[TimeAssignedToTeam] [datetime] NULL,
	[UserAgent] [nvarchar](max) NULL,
 CONSTRAINT [PK_Volunteers] PRIMARY KEY CLUSTERED 
(
	[VolunteerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[CoachesTrainingDivisions] ([ID], [Name]) VALUES (1, N'Primary')
INSERT [dbo].[CoachesTrainingDivisions] ([ID], [Name]) VALUES (2, N'Division 1')
INSERT [dbo].[CoachesTrainingDivisions] ([ID], [Name]) VALUES (3, N'Division 2')
INSERT [dbo].[CoachesTrainingDivisions] ([ID], [Name]) VALUES (4, N'Division 3')
INSERT [dbo].[CoachesTrainingDivisions] ([ID], [Name]) VALUES (5, N'I Don''t Know Yet')
INSERT [dbo].[CoachesTrainingDivisions] ([ID], [Name]) VALUES (6, N'Not Applicable')
GO
SET IDENTITY_INSERT [dbo].[CoachesTrainingRegions] ON 

INSERT [dbo].[CoachesTrainingRegions] ([ID], [Name]) VALUES (1, N'9')
INSERT [dbo].[CoachesTrainingRegions] ([ID], [Name]) VALUES (2, N'11')
INSERT [dbo].[CoachesTrainingRegions] ([ID], [Name]) VALUES (3, N'12')
INSERT [dbo].[CoachesTrainingRegions] ([ID], [Name]) VALUES (4, N'14')
INSERT [dbo].[CoachesTrainingRegions] ([ID], [Name]) VALUES (5, N'16')
INSERT [dbo].[CoachesTrainingRegions] ([ID], [Name]) VALUES (6, N'I Don''t Know')
SET IDENTITY_INSERT [dbo].[CoachesTrainingRegions] OFF
GO
SET IDENTITY_INSERT [dbo].[CoachesTrainingRegistrations] ON 

INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (147, N'Lali', N'Fisher', N'Hayfield Elementary School', N'5', N'2', N'0', N'hlfisher@fcps.edu', N'0', N'11', CAST(N'2012-10-28T12:54:21.753' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (148, N'Sabrina', N'Hsu', N'Navy ES', N'1', N'1', N'6', N'Brikyun@gmail.com', N'0', N'9', CAST(N'2012-10-28T15:21:25.427' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A403 Safari/8536.25')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (149, N'Anne', N'Kane', N'Frost MS', N'3', N'4', N'3', N'kane.nine@verizon.net', N'5', N'9', CAST(N'2012-10-28T15:43:12.440' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; WOW64; Trident/5.0; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (150, N'Evelynn', N'Euler', N'Frost MS', N'1', N'4', N'3', N'eulere@cox.net', N'1', N'9', CAST(N'2012-10-28T15:46:38.647' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; WOW64; Trident/5.0; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (151, N'Megan', N'Smith', N'Charles E. Smith Jewish Day School', N'1', N'1', N'4', N'Msmith@cesjds.org', N'0', N'I Don''t Know', CAST(N'2012-10-28T19:13:14.330' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (152, N'Joe', N'Carr', N'Forestville Elementary', N'1', N'1', N'6', N'josecarr@deloitte.com', N'', N'9', CAST(N'2012-10-28T19:43:32.220' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; AskTbORJ/5.15.4.23821)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (153, N'Nuria', N'Gabitova', N'Forestville Elementary', N'1', N'1', N'6', N'ngabitova@yahoo.com', N'', N'9', CAST(N'2012-10-28T19:45:16.383' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; AskTbORJ/5.15.4.23821)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (154, N'Loan', N'Hoang', N'Forestville Elementary', N'1', N'1', N'6', N'lorrenity@yahoo.com', N'', N'9', CAST(N'2012-10-28T19:46:39.610' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; AskTbORJ/5.15.4.23821)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (155, N'Yiqing', N'Li', N'Forestville Elementary', N'1', N'2', N'5', N'yiqinghe@hotmail.com', N'', N'9', CAST(N'2012-10-28T19:48:20.123' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; AskTbORJ/5.15.4.23821)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (156, N'Dayan', N'Sankar', N'Forestville Elementary', N'1', N'2', N'5', N'dayan.sankar@gmail.com', N'', N'9', CAST(N'2012-10-28T19:49:34.787' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; AskTbORJ/5.15.4.23821)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (157, N'Anita', N'Wasan', N'Forestville Elementary', N'1', N'2', N'1', N'anitananda@yahoo.com', N'', N'9', CAST(N'2012-10-28T19:51:13.550' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; AskTbORJ/5.15.4.23821)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (158, N'Lakshmi', N'Raman', N'Liberty Elementary', N'2', N'1', N'6', N'lakshmilatest@gmail.com', N'0', N'9', CAST(N'2012-10-29T07:07:48.050' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; InfoPath.2; AskTbORJ/5.15.9.29495)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (159, N'tana', N'reagan', N'forest edge', N'1', N'2', N'5', N'tana.reagan@verizon.net', N'', N'', CAST(N'2012-10-29T08:08:13.917' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET CLR 1.1.4322)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (160, N'Jane', N'Mitchell', N'Round Hill ES', N'1', N'2', N'1', N'jmitchelldesigns@yahoo.com', N'0', N'14', CAST(N'2012-10-29T09:29:40.757' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (161, N'Michelle', N'Salmin', N'Round Hill ES', N'1', N'1', N'6', N'salmins1@att.net', N'0', N'14', CAST(N'2012-10-29T09:31:11.050' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (162, N'Jiong-Ping', N'Lu', N'Frost MS', N'2', N'5', N'0', N'jplu@ieee.org', N'0', N'9', CAST(N'2012-10-29T14:04:55.583' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (163, N'Elizabeth', N'Dougherty', N'Navy ES', N'1', N'1', N'6', N'elizabeth_dougherty_vb@hotmail.com', N'0', N'9', CAST(N'2012-10-30T08:15:08.430' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (164, N'Sonia', N'Yadav', N'Hunters Woods Elemetary', N'1', N'2', N'5', N'sonia.yadav@gmail.com', N'0', N'9', CAST(N'2012-10-30T08:27:28.297' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Win64; x64; Trident/6.0; Touch; BOIE9;ENUSMSE)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (165, N'molly', N'scyrkels', N'Hunter''s Woods Elementary', N'1', N'5', N'3', N'mollyscyrkels@yahoo.com', N'0', N'I Don''t Know', CAST(N'2012-10-30T09:09:10.147' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/536.26.14 (KHTML, like Gecko) Version/6.0.1 Safari/536.26.14')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (166, N'Sudipto', N'Roy', N'Hunters Woods Elementary School', N'1', N'2', N'1', N'sudipto37@yahoo.com', N'0', N'9', CAST(N'2012-10-30T09:12:44.053' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (167, N'Michelle', N'Nyhuis', N'Cedar Lane ES', N'2', N'1', N'6', N'menyhuis@msn.com', N'0', N'16', CAST(N'2012-10-30T09:36:02.807' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A403 Safari/8536.25')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (168, N'Cynthia', N'Foy', N'Cedar  Lane ES', N'1', N'1', N'6', N'cynthiafoy927@gmail.com', N'0', N'16', CAST(N'2012-10-30T09:37:32.397' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A403 Safari/8536.25')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (169, N'Tanuja', N'Ghodgaonkar', N'Navy ES', N'1', N'1', N'6', N'tanujaparag@yahoo.com', N'0', N'9', CAST(N'2012-10-30T14:06:39.660' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (170, N'Min', N'Feng', N'Mantua ES', N'5', N'2', N'0', N'mmfeng@hotmail.com', N'0', N'9', CAST(N'2012-10-30T14:44:43.077' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (171, N'hong', N'zhou', N'hunters woods', N'2', N'5', N'3', N'zh1879@hotmail.com', N'', N'9', CAST(N'2012-10-30T15:35:33.427' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.0; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (172, N'Elizabeth', N'Lower-Basch', N'Belvedere ES', N'1', N'2', N'4', N'elizabethlb@gmail.com', N'0', N'9', CAST(N'2012-10-30T17:28:37.817' AS DateTime), N'Mozilla/5.0 (Windows NT 6.0; rv:16.0) Gecko/20100101 Firefox/16.0')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (173, N'Cynthia', N'Foy', N'Cedar Lane ES', N'1', N'1', N'6', N'cynthiafoy927@gmail.com', N'0', N'16', CAST(N'2012-10-30T17:55:01.257' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; BTRS129061; GTB7.4; chromeframe/22.0.1229.94; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (174, N'Danielle', N'Ferrin', N'Cedar Lane ES', N'1', N'2', N'4', N'mdferrins@comcast.net', N'0', N'16', CAST(N'2012-10-30T17:58:41.173' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; BTRS129061; GTB7.4; chromeframe/22.0.1229.94; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (175, N' Srimati ', N'Raja', N'Cedar Lane ES', N'1', N'2', N'5', N'srimatiraja@gmail.com', N'0', N'16', CAST(N'2012-10-30T18:02:06.877' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; BTRS129061; GTB7.4; chromeframe/22.0.1229.94; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (176, N'Kee Kee', N'Cho', N'Cedar Lane ES', N'2', N'2', N'0', N'kkcho@verizon.net', N'0', N'16', CAST(N'2012-10-30T18:04:26.933' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; BTRS129061; GTB7.4; chromeframe/22.0.1229.94; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (177, N'Rickard', N'Larne', N'Lake Anne ES', N'1', N'2', N'0', N'rlarne@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-10-30T20:22:57.070' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (178, N'Laly', N'Kassa', N'Wakefield Forest Elementary School', N'1', N'5', N'0', N'lalykassa@yahoo.com', N'0', N'I Don''t Know', CAST(N'2012-10-31T06:27:04.340' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; GTB7.4; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; MDDR; InfoPath.3)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (179, N'Helen', N'Deng', N'Forest Edge', N'1', N'2', N'3', N'h_deng2002@yahoo.com', N'0', N'9', CAST(N'2012-10-31T07:00:10.233' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; MS-RTC LM 8; .NET4.0C; .NET4.0E)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (180, N'Michael', N'Scheiwe', N'Wakefield Forest Elementary School', N'1', N'1', N'6', N'michael.a.scheiwe@hp.com', N'1', N'9', CAST(N'2012-10-31T08:09:09.923' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (181, N'Nancy', N'Wadson', N'Belvedere ES', N'1', N'5', N'5', N'nkwadson@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-10-31T12:08:14.557' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (182, N'Sudhir', N'Meduri', N'Frederick Douglass ES, Leesburg', N'1', N'1', N'6', N'anjukothari@yahoo.com', N'0', N'14', CAST(N'2012-10-31T18:22:09.663' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.2; MS-RTC LM 8)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (183, N'Nevein', N'Zalenski', N'Frederick Douglass', N'1', N'1', N'6', N'anjukothari@yahoo.com', N'0', N'14', CAST(N'2012-10-31T18:23:39.083' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.2; MS-RTC LM 8)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (184, N'Shira', N'Jorgensen', N'Hayfield Elementary', N'2', N'5', N'0', N'Shira376@aol.com', N'4', N'I Don''t Know', CAST(N'2012-11-01T05:23:18.117' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; MathPlayer 2.10d; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; BRI/1)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (185, N'Anna', N'Pane', N'Wolftrap ES', N'1', N'5', N'0', N'ANNA.PANE@GMAIL.COM', N'0', N'I Don''t Know', CAST(N'2012-11-01T10:08:28.247' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.52 Safari/537.11')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (186, N'Leslie', N'Carl', N'Wolftrap ES', N'1', N'3', N'0', N'lcarl2b@aol.com', N'0', N'9', CAST(N'2012-11-01T10:13:10.533' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.52 Safari/537.11')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (187, N'Stephanie', N'Berg', N'Wakefield Forest', N'1', N'5', N'6', N'stephanie@berganalytics.com', N'0', N'9', CAST(N'2012-11-01T11:41:42.320' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (188, N'Tina', N'Mally', N'Vienna Elem.', N'1', N'5', N'0', N'tinamally@yahoo.com', N'0', N'9', CAST(N'2012-11-01T15:53:44.543' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; chromeframe/22.0.1229.94; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; MDDC; .NET4.0C; BRI/2)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (189, N'Srimathi', N'Srinivasaraghavan ', N'Cedar Lane ES', N'2', N'2', N'5', N'srimatiraja@gmail.com', N'0', N'16', CAST(N'2012-11-01T19:40:23.437' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; BTRS129061; GTB7.4; chromeframe/22.0.1229.94; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (190, N'Tiffany', N'Hunt', N'Cedar Lane ES', N'2', N'2', N'1', N'uvagirl1996@gmail.com', N'0', N'16', CAST(N'2012-11-01T19:42:23.477' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; BTRS129061; GTB7.4; chromeframe/22.0.1229.94; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (191, N'Diana', N'Pentecost', N'Cedar Lane ES', N'1', N'2', N'5', N'Diana8@aol.com', N'0', N'16', CAST(N'2012-11-01T19:46:00.117' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; BTRS129061; GTB7.4; chromeframe/22.0.1229.94; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (192, N'Neelima', N'Chennamaraja', N'Hunters Woods Elementary School ES', N'2', N'2', N'4', N'neelima.jhu@gmail.com', N'0', N'9', CAST(N'2012-11-02T05:51:29.150' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/534.57.2 (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (193, N'Chris', N'Ammon', N'Bailey''s Elementary', N'1', N'5', N'3', N'chrisammon18@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-02T06:18:20.750' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (194, N'Amanda', N'Aguilera', N'Sleepy Hollow Elementary School', N'3', N'2', N'0', N'amandaaguilera1@verizon.net', N'1', N'9', CAST(N'2012-11-02T06:35:58.813' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (195, N'Jason', N'Iannotti', N'Sleepy Hollow', N'2', N'2', N'0', N'jpiannotti@aol.com', N'0', N'I Don''t Know', CAST(N'2012-11-02T07:20:05.697' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (196, N'Nick', N'Manicone', N'Louise Archer Elementary', N'1', N'2', N'0', N'maniconen@gmail.com', N'0', N'9', CAST(N'2012-11-02T07:28:22.280' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; InfoPath.1; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (197, N'anissa', N'mezzour', N'bailey''s elementary', N'1', N'1', N'6', N'amezzour@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-02T08:09:36.440' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (198, N'Sarah', N'Baker', N'Navy ES', N'2', N'1', N'6', N'sarahbaker648@gmail.com', N'0', N'9', CAST(N'2012-11-02T09:57:53.087' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.2; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; MS-RTC LM 8)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (199, N'Josh', N'Vichness', N'Wakefield Forest Elementary', N'1', N'2', N'0', N'ootm@vichness.com', N'0', N'9', CAST(N'2012-11-02T11:54:22.283' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (200, N'Becky', N'Lee', N'Cedar Lane ES', N'2', N'2', N'4', N'peichiehlee@yahoo.com', N'0', N'16', CAST(N'2012-11-02T13:00:17.577' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; BTRS129061; GTB7.4; chromeframe/22.0.1229.94; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (201, N'Tyler', N'Maxey', N'Lake Anne Elm', N'1', N'5', N'0', N'maxey15@netzero.com', N'0', N'I Don''t Know', CAST(N'2012-11-02T16:27:08.143' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (202, N'Sally', N'Masri', N'Sleepy Hollow Elementary School', N'2', N'2', N'0', N'Sally.masri@me.com', N'0', N'9', CAST(N'2012-11-02T19:01:56.787' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (203, N'Melissa', N'Morrison', N'Reston Montessori School', N'1', N'5', N'0', N'Mbamorrisons@verizon.net', N'0', N'9', CAST(N'2012-11-02T20:27:30.340' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (204, N'Kevin', N'McCabe', N'Our Lady of Hope', N'1', N'3', N'3', N'mccabejk@aol.com', N'5', N'16', CAST(N'2012-11-03T08:39:31.933' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; AOL 9.7; AOLBuild 4343.30; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (205, N'Radhika', N'Murari', N'Hunters Woods Elementary School', N'1', N'2', N'1', N'rmurari@iminit.biz', N'0', N'I Don''t Know', CAST(N'2012-11-04T04:00:51.580' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_4) AppleWebKit/536.25 (KHTML, like Gecko) Version/6.0 Safari/536.25')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (206, N'Ambreen', N'Ghazenfer', N'Alfatih Academy', N'2', N'5', N'0', N'ambreengpk@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-04T05:21:53.400' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:12.0) Gecko/20100101 Firefox/12.0')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (207, N'Ghazenfer', N'Mansoor', N'Alfatih Academy', N'1', N'5', N'0', N'gmansoor@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-04T05:32:20.507' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:12.0) Gecko/20100101 Firefox/12.0')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (208, N'Scott', N'Kestner', N'Hunters Woods ES', N'1', N'5', N'0', N'scottkestner@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-04T06:54:03.463' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (209, N'Beth', N'McMartin', N'Bailey''s Elementary', N'3', N'2', N'2', N'bmcmartin1@yahoo.com', N'1', N'9', CAST(N'2012-11-04T12:28:51.617' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1; rv:6.0.2) Gecko/20100101 Firefox/6.0.2')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (210, N'Rebecca', N'Castagna', N'Forest Edge', N'1', N'2', N'5', N'rebecca1514@gmail.com', N'0', N'9', CAST(N'2012-11-04T13:43:40.547' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_4) AppleWebKit/534.56.5 (KHTML, like Gecko) Version/5.1.6 Safari/534.56.5')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (211, N'Debora', N'Navarro', N'Sleepy Hollow Elementary School', N'2', N'5', N'0', N'demerfn@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-04T14:49:59.930' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB7.4; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E; InfoPath.3)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (212, N'Sunil', N'Taori', N'Rachel Carson MS', N'1', N'3', N'4', N'Coach.Odyssey@gmail.com', N'3', N'I Don''t Know', CAST(N'2012-11-04T18:27:00.627' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (213, N'Grace', N'Juan', N'n/a', N'5', N'5', N'0', N'gracejuan1@yahoo.com', N'0', N'I Don''t Know', CAST(N'2012-11-04T19:56:58.117' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_4) AppleWebKit/534.56.5 (KHTML, like Gecko) Version/5.1.6 Safari/534.56.5')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (214, N'Suyen', N'Michlowitz', N'Catoctin ES', N'1', N'2', N'3', N'michlowitz@verizon.net', N'0', N'14', CAST(N'2012-11-04T21:00:13.833' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/534.57.2 (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (215, N'Susmitha', N'Chintaluri', N'Reston Montessori School', N'1', N'2', N'0', N'susmitheboss@gmail.com', N'0', N'9', CAST(N'2012-11-05T03:54:59.583' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (216, N'Amy', N'Williams', N'Canterbury Woods ES', N'3', N'2', N'0', N'amy@crossoveravs.com', N'0', N'9', CAST(N'2012-11-05T07:29:18.447' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:16.0) Gecko/20100101 Firefox/16.0')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (217, N'Lalit', N'Bedi', N'Louise Archer', N'3', N'2', N'0', N'lalitbedi@gmail.com', N'0', N'9', CAST(N'2012-11-05T07:37:16.960' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (218, N'William', N'Chen', N'Hunters Woods', N'2', N'2', N'4', N'chen899@hotmail.com', N'0', N'9', CAST(N'2012-11-05T10:22:32.880' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (219, N'Shannon', N'Hunter', N'John W. Tolbert ES', N'3', N'6', N'5', N'hunterzack@verizon.net', N'2', N'14', CAST(N'2012-11-05T13:51:54.427' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (220, N'Bill', N'Musgrove', N'The Langley School', N'1', N'3', N'0', N'amymyk@verizon.net', N'0', N'9', CAST(N'2012-11-05T13:54:26.230' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/534.57.2 (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (221, N'Ryan', N'McKinney', N'The Langley School', N'1', N'3', N'0', N'amymyk@verizon.net', N'0', N'9', CAST(N'2012-11-05T13:55:00.923' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/534.57.2 (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (222, N'Lisa', N'Atkinson', N'John W. Tolbert ES', N'1', N'2', N'5', N'lisa@abetterway2fitness.com', N'2', N'14', CAST(N'2012-11-05T13:57:41.323' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (223, N'Ryan', N'Ciolli', N'John W. Tolbert ES', N'1', N'2', N'0', N'rkciolli@gmail.com', N'1', N'14', CAST(N'2012-11-05T13:59:14.487' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (224, N'Raman', N'Singh', N'John W. Tolbert ES', N'1', N'2', N'0', N'rpsingh01@gmail.com', N'0', N'14', CAST(N'2012-11-05T14:00:44.170' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (225, N'David', N'Brookes-Weiss', N'Belvedere Elementary School', N'1', N'1', N'6', N'dweiss@foley.com', N'0', N'9', CAST(N'2012-11-05T16:19:38.127' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.2; Trident/4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E; InfoPath.2; MS-RTC LM 8)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (226, N'Brenda', N'Morton', N'Dominion Trail Elementary School', N'2', N'2', N'0', N'bamorton4b@gmail.com', N'0', N'9', CAST(N'2012-11-06T09:01:32.957' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1; rv:10.0.3) Gecko/20100101 Firefox/10.0.3')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (227, N'Nina', N'Villarreal', N'T Clay Wood Middle School', N'1', N'2', N'5', N'NWVillarreal@yahoo.com', N'0', N'9', CAST(N'2012-11-06T10:28:57.287' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (228, N'Pontea', N'Amiri', N'Montessori School of Fairfax ES', N'1', N'5', N'0', N'msofx@aol.com', N'0', N'9', CAST(N'2012-11-06T10:28:58.003' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (229, N'Nejla ', N'Izadi', N'Montessori School of Fairfax ES', N'1', N'5', N'0', N'msofx@aol.com', N'0', N'9', CAST(N'2012-11-06T10:30:28.357' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (230, N'Melanie', N'Armstrong', N'Edlin', N'1', N'2', N'1', N'melanie.thomas.armstrong@us.pwc.com', N'0', N'9', CAST(N'2012-11-06T13:57:54.317' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; OPT-OUT; GTB7.4; .NET CLR 2.0.50727; .NET4.0C; .NET4.0E; .NET CLR 3.0.04506.30; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 1.1.4322; OPT-OUT)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (231, N'Helen', N'Butler', N'Jamestown Elementary', N'1', N'2', N'0', N'Helenprioleau@hotmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-06T14:46:04.063' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A403 Safari/8536.25')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (232, N'Aasma', N'Farooq', N'Leesburg Elementrary School', N'1', N'5', N'0', N'aasma2@hotmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-07T07:25:22.113' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.2; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (233, N'Renee', N'Boyce', N'Lake Anne ES', N'3', N'2', N'5', N'ReneeBoyce@comcast.net', N'4', N'9', CAST(N'2012-11-07T08:07:25.053' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/534.57.2 (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (234, N'Suzanne', N'Webb', N'Pine Spring ES', N'3', N'3', N'5', N'suzannedwebb@hotmail.com', N'6', N'9', CAST(N'2012-11-07T09:18:22.653' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; MathPlayer 2.20; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (235, N'Sarah', N'Collins', N'Armstrong ES', N'1', N'1', N'6', N'sarahcollins4@gmail.com', N'0', N'9', CAST(N'2012-11-07T09:39:41.650' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (236, N'shabahang', N'sharif', N'Belmont Station Elementary', N'1', N'1', N'6', N'shabs1967@aol.com', N'0', N'14', CAST(N'2012-11-07T12:37:57.247' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.94 Safari/537.4')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (237, N'Vishy', N'Ramanathan', N'WAPLES MILL ELEMENTARY SCHOOL', N'1', N'5', N'0', N'vishyramanathan@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-07T12:48:19.580' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.0; Trident/5.0)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (238, N'Rujuta ', N'Shrotriya', N'Mosby Wood ES', N'3', N'2', N'0', N'rujutashro@gmail.com', N'', N'9', CAST(N'2012-11-07T15:17:08.770' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (239, N'Sowmya', N'Sundaraman', N'Mosby Woods ES', N'1', N'2', N'0', N'sowmya.sundar95@gmail.com', N'0', N'9', CAST(N'2012-11-07T15:19:29.950' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (240, N'Kim', N'Moehnke', N'Legacy Elementary ', N'2', N'1', N'6', N'Kmoehnke@hotmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-07T17:20:08.677' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (241, N'Brian', N'Dack', N'Liberty ES', N'1', N'4', N'0', N'dackbw@gmail.com', N'0', N'I Don''t Know', CAST(N'2012-11-07T18:11:57.290' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/536.26.17 (KHTML, like Gecko) Version/6.0.2 Safari/536.26.17')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (242, N'Kinjal', N'Parikh', N'Navy elementary school', N'2', N'5', N'0', N'jaykinjal@gmail.com', N'1', N'I Don''t Know', CAST(N'2012-11-07T18:35:40.383' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/536.26.14 (KHTML, like Gecko) Version/6.0.1 Safari/536.26.14')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (243, N'Joseph', N'Montie', N'Spring Hill Elementary School', N'3', N'5', N'0', N'jmontie167@aol.com', N'0', N'9', CAST(N'2012-11-07T18:49:07.870' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; GTB7.4; SLCC1; .NET CLR 2.0.50727; InfoPath.2; .NET CLR 3.5.30729; .NET CLR 3.0.30618; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (244, N'Sajeev', N'Malaveetil', N'Mosby Woods Elementary', N'1', N'5', N'0', N'sajeev.malaveetil@gmail.com', N'0', N'9', CAST(N'2012-11-07T23:01:24.780' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; GTB7.4; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; HPNTDF; .NET4.0C)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (245, N'shelley', N'omar', N'Montessori School of Holmes Run', N'3', N'2', N'0', N'shelleyomar@cox.net', N'0', N'9', CAST(N'2012-11-08T07:28:34.413' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/536.26.14 (KHTML, like Gecko) Version/6.0.1 Safari/536.26.14')
GO
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (246, N'Vinita', N'Gandhi', N'Belmont Station', N'2', N'1', N'6', N'vinita.bills@gmail.com', N'0', N'14', CAST(N'2012-11-08T11:41:40.937' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E; InfoPath.2)')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (247, N'Christina', N'Callaway', N'Lake Anne Elementary', N'1', N'2', N'4', N'thecallaways@gmail.com', N'0', N'9', CAST(N'2012-11-08T14:39:29.710' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (248, N'Christina', N'Callaway', N'Lake Anne Elementary', N'1', N'2', N'4', N'thecallaways@gmail.com', N'0', N'9', CAST(N'2012-11-08T14:39:35.123' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11')
INSERT [dbo].[CoachesTrainingRegistrations] ([RegistrationID], [FirstName], [LastName], [SchoolName], [Role], [Division], [SelectedProblem], [EmailAddress], [YearsInvolved], [RegionNumber], [TimeRegistered], [UserAgent]) VALUES (249, N'Kim', N'Pinkston', N'Haycock', N'1', N'5', N'0', N'kppinkston@aol.com', N'0', N'9', CAST(N'2012-11-08T15:33:58.707' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)')
SET IDENTITY_INSERT [dbo].[CoachesTrainingRegistrations] OFF
GO
INSERT [dbo].[CoachesTrainingRoles] ([ID], [Name]) VALUES (1, N'Coach')
INSERT [dbo].[CoachesTrainingRoles] ([ID], [Name]) VALUES (2, N'Assistant Coach')
INSERT [dbo].[CoachesTrainingRoles] ([ID], [Name]) VALUES (3, N'Coordinator')
INSERT [dbo].[CoachesTrainingRoles] ([ID], [Name]) VALUES (4, N'Division 3 Team Captain')
INSERT [dbo].[CoachesTrainingRoles] ([ID], [Name]) VALUES (5, N'I Have No Team Yet')
INSERT [dbo].[CoachesTrainingRoles] ([ID], [Name]) VALUES (6, N'None of the Above')
GO
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'AcceptingPayPal', N'True')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'CoachesHandbookURL', N'')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'CoachesTrainingRegistrationCloseDateTime', N'2013-11-08 22:00')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'CoachesTrainingRegistrationOpenDateTime', N'2013-08-01 00:00')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'ContactUsURL', N'/wp/?page_id=129')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'CoordinatorsDoNotPayCoachesTrainingRegistrationFee', N'True')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'EmailServer', N'mail.novanorth.org')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'EventVolunteerInformationMessage', N'Each team is required to provide one volunteer. &nbsp;As that volunteer, you will be asked to work about 2 hours at morning registration for judges and teams, souvenir sales, the spontaneous waiting area, or other similar jobs. &nbsp;Volunteers will be informed by e-mail of their assignment.')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'HomePage', N'https://www.novanorth.org')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'IsCoachesTrainingRegistrationDown', N'False')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'IsJudgesRegistrationDown', N'False')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'IsTournamentRegistrationDown', N'False')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'IsVolunteerRegistrationDown', N'False')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'JudgesRegistrationCloseDateTime', N'2027-12-31 23:59')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'JudgesRegistrationOpenDateTime', N'2026-06-01 00:00')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'LinkToSynopses', N'https://odysseyofthemind.com/wp-content/uploads/2026/06/Long-Term-Synopses-2027.pdf')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'PrimaryTeamsMayDoSpontaneous', N'True')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'ProgramGuideURL', N'https://www.odysseyofthemind.com/program-guide')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'RegionalDirectorEmail', N'director@novanorth.org')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'RegionalDirectorText', N'our Regional Director')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'RegionName', N'NoVA North')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'RegionNumber', N'9')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'SchoolCoordinatorsDoNotPayMessage', N'<ul><li>Note that School Coordinators do not pay this fee.</li></ul>')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'TeamsVolunteerWantsToSeeMessage', N'Please list the teams that you would like to see perform during the day so we may do our best to schedule around this conflict. &nbsp;For each team, list the school, division, problem name, and coach''s name so that we know which team you mean. &nbsp;Also let us know if there is any time the day of the tournament that you will not be available to serve as a volunteer:')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'TournamentRegistrationCloseDateTime', N'2027-12-31 23:59')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'TournamentRegistrationOpenDateTime', N'2026-06-01 00:00')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'VolunteerNotesMessage', N'Please provide us with any other information you would like to share as part of your registration.&nbsp; For example, this is a good place to let us know if you have any previous experience volunteering at Odyssey tournaments, and if so, what that was. &nbsp;Also let us know if you would be interested in doing that again or if you would prefer to do something else. &nbsp;Please keep your comments to 500 characters or less:')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'VolunteerRegistrationCloseDateTime', N'2014-1-18 14:00')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'VolunteerRegistrationOpenDateTime', N'2013-11-10 00:00')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'WebmasterEmail', N'webmaster@novanorth.org')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'WebmasterEmailPassword', N'$(webmaster_email_password)')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'WillHaveCoachesTrainingRegistration', N'False')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'WillHaveVolunteerRegistration', N'False')
INSERT [dbo].[Config] ([Name], [Value]) VALUES (N'Year', N'2026')
GO
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (1, N'Vehicle Problem Captain (Problem # 1)', N'problem1@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (2, N'Technical Problem Captain (Problem # 2)', N'problem2@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (3, N'Classics Problem Captain (Problem # 3)', N'problem3@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (4, N'Structure Problem Captain (Problem # 4)', N'problem4@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (5, N'Performance Problem Captain (Problem # 5)', N'problem5@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (6, N'Spontaneous Problem Captain', N'spontaneous@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (7, N'Primary Problem Captain', N'primary@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (8, N'----------------------------', N'')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (9, N'The Regional Directors', N'director@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (10, N'The Regional Board', N'board@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (11, N'The Regional Treasurer', N'treasurer@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (12, N'The Regional Secretary', N'secretary@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (13, N'The Regional Webmaster', N'webmaster@vaodyssey13.org')
INSERT [dbo].[ContactUsRecipients] ([ID], [contact_name], [email_address]) VALUES (14, N'The Regional Judges Coordinator', N'judgescoordinator@vaodyssey13.org')
GO
INSERT [dbo].[ContactUsSenderRoles] ([ID], [role_name]) VALUES (1, N'Coordinator')
INSERT [dbo].[ContactUsSenderRoles] ([ID], [role_name]) VALUES (2, N'Coach')
INSERT [dbo].[ContactUsSenderRoles] ([ID], [role_name]) VALUES (3, N'Parent')
INSERT [dbo].[ContactUsSenderRoles] ([ID], [role_name]) VALUES (4, N'Judge')
INSERT [dbo].[ContactUsSenderRoles] ([ID], [role_name]) VALUES (5, N'Team Member')
INSERT [dbo].[ContactUsSenderRoles] ([ID], [role_name]) VALUES (6, N'Other')
GO
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (10, N'Coordinators Meeting', N'https://www.fairfaxcounty.gov/library/branches/ch/direct.htm', N'#FFFF00', NULL, NULL, NULL, NULL, CAST(N'2019-09-26T00:00:00.000' AS DateTime), NULL, N'Dolley Madison Library', N'7:00 to 8:30 PM', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (20, N'Coordinators Meeting #2', N'https://www.fairfaxcounty.gov/library/BRANCHES/po/', N'#FFFF00', NULL, NULL, NULL, NULL, CAST(N'2009-10-21T00:00:00.000' AS DateTime), NULL, N'Pohick Regional Library conference room', N'6:45 to 7:45 PM', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (30, N'Coaches'' Training', N'https://www.fcps.edu/HughesMS/', N'#FFFF00', NULL, NULL, NULL, NULL, CAST(N'2018-10-20T00:00:00.000' AS DateTime), NULL, N'MITRE CORP', N'Check-in begins at 8:30 am. Program runs from 9:00 am to 4:30 pm.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'20.00', NULL, NULL, NULL, N'NoVA North Odyssey of the Mind', NULL, NULL, N'<p><strong>Thank you for being an Odyssey coach!</strong></p><p>You are registered for:</p><div><strong>Coaches Training in NoVA South (<span>Region</span>)</strong></div>    <li><strong>Location:</strong> <span>Location</span></li>   <li><b>Date: </b><span>Date</span></li>    <li><b>Time: </b><span>Time</span></li></ul><p>Please remember to bring the following:</p><ul>    <li>Lunch (we will provide beverages),</li>    <li>a copy of the <span>Years</span> Program Guide (available at <span>ProgramGuide</span>),</li>    <span>VirginiaHandbook</span>    <li>A check for $<span>Fee</span> made payable to "<span>MakeChecksOutTo</span>".        <span>CoordinatorsDoNotPay</span>    </li></ul><p>We look forward to seeing you there.</p><p>    If you have any questions or need to discuss anything regarding your registration,contact <span>RegionalDirectorEmail</span>.</p>')
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (60, N'Regional Tournament Team Registration Deadline', NULL, NULL, NULL, NULL, N'VA', NULL, CAST(N'2024-01-15T00:00:00.000' AS DateTime), CAST(N'2024-01-15T00:00:00.000' AS DateTime), NULL, N'11 PM', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (70, N'Coaches Q&A', N'https://tinyurl.com/4dlmc2u', N'#FFFF00', N'13625 Eds Blvd', N'Herndon', N'VA', NULL, CAST(N'2023-12-02T00:00:00.000' AS DateTime), CAST(N'2023-12-03T00:00:00.000' AS DateTime), N'Sully District Community Rooms', N'9:30 AM to 12:45 PM, 1 PM - 4 PM', N'Michelle O''Brien', N'director@novanorth.org', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (90, N'Judges Training', NULL, N'#FFFF00', N'6801 Union Mill Road', N'Clifton', N'VA', NULL, CAST(N'2027-02-20T00:00:00.000' AS DateTime), CAST(N'2027-02-20T00:00:00.000' AS DateTime), N'Liberty Middle School', N'8:30 AM - 2 PM', N'Margaret Eccles', N'judgescoordinator@novanorth.org', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'<div id="contentstart"><p style="text-align: center"><b>You have been assigned Judge ID <span>JudgeID</span></b></p>
<p><b>Congratulations, your judge registration is complete.&nbsp; Please print this page for your records.</b></p>
<p>Our system has registered you with the following information: 
<ul><li>Judge ID:   <span>JudgeID</span></li>
<li>First Name: <span>FirstName</span></li>
<li>Last Name:  <span>LastName</span></li>
</ul>
<p>Should a coach request that you be a judge for their team, please provide them with all of this information.&nbsp; 
Please do not give out your ID number to more than one team since you may only judge on behalf of <b>ONE</b> team.</p>
<p>As a reminder, you have agreed to attend the following two events:</p>
<ul><li><b>Region <span>Region</span> Odyssey of the Mind Judges Training</b><br /><br />
<ul><li> <b>Location:</b> <span>JudgesTrainingLocation</span></li>
<li> <b>Date:</b> <span>JudgesTrainingDate</span></li>
<li> <b>Time:</b> <span>JudgesTrainingTime</span></li>
</ul></li></ul>

<ul>
<li>
<b>Region <span>Region</span> Odyssey of the Mind Tournament</b><br /><br />
<ul>
<li> <b>Location:</b> <span>TournamentLocation</span></li>
<li> <b>Date:</b> <span>TournamentDate</span></li>
<li> <b>Time:</b> <span>TournamentTime</span></li>
</ul>
</li>
</ul>

<p>In February, you will receive information about Judges Training which will include the problem you have been
assigned to and any other information that you will need.</p>
<p>We will serve breakfast on the morning of Judges Training, including coffee and juice. &nbsp;Breakfast typically 
consists of bagels and muffins. &nbsp;<span style="font-weight: bold;">You will need to bring a packed lunch to Judges
Training.</span> &nbsp;At the tournament, we will provide breakfast and lunch for judges.</p>
<p>If you have any questions or find you cannot attend either judges training or the tournament, please use our 
<a href="<span>ContactUsURL</span>" target="_blank">Contact Us</a> page to reach the Judges Coordinator.</p>

<p>If you agreed to judge on behalf of a team and find that you cannot attend either Judges Training or the tournament, you must contact the 
coach and advise him/her so that the team knows they need to find another judge.</p>

<p>We invite you to explore our web site at <a href="https://www.novanorth.org" target="_blank">www.novanorth.org</a> to learn more about Odyssey of the Mind and the NoVA North Region.
A good starting point are these two brief videos:<p>
<ul>
<li><a href="https://www.novanorth.org/wp/?page_id=2827" target="_blank">A Creative Experience</a> (https://www.novanorth.org/wp/?page_id=2827)</li>
</ul>

<p>Our NoVA North Regional Board has a few openings for those who would like to become more involved. &nbsp;Please
contact our Regional Director through our <a href="<span>ContactUsURL</span>" target="_blank">Contact Us</a> page if
you are interested or would like further information.</p>

<p align="center"><b>You have been assigned Judge ID <span>JudgeID</span></b></p>
</div>
')
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (95, N'Volunteer Registration', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'<div id="contentstart"><p style="text-align: center"><b>You have been assigned Volunteer ID <span>VolunteerID</span>.</b></p>
<p><b>Congratulations, your volunteer registration is complete.&nbsp; Please print this page for your records.</b></p>
<p>Our system has registered you with the following information: 
<ul><li>Volunteer ID:   <span>VolunteerID</span></li>
<li>First Name: <span>FirstName</span></li>
<li>Last Name:  <span>LastName</span></li>
</ul>
<p>Our Volunteer Coordinator will reach out to you in late January/early February to clarify your scheduled volunteer time and assignment.</p>
<p>Volunteers will be responsible for their own breakfast and lunch at the tournament.</p>
<p>Should a coach request that you be a volunteer for their team, please provide them with all of this information.&nbsp; 
Please do not give out your ID number to more than one team since you may only volunteer on behalf of <b>ONE</b> team.</p>
<p>As a reminder, you have agreed to attend the following event:</p>

<ul>
<li>
<b>Region <span>Region</span> Odyssey of the Mind Tournament</b><br /><br />
<ul>
<li> <b>Location:</b> <span>TournamentLocation</span></li>
<li> <b>Date:</b> <span>TournamentDate</span></li>
<li> <b>Time:</b> <span>TournamentTime</span></li>
</ul>
</li>
</ul>

<p>If you have any questions or find you cannot attend the tournament, please use our 
<a href="<span>ContactUsURL</span>" target="_blank">Contact Us</a> page to reach the Volunteer Coordinator.</p>

<p>If you agreed to volunteer on behalf of a team and find that you cannot attend the tournament, you must contact the
coach and advise him/her so that the team knows they need to find another volunteer.</p>

<p>We invite you to explore our web site at <a href="https://www.novanorth.org" target="_blank">www.novanorth.org</a> to learn more about Odyssey of the Mind and the NoVA North Region.
A good starting point are these two brief videos:<p>
<ul>
<li><a href="https://www.novanorth.org/wp/?page_id=2827" target="_blank">A Creative Experience</a> (https://www.novanorth.org/wp/?page_id=2827)</li>
</ul>
<p>Our NoVA North Regional Board has a few openings for those who would like to become more involved. &nbsp;Please
contact our Regional Director through our <a href="<span>ContactUsURL</span>" target="_blank">Contact Us</a> page if
you are interested or would like further information.</p>

<p align="center"><b>You have been assigned Volunteer ID <span>VolunteerID</span>.</b></p>
</div>
')
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (100, N'NoVA North Regional Tournament', NULL, N'#FFFF00', N'2900 Sutton Road', N'Vienna', N'VA', NULL, CAST(N'2027-03-06T08:00:00.000' AS DateTime), NULL, N'Oakton High School', N'8 AM to 5 PM', N'Michelle O''Brien', N'director@novanorth.org', NULL, NULL, NULL, N'Nova North, Inc.', N'PO Box 302', NULL, N'Oakton', N'VA', N'22124', NULL, N'treasurer@novanorth.org', N'100', NULL, NULL, CAST(N'2027-01-31' AS Date), N'NoVA North Odyssey of the Mind', NULL, NULL, N'<div id=contentstart>
    <p style="text-align: center"><b>You have been assigned Team ID <span>TeamID</span></b></p>
    <p>Dear Coach <span>CoachLastName</span>,</p>
    <p>
        You have registered the team listed below.&nbsp; Your judge is listed below,
        as well.&nbsp; Please remind your judge that the team is counting on him or her
        to attend both Judges Training and the Regional Tournament.<br />

    </p>
    <p>
        We recommend that you print this page and keep it for your records. &nbsp;If you need to change any information
        that you entered during your registration,
        please send an e-mail to the Webmaster for this site and include your Team ID # from this registration.
    </p>

    <p>
        <b>
            For this registration to be complete, the treasurer must
            <span style="text-decoration: underline">receive</span> payment of the team’s <span>EventCost</span> <span>FriendlyRegistrationName</span>
            fee no later than <span>PaymentDueDate</span>
        </b>. &nbsp;Payment must be by check, made out to &quot;<span>EventMakeChecksOutTo</span>&quot;,
        and mailed to the address below. &nbsp;Please put your team ID number (see top of this page) on the check. &nbsp;If your team’s
        <span>FriendlyRegistrationName</span> fee is being paid as one of a group of teams (for example, the school PTA is
        paying for all teams) be sure to advise your school coordinator of your team’s ID number so that all team ID numbers
        can be included on the group check.
    </p>

    <p>Mail your payment to:</p>
    <blockquote>
        <span>EventPayeeName</span><br />
        <span>EventPayeeAddress1</span><br />
        <span>EventPayeeAddress2</span><br />
        <span>EventPayeeCity</span>, <span>EventPayeeState</span> <span>EventPayeeZipCode</span>
    </blockquote>

    <p>
        You may also contact the Treasurer from our <a href="<span>ContactUsURL</span>" target="_blank">Contact Us</a> page.
    </p>

    <p>Please do not request a receipt from the Treasurer. &nbsp;Your cancelled check is your payment receipt.</p>

    <p>Thanks for signing up!</p>

    <table border=''0'' cellpadding=''0'' cellspacing=''0'' style=''border-collapse: collapse'' id=''AutoNumber16'' width=''736''>
        <tr>
            <td width=''736'' colspan=''2'' align=''center''><hr /></td>
        <tr>
            <td width=''736'' colspan=''2'' align=''center''><b>Your School:</b></td>
        <tr>
            <td width=''305''><br /><b>Name:</b></td>
            <td width=''431''>
                <span>SchoolName</span>
        </tr>
        <tr>
            <td width=''736'' colspan=''2''>
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan=''2''><p align=''center''><b>Your Team:</b></td>
        </tr>
        <tr>
            <td width=''304''><br /><b>Name:</b></td>
            <td rowspan=''2''>
                <u>
                    <br /><b>
                        Official team names and membership numbers will be announced
                        in the Tournament Schedule when it is posted on the regional web site.
                    </b>
                </u><br />
            </td>
        </tr>
        <tr>
            <td width=''304''><b>Membership Number:</b></td>
        </tr>
        <tr>
            <td width=''304''><b>Division:</b></td>
            <td width=''185''>
                <span>Division</span>
            </td>
        </tr>
        <span>SpontaneousOrProblemName</span>
        <tr>
            <td colspan=''2''><hr /></td>
        </tr>
        <tr>
            <td width=''736'' colspan=''2''>
                <p align=''center''><b>Your Judge:</b>
            </td>
        </tr>
        <tr>
            <td width=''305''>
                <br /><b>ID Number:</b>
            </td>
            <td width=''431''><br /><span>JudgeID</span></td>
        </tr>
        <tr>
            <td width=''305''>
                <b>First Name:</b>
            </td>
            <td width=''431''><span>JudgeFirstName</span></td>
        </tr>
        <tr>
            <td width=''305''>
                <b>Last Name:</b>
            </td>
            <td width=''431''><span>JudgeLastName</span></td>
        </tr>
        <tr>
            <td colspan=''2''><hr /></td>
        </tr>
        <td colspan=''2''>
            <p align=''center''><b>Your Volunteer:</b>
        </td>
        <tr>
            <td width=''305''>
                <br /><b>ID Number:</b>
            </td>
            <td width=''431''><br /><span>VolunteerID</span></td>
        </tr>
        <tr>
            <td width=''305''><b>First Name:</b></td>
            <td width=''431''><span>VolunteerFirstName</span></td>
        </tr>
        <tr>
            <td width=''305''><b>Last Name:</b></td>
            <td width=''431''><span>VolunteerLastName</span></td>
        </tr>
        <tr>
            <td colspan=''2''><hr /></td>
        </tr>
        <tr>
            <td colspan=''2''>
                <p align=''center''><b>Your Coach:</b>
            </td>
        </tr>
        <tr>
            <td>
                <br /><b>First Name:</b>
            </td>
            <td><br /><span>CoachFirstName</span></td>
        </tr>
        <tr>
            <td>
                <b>Last Name:</b>
            </td>
            <td><span>CoachLastName</span></td>
        </tr>
        <tr>
            <td>
                <b>Address:</b>
            </td>
            <td><span>CoachAddress</span></td>
        </tr>
        <tr>
            <td>
                <b>City:</b>
            </td>
            <td><span>CoachCity</span></td>
        </tr>
        <tr>
            <td>
                <b>State:</b>
            </td>
            <td><span>CoachState</span></td>
        </tr>
        <tr>
            <td>
                <b>Zip Code:</b>
            </td>
            <td><span>CoachZipCode</span></td>
        </tr>
        <tr>
            <td>
                <b>Evening Phone:</b>
            </td>
            <td><span>CoachEveningPhone</span></td>
        </tr>
        <tr>
            <td>
                <b>Daytime Phone:</b>
            </td>
            <td><span>CoachDaytimePhone</span></td>
        </tr>
        <tr>
            <td>
                <b>Mobile Phone:</b>
            </td>
            <td><span>CoachMobilePhone</span></td>
        </tr>
        <tr>
            <td width=''304''>
                <b>E-mail:</b>
            </td>
            <td><span>CoachEmailAddress</span></td>
        </tr>
        <tr>
            <td colspan=''2''><hr /></td>
        </tr>

        <tr>
            <td colspan=''2''><p align=''center''><b>Your Alternate Coach:</b></td>
        </tr>
        <tr>
            <td width=''304''><br /><b>First Name:</b></td>
            <td><br /><span>AltCoachFirstName</span></td>
        </tr>
        <tr>
            <td>
                <b>Last Name:</b>
            </td>
            <td><span>AltCoachLastName</span></td>
        </tr>
        <tr>
            <td>
                <b>Evening Phone:</b>
            </td>
            <td><span>AltCoachEveningPhone</span></td>
        </tr>
        <tr>
            <td>
                <b>Daytime Phone:</b>
            </td>
            <td><span>AltCoachDaytimePhone</span></td>
        </tr>
        <tr>
            <td>
                <b>Mobile Phone:</b>
            </td>
            <td><span>AltCoachMobilePhone</span></td>
        </tr>
        <tr>
            <td>
                <b>E-mail:</b>
            </td>
            <td><span>AltCoachEmailAddress</span></td>
        </tr>
        <tr>
            <td colspan=''2'' style="text-align: center">
                <hr />
            </td>
        </tr>
        <span>FirstTeamMember</span>
        <span>SecondTeamMember</span>
        <span>ThirdTeamMember</span>
        <span>FourthTeamMember</span>
        <span>FifthTeamMember</span>
        <span>SixthTeamMember</span>
        <span>SeventhTeamMember</span>

        <tr>
            <td width=''736'' colspan=''2'' style="text-align: center"><b>Scheduling Issues:</b></td>
        </tr>
        <tr>
            <td colspan=''2''>
                <br />
                <span>SchedulingIssues</span>
            </td>
        </tr>
        <tr><td colspan=''2''><hr /></td></tr>
        <tr>
            <td width=''736'' colspan=''2'' style="text-align: center"><b>Special Considerations:</b></td>
        </tr>
        <tr>
            <td colspan=''2''>
                <br />
                <span>SpecialConsiderations</span>
            </td>
        </tr>
    </table>

    <p style="text-align: center"><b>You have been assigned Team ID <span>TeamID</span></b></p>
</div>')
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (110, N'Virginia State Tournament', NULL, NULL, N'801 N King St', N'Leesburg', N'VA', NULL, CAST(N'2023-04-15T00:00:00.000' AS DateTime), NULL, N'Tuscarora High School', N'All Day', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Events] ([ID], [EventName], [LocationURL], [LocationURLColor], [LocationAddress], [LocationCity], [LocationState], [LocationPhone], [StartDate], [EndDate], [Location], [Time], [EventCoordinatorName], [EventCoordinatorEmail], [EventCoordinatorPhone], [InformationURL], [LocationMapURL], [EventPayeeName], [EventPayeeAddress1], [EventPayeeAddress2], [EventPayeeCity], [EventPayeeState], [EventPayeeZipCode], [EventPayeePhone1], [EventPayeeEmail1], [EventCost], [LateEventCost], [LateEventCostStartDate], [PaymentDueDate], [EventMakeChecksOutTo], [EventVolunteerInformationMessage], [TeamsVolunteerWantsToSeeMessage], [EventMailBody]) VALUES (120, N'Odyssey of the Mind World Finals', N'https://www.iastate.edu/', N'#FFFF00', N'2229 Lincoln Way', N'Ames', N'IA', NULL, CAST(N'2023-05-24T00:00:00.000' AS DateTime), CAST(N'2023-05-27T00:00:00.000' AS DateTime), N'Iowa State University', N'All Day', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO

SET IDENTITY_INSERT [dbo].[Judges] ON
INSERT INTO [dbo].[Judges] ([JudgeID], [TeamID], [FirstName], [LastName], [Address], [AddressLine2], [City], [State], [ZipCode], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [ProblemChoice1], [ProblemChoice2], [ProblemChoice3], [HasChildrenCompeting], [COI], [ProblemCOI1], [ProblemCOI2], [ProblemCOI3], [ProblemAssigned], [InformationMailed?], [AttendedJT?], [Active], [WillingToBeScorechecker], [TshirtSize], [WantsCEUCredit], [YearsOfLongTermJudgingExperience], [YearsOfSpontaneousJudgingExperience], [PreviousPositions], [ProblemID], [TimeRegistered], [TimeAssignedToTeam], [TimeRegistrationStarted], [UserAgent]) VALUES (1, NULL, N'John', N'Smith', N'123 Main St', NULL, N'Los Angeles', N'CA', N'90210', N'(213) 123-4567', N'(213) 234-5678', N'(213) 345-6789', N'john@hotmail.com', N'Here is a comment for the Odyssey board', N'1', N'2', N'3', N'No', NULL, N'0', N'0', N'0', NULL, NULL, NULL, NULL, N'No', N'XXL', N'No', N'12', N'15', N'Head Judge;Style Judge;Timekeeper', NULL, N'2025-06-13 21:15:02', NULL, N'2025-06-13 21:14:13', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/137.0.0.0 Safari/537.36 Edg/137.0.0.0')
GO
SET IDENTITY_INSERT [dbo].[Judges] OFF
GO

INSERT [dbo].[Problem] ([ProblemID], [ProblemCategory], [ProblemName], [ProblemDescription], [Divisions], [CostLimit], [ProblemCaptainID], [PCFirstName], [PCLastName], [PCAddress], [PCCity], [PCStateOrProvince], [PCPostalCode], [PCWorkPhone], [PCHomePhone], [PCMobilePhone], [PCFaxNumber], [PCEmail1], [PCEmail2], [Notes]) VALUES (0, NULL, N'I Don''t Know', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Problem] ([ProblemID], [ProblemCategory], [ProblemName], [ProblemDescription], [Divisions], [CostLimit], [ProblemCaptainID], [PCFirstName], [PCLastName], [PCAddress], [PCCity], [PCStateOrProvince], [PCPostalCode], [PCWorkPhone], [PCHomePhone], [PCMobilePhone], [PCFaxNumber], [PCEmail1], [PCEmail2], [Notes]) VALUES (1, N'Vehicle', N'P1:Drive-in Movie', N'', N'I, II, III, & IV', N'$145 USD', N'1', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'')
INSERT [dbo].[Problem] ([ProblemID], [ProblemCategory], [ProblemName], [ProblemDescription], [Divisions], [CostLimit], [ProblemCaptainID], [PCFirstName], [PCLastName], [PCAddress], [PCCity], [PCStateOrProvince], [PCPostalCode], [PCWorkPhone], [PCHomePhone], [PCMobilePhone], [PCFaxNumber], [PCEmail1], [PCEmail2], [Notes]) VALUES (2, N'Technical', N'P2:AI Tech-No-Art', N'', N'I, II, & III', N'$145 USD', N'2', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'')
INSERT [dbo].[Problem] ([ProblemID], [ProblemCategory], [ProblemName], [ProblemDescription], [Divisions], [CostLimit], [ProblemCaptainID], [PCFirstName], [PCLastName], [PCAddress], [PCCity], [PCStateOrProvince], [PCPostalCode], [PCWorkPhone], [PCHomePhone], [PCMobilePhone], [PCFaxNumber], [PCEmail1], [PCEmail2], [Notes]) VALUES (3, N'Classics', N'P3: Classics... Opening Night Antics', N'', N'I, II, III & IV', N'$125 USD', N'3', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'')
INSERT [dbo].[Problem] ([ProblemID], [ProblemCategory], [ProblemName], [ProblemDescription], [Divisions], [CostLimit], [ProblemCaptainID], [PCFirstName], [PCLastName], [PCAddress], [PCCity], [PCStateOrProvince], [PCPostalCode], [PCWorkPhone], [PCHomePhone], [PCMobilePhone], [PCFaxNumber], [PCEmail1], [PCEmail2], [Notes]) VALUES (4, N'Structure', N'P4: Deep Space Structure', N'', N'I, II, III & IV', N'$145 USD', N'4', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'')
INSERT [dbo].[Problem] ([ProblemID], [ProblemCategory], [ProblemName], [ProblemDescription], [Divisions], [CostLimit], [ProblemCaptainID], [PCFirstName], [PCLastName], [PCAddress], [PCCity], [PCStateOrProvince], [PCPostalCode], [PCWorkPhone], [PCHomePhone], [PCMobilePhone], [PCFaxNumber], [PCEmail1], [PCEmail2], [Notes]) VALUES (5, N'Drama', N'P5:Rocking World Detour', N'', N'I, II, III, & IV', N'$125 USD', N'5', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'')
INSERT [dbo].[Problem] ([ProblemID], [ProblemCategory], [ProblemName], [ProblemDescription], [Divisions], [CostLimit], [ProblemCaptainID], [PCFirstName], [PCLastName], [PCAddress], [PCCity], [PCStateOrProvince], [PCPostalCode], [PCWorkPhone], [PCHomePhone], [PCMobilePhone], [PCFaxNumber], [PCEmail1], [PCEmail2], [Notes]) VALUES (6, N'Primary', N'Dinos on Parade', N'', N'Grades K-2', N'$125 USD', N'6', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'')
INSERT [dbo].[Problem] ([ProblemID], [ProblemCategory], [ProblemName], [ProblemDescription], [Divisions], [CostLimit], [ProblemCaptainID], [PCFirstName], [PCLastName], [PCAddress], [PCCity], [PCStateOrProvince], [PCPostalCode], [PCWorkPhone], [PCHomePhone], [PCMobilePhone], [PCFaxNumber], [PCEmail1], [PCEmail2], [Notes]) VALUES (7, NULL, N'Spontaneous', N'No synopsis. – The team is presented with a problem they have never seen before, to be solved on-the-spot.  The problem may be verbal, hands-on, or a combination.', N'All', N'N/A', N'7', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[Schools] ON 

INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (8, N'Crossfield Elementary (27938)', 9, NULL, NULL, NULL, NULL, NULL, N'27938', N'yes', N'30742', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (10, N'Forest Edge Elementary (30388)', 9, NULL, NULL, NULL, NULL, NULL, N'30388', N'yes', N'32637', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (11, N'Fox Mill Elementary (28383)', 9, NULL, NULL, NULL, NULL, NULL, N'28383', N'yes', N'34496', N'no', N'45986', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (16, N'Hunters Woods Elementary (26974)', 9, NULL, NULL, NULL, NULL, NULL, N'26974', N'yes', N'30754', N'no', N'35987', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (19, N'Lake Anne Elementary (28584)', 9, NULL, NULL, NULL, NULL, NULL, N'28584', N'yes', N'32582', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (21, N'Langston Hughes Middle (24750)', 9, NULL, NULL, NULL, NULL, NULL, N'24750', N'yes', N'19807', N'no', N'22130', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (25, N'Mosaic Elementary (34282)', 9, NULL, NULL, NULL, NULL, NULL, N'34282', N'yes', N'40074', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (28, N'Oakton Elementary (29523)', 9, NULL, NULL, NULL, NULL, NULL, N'29523', N'yes', N'33515', N'no', N'37065', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (29, N'Oakton High (32076)', 9, NULL, NULL, NULL, NULL, NULL, N'32076', N'yes', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (35, N'Rachel Carson Middle (31253)', 9, NULL, NULL, NULL, NULL, NULL, N'31253', N'yes', N'31523', N'no', N'34464', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (42, N'South Lakes High (32307)', 9, NULL, NULL, NULL, NULL, NULL, N'32307', N'yes', N'35333', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (44, N'Sunrise Valley Elementary (30527)', 9, NULL, NULL, NULL, NULL, NULL, N'30527', N'yes', N'43289', N'no', N'43290', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (46, N'Waples Mill Elementary (27319)', 9, NULL, NULL, NULL, NULL, NULL, N'27319', N'no', N'45224', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (69, N'Capital Baptist Homeschool (34281)', 9, NULL, NULL, NULL, NULL, NULL, N'34281', N'no', N'35853', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (71, N'Clearview Elementary (34978)', 9, NULL, NULL, NULL, NULL, NULL, N'34978', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (72, N'Colvin Run Elementary (34412)', 9, NULL, NULL, NULL, NULL, NULL, N'34412', N'no', N'35858', N'no', N'41103', N'no', N'45229', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (73, N'Cooper Middle (32330)', 9, NULL, NULL, NULL, NULL, NULL, N'32330', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (74, N'Fairhill Elementary (34615)', 9, NULL, NULL, NULL, NULL, NULL, N'34615', N'no', N'40042', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (75, N'Forestville Elementary (25649)', 9, NULL, NULL, NULL, NULL, NULL, N'25649', N'no', N'32449', N'no', N'42331', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (76, N'James Madison High (34337)', 9, NULL, NULL, NULL, NULL, NULL, N'34337', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (77, N'Kilmer Middle (37402)', 9, NULL, NULL, NULL, NULL, NULL, N'37402', N'no', N'20571', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (78, N'Louise Archer Elementary A (31526)', 9, NULL, NULL, NULL, NULL, NULL, N'31526', N'no', N'44280', N'yes', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (79, N'Marshall Road Elementary (32229)', 9, NULL, NULL, NULL, NULL, NULL, N'32229', N'no', N'33405', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (80, N'Montessori School of McLean (35043)', 9, NULL, NULL, NULL, NULL, NULL, N'35043', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (83, N'Pine Spring Elementary (33080)', 9, NULL, NULL, NULL, NULL, NULL, N'33080', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (86, N'Vienna Elementary (25021)', 9, NULL, NULL, NULL, NULL, NULL, N'25021', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (89, N'Woodburn Elementary (34491)', 9, NULL, NULL, NULL, NULL, NULL, N'34491', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (90, N'Lemon Road Elementary (32154)', 9, NULL, NULL, NULL, NULL, NULL, N'32154', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (94, N'Camelot Elementary (35600)', 9, NULL, NULL, NULL, NULL, NULL, N'35600', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (99, N'Great Falls Elementary (35508)', 9, NULL, NULL, NULL, NULL, NULL, N'35508', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (100, NULL, 9, NULL, NULL, NULL, NULL, NULL, NULL, N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (101, N'Luther Jackson Middle (38143)', 9, NULL, NULL, NULL, NULL, NULL, N'38143', N'no', N'44869', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (102, N'Navy Elementary (35835)', 9, NULL, NULL, NULL, NULL, NULL, N'35835', N'no', N'42382', N'no', N'43440', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (105, N'Wolftrap Elementary (30618)', 9, NULL, NULL, NULL, NULL, NULL, N'30618', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (107, N'Dranesville Elementary (36733)', 9, NULL, NULL, NULL, NULL, NULL, N'36733', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (113, N'Westlawn Elementary (36621)', 9, NULL, NULL, NULL, NULL, NULL, N'36621', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (114, N'Herndon High (36944)', 9, NULL, NULL, NULL, NULL, NULL, N'36944', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (115, N'Timber Lane Elementary (36673)', 9, NULL, NULL, NULL, NULL, NULL, N'36673', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (120, N'St. Mark Catholic (37259)', 9, NULL, NULL, NULL, NULL, NULL, N'37259', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (127, N'Falls Church High (38263)', 9, NULL, NULL, NULL, NULL, NULL, N'38263', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (131, N'Hutchison Elementary (38715)', 9, NULL, NULL, NULL, NULL, NULL, N'38715', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (135, N'Montessori School of Holmes Run (39059)', 9, NULL, NULL, NULL, NULL, NULL, N'39059', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (141, N'The Academy Of Christian Education (39595)', 9, NULL, NULL, NULL, NULL, NULL, N'39595', N'no', N'44392', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (143, N'Aldrin Elementary (34360)', 9, NULL, NULL, NULL, NULL, NULL, N'34360', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (145, N'Edlin School (39940)', 9, NULL, NULL, NULL, NULL, NULL, N'39940', N'no', N'42070', N'no', N'45106', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (149, N'Armstrong Ele Sch (29186)', 9, NULL, NULL, NULL, NULL, NULL, N'29186', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (150, N'Bailey''s Ele Sch for Arts/Sci (17700)', 9, NULL, NULL, NULL, NULL, NULL, N'17700', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (151, N'Beech Tree Ele Sch (34226)', 9, NULL, NULL, NULL, NULL, NULL, N'34226', N'no', N'34417', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (152, N'Belvedere Ele Sch (23724)', 9, NULL, NULL, NULL, NULL, NULL, N'23724', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (153, N'Canterbury Woods Ele Sch (19995)', 12, NULL, NULL, NULL, NULL, NULL, N'19995', N'no', N'21091', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (154, N'Congressional School of VA (40743)', 9, NULL, NULL, NULL, NULL, NULL, N'40743', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (156, N'Kent Gardens Ele Sch (23353)', 9, NULL, NULL, NULL, NULL, NULL, N'23353', N'no', N'45180', N'no', N'45181', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (157, N'Lutheran Church of the Redeemer (40973)', 9, NULL, NULL, NULL, NULL, NULL, N'40973', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (158, N'Mantua Elementary (30646)', 12, NULL, NULL, NULL, NULL, NULL, N'30646', N'no', N'42492', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (159, N'Montessori School of Northern VA (35609)', 9, NULL, NULL, NULL, NULL, NULL, N'35609', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (160, N'Pinnacle Academy (40749)', 12, NULL, NULL, NULL, NULL, NULL, N'40749', N'no', N'40750', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (161, N'Robert Frost Middle (23506)', 12, NULL, NULL, NULL, NULL, NULL, N'23506', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (162, N'Sleepy Hollow Elementary (32248)', 9, NULL, NULL, NULL, NULL, NULL, N'32248', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (163, N'Thomas Jefferson High Sci/Tech (5808)', 11, NULL, NULL, NULL, NULL, NULL, N'5808', N'no', N'8311', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (164, N'Thoreau Middle (40813)', 9, NULL, NULL, NULL, NULL, NULL, N'40813', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (165, N'Wakefield Forest Elementary (11847)', 12, NULL, NULL, NULL, NULL, NULL, N'11847', N'no', N'11848', N'no', N'18472', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (166, N'Westminster School (38940)', 9, NULL, NULL, NULL, NULL, NULL, N'38940', N'no', N'38941', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (168, N'Haycock Elementary (6610)', 9, NULL, NULL, NULL, NULL, NULL, N'6610', N'no', N'6611', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (169, N'Herndon Reston Homeschoolers (31275)', 9, NULL, NULL, NULL, NULL, NULL, N'31275', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (170, N'Deo Gratias Homeschoolers (41329)', 9, NULL, NULL, NULL, NULL, NULL, N'41329', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (172, N'Reston Montessori School (41462)', 9, NULL, NULL, NULL, NULL, NULL, N'41462', N'no', N'41463', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (173, N'Mason Crest Elementary (41477)', 9, NULL, NULL, NULL, NULL, NULL, N'41477', N'no', N'43387', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (174, N'Langley H S (41493)', 9, NULL, NULL, NULL, NULL, NULL, N'41493', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (175, N'Spring Hill Elementary (30642)', 9, NULL, NULL, NULL, NULL, NULL, N'30642', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (176, N'Al Fatih Academy (41514)', 9, NULL, NULL, NULL, NULL, NULL, N'41514', N'no', N'42069', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (178, N'McLean Robotics Institute (41575)', 9, NULL, NULL, NULL, NULL, NULL, N'41575', N'no', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (179, N'Terraset Elementary (41574)', 9, NULL, NULL, NULL, NULL, NULL, N'41574', N'no', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (180, N'Westbriar Elementary School (42145)', 9, NULL, NULL, NULL, NULL, NULL, N'42145', N'no', N'46720', N'no', N'45651', N'no', N'45652', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (181, N'Pinecrest School (42218)', 9, NULL, NULL, NULL, NULL, NULL, N'42218', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (182, N'Potomac School (42228)', 9, NULL, NULL, NULL, NULL, NULL, N'42228', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (183, N'Emmanuel Lutheran Church (42435)', 9, NULL, NULL, NULL, NULL, NULL, N'42435', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (184, N'Longfellow Middle (15934)', 9, NULL, NULL, NULL, NULL, NULL, N'15934', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (185, N'Chesterbrook Elementary (30092)', 9, NULL, NULL, NULL, NULL, NULL, N'30092', N'no', N'44131', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (186, N'Woodson High School (34445)', 12, NULL, NULL, NULL, NULL, NULL, N'34445', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (187, N'Dodgeball Theatre (20571)', 9, NULL, NULL, NULL, NULL, NULL, N'20571', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (188, N'Stenwood Elementary School (43994)', 9, NULL, NULL, NULL, NULL, NULL, N'43994', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (189, N'Freedom Hill Elementary School (44102)', 9, NULL, NULL, NULL, NULL, NULL, N'44102', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (191, N'Glasgow Middle School (27251)', 9, NULL, NULL, NULL, NULL, NULL, N'27251', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (193, N'Flint Hill School (22826)', 9, NULL, NULL, NULL, NULL, NULL, N'22826', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (194, N'Marshall HS (7858)', 9, NULL, NULL, NULL, NULL, NULL, N'7858', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (195, N'Glen Forest ES (45292)', 9, NULL, NULL, NULL, NULL, NULL, N'45292', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (196, N'St. Joseph School (45645)', 9, NULL, NULL, NULL, NULL, NULL, N'45645', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (197, N'Carson Middle School (45733)', 9, NULL, NULL, NULL, NULL, NULL, N'45733', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (198, N'Ideaventions Academy for Math/Sci (45772)', 9, NULL, NULL, NULL, NULL, NULL, N'45772', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (200, N'McLean H S (23563)', 9, NULL, NULL, NULL, NULL, NULL, N'23563', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (201, N'Basis Independent McLean (46461)', 9, NULL, NULL, NULL, NULL, NULL, N'46461', N'no', N'46462', N'no', N'46463', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (202, N'Dolley Madison Library (46496)', 9, NULL, NULL, NULL, NULL, NULL, N'46496', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (203, N'Cunningham Park Elementary School (26971)', 9, NULL, NULL, NULL, NULL, NULL, N'26971', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (204, N'Parklawn Elementary School (46540)', 9, NULL, NULL, NULL, NULL, NULL, N'46540', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (205, N'Compass Homeschool (46568)', 9, NULL, NULL, NULL, NULL, NULL, N'46568', N'no', N'46569', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (206, N'Shrevewood Elementary School (18952)', 9, NULL, NULL, NULL, NULL, NULL, N'18952', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (207, N'Vienna Girls (48428(V))', 9, NULL, NULL, NULL, NULL, NULL, N'48428(V)', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (208, N'Immanuel Christian School (35779)', 11, NULL, NULL, NULL, NULL, NULL, N'35779', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (209, N'Nysmith School for the Gifted (30470)', 12, NULL, NULL, NULL, NULL, NULL, N'30470', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (210, N'Oak Hill Elementary School (31486)', 12, NULL, NULL, NULL, NULL, NULL, N'31486', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (211, N'Clermont Elementary School (10502)', 11, NULL, NULL, NULL, NULL, NULL, N'10502', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (212, N'Sangster Elementary School A (29184)', 12, NULL, NULL, NULL, NULL, NULL, N'29184', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (213, N'McNair Upper Elementary School (49397)', 12, NULL, NULL, NULL, NULL, NULL, N'49397', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (214, N'Floris Elementary School (35369)', 12, NULL, NULL, NULL, NULL, NULL, N'35369', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (215, N'Louise Archer Elementary B (44280)', 9, NULL, NULL, NULL, NULL, NULL, N'44280', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (216, N'Sangster Elementary School B (32279)', 12, NULL, NULL, NULL, NULL, NULL, N'32279', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (217, N'Sangster Elementary School C (45151)', 12, NULL, NULL, NULL, NULL, NULL, NULL, N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (218, N'Sangster Elementary School D (45152)', 12, NULL, NULL, NULL, NULL, NULL, NULL, N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (220, N'Sangster Elementary School E (45153)', 12, NULL, NULL, NULL, NULL, NULL, N'45153', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (221, N'Sangster Elementary School F (45154)', 12, NULL, NULL, NULL, NULL, NULL, N'45154', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (222, N'Lake Braddock Secondary School (5812)', 12, NULL, NULL, NULL, NULL, NULL, N'5812', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Schools] ([ID], [Name], [RegionNumber], [Address], [City], [State], [PostalCode], [Phone], [Membership#1], [Membership#1seen], [Membership#2], [Membership#2seen], [Membership#3], [Membership#3seen], [Membership#4], [Membership#4seen], [CoordNew?], [CoordFirstName], [CoordLastName], [CoordAddress], [CoordCity], [CoordState], [CoordPostalCode], [CoordPhone], [CoordAltPhone], [CoordMobilePhone], [CoordFaxNumber], [CoordEmailName], [Share?], [Notes]) VALUES (223, N'Powell Elementary School (39730)', 12, NULL, NULL, NULL, NULL, NULL, N'39730', N'no', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Schools] OFF
GO
SET IDENTITY_INSERT [dbo].[Volunteers] ON 

INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (6, 35, N'Megan', N'Hoskins', N'(703) 323-0567', N'(703) 323-0567', N'(425) 260-8355', N'hoskinsspam@gmail.com', NULL, N'1. Frost Middle School, Division 2, Problem 2 - The Not-So-Haunted House, Coach Paula Vonasek

2. Canterbury Woods Elementary School, Division 1, Problem 5 -- Seeing is Believing, Coach Amy Williams (This team has not confirmed their choice of problem, so that may change)', CAST(N'2013-12-02T19:45:15.403' AS DateTime), CAST(N'2013-12-02T19:48:37.213' AS DateTime), CAST(N'2013-12-14T09:05:16.173' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9) AppleWebKit/537.71 (KHTML, like Gecko) Version/7.0 Safari/537.71')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (9, 28, N'Tarek', N'Rizk', N'(703) 623-1948', N'(703) 623-1948', N'(703) 623-1948', N'tarekrizk@gmail.com', NULL, N'School: Wakefield Forest ES 
Division: I 
Problem name: It''s How We Rule 
Coach''s name: Vicki Hurlebaus
AND
School: Wakefield Forest ES 
2nd Grade Team
Coach''s name: Katrena Henderson', CAST(N'2013-12-05T12:39:40.633' AS DateTime), CAST(N'2013-12-05T12:42:52.147' AS DateTime), CAST(N'2013-12-07T06:22:29.277' AS DateTime), N'Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (10, 31, N'Katie', N'Stribling', N'(703) 978-3418', N'(703) 978-3418', N'(703) 346-8663', N'ktstribling@gmail.com', NULL, N'Wakefield Forest Elementary School
Coach Jennifer Cassata
Problem #1 Drivers Test', CAST(N'2013-12-05T17:46:01.523' AS DateTime), CAST(N'2013-12-05T17:48:02.913' AS DateTime), CAST(N'2013-12-10T07:51:25.470' AS DateTime), N'Mozilla/5.0 (Windows NT 6.0; rv:25.0) Gecko/20100101 Firefox/25.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (11, 33, N'Sonie', N'Kalidindi', N'(703) 476-9288', N'(703) 476-9288', N'(703) 476-9288', N'soniekalidindi@hotmail.com', NULL, NULL, CAST(N'2013-12-06T14:40:35.423' AS DateTime), CAST(N'2014-01-02T16:39:45.373' AS DateTime), CAST(N'2013-12-14T07:37:28.893' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9) AppleWebKit/537.71 (KHTML, like Gecko) Version/7.0 Safari/537.71')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (13, 131, N'Celia', N'Dallas', N'(703) 526-8554', N'(703) 450-9173', N'(703) 608-2592', N'cdallas@cambridgeassociates.com', N'I volunteered at last year''s regional event by watching the door and letting people in/out in between team performances.

I would prefer to volunteer after my daughter competes, rather than before.', N'Forestville Elementary, Division 1, That''s How We Rule, LeLoup and Murphy', CAST(N'2013-12-06T18:35:43.607' AS DateTime), CAST(N'2013-12-06T18:43:10.800' AS DateTime), CAST(N'2014-01-09T10:02:13.100' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (14, 52, N'Stephanie', N'Hando', N'(703) 200-5092', N'(703) 200-5092', N'(703) 200-5092', N'steffimidge@yahoo.com', NULL, N'Fox Mill Elementary Team coached by Deepa Krappadath & K. Malaika Walton. I believe they are doing The Not-So Haunted House.', CAST(N'2013-12-09T05:39:31.963' AS DateTime), CAST(N'2013-12-09T06:21:00.337' AS DateTime), CAST(N'2013-12-21T08:33:59.023' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (15, 37, N'Dawn', N'Baron', N'(703) 434-9306', N'(703) 434-9306', N'(703) 434-9306', N'dawn@dawnbaron.com', NULL, N'The Creative Seven', CAST(N'2013-12-09T18:07:57.340' AS DateTime), CAST(N'2013-12-09T18:08:50.523' AS DateTime), CAST(N'2013-12-16T10:40:47.093' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/536.30.1 (KHTML, like Gecko) Version/6.0.5 Safari/536.30.1')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (16, 96, N'Manmohan', N'Konda', N'(805) 704-0507', N'(713) 429-5909', N'(805) 704-0507', N'konda_mohan@yahoo.com', NULL, N'Team: Nuclear Nachos
School: Navy Elementary
Problem: Not-So-Haunted House 
Coach Name: Anitha Pillai and Clarice Ransom
', CAST(N'2013-12-10T11:52:40.283' AS DateTime), CAST(N'2013-12-10T12:00:11.110' AS DateTime), CAST(N'2014-01-03T11:51:08.413' AS DateTime), N'Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; MALNJS; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (18, 109, N'Kelly', N'Keller', N'(703) 531-2319', N'(703) 390-9433', N'(703) 772-6834', N'Kkeller263@gmail.com', NULL, N'Hunters Woods Elementary, not sure of division, problem 1, Coach Hitesh Parekh', CAST(N'2013-12-10T18:34:34.147' AS DateTime), CAST(N'2013-12-10T18:44:17.797' AS DateTime), CAST(N'2014-01-06T08:14:41.280' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (19, 54, N'Nishi', N'Mendis-Krishnaiyer', N'(202) 472-1962', N'(703) 242-3005', N'(703) 599-5257', N'nmendis@worldbank.org', N'I volunteered in 2013 for the spontaneous section of the tournament (my son was in a 3rd grade group). Mine was a verbal spontaneous section. I am fine with doing the same or something new.', N'Westbriar, K-2, Primary: The World’s First Art Festival, Mahaveer Nabiraj', CAST(N'2013-12-11T07:02:30.843' AS DateTime), CAST(N'2013-12-11T07:05:58.987' AS DateTime), CAST(N'2013-12-22T14:14:35.367' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (23, 41, N'Jen', N'Lee', N'(609) 969-7380', N'(240) 880-7795', N'(609) 969-7380', N'xjleex@yahoo.com', N'No previous experience with Odyssey tournaments.', N'My child is on a team and don''t want to miss it. 
Colvin Run Elementary School.  Primary (K-2nd grade).  World''s First Arts Festival. Elaine Wang.', CAST(N'2013-12-11T20:07:49.687' AS DateTime), CAST(N'2013-12-11T20:10:49.157' AS DateTime), CAST(N'2013-12-17T10:22:26.467' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (24, 194, N'Donna', N'Silverman', N'(703) 623-3710', N'(703) 435-7371', N'(703) 623-3710', N'donnadey@aol.com', N'This is my first experience with the Odyssey of the Mind program', N'Kent Gardens Elementary School, 4th grade, Chris Hinton,', CAST(N'2013-12-12T05:09:15.000' AS DateTime), CAST(N'2013-12-12T05:13:34.997' AS DateTime), CAST(N'2014-01-14T16:52:09.493' AS DateTime), N'Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.0 AOL/9.7 AOLBuild/4343.2039.US Safari/537.1')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (25, 56, N'Sripriya', N'Subramanian', N'(203) 770-3113', N'(203) 770-3113', N'(203) 770-3113', N'Sreepriyas@gmail.com', NULL, N'Westbriar, K-2, Primary: The World’s First Art Festival, Mahaveer Nabiraj
Westbriar, 3-5, Problem 2: The Not-so Haunted House, Katie Clark Wedding
Westbriar, 3-5, Problem 1: The Driver’s Test, Behnaz Hossein', CAST(N'2013-12-12T06:15:17.020' AS DateTime), CAST(N'2013-12-12T06:17:19.610' AS DateTime), CAST(N'2013-12-22T19:39:19.627' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (27, 198, N'Monica ', N'Duggal', N'(703) 801-4784', N'(703) 204-0681', N'(703) 801-4784', N'duggal_monica@yahoo.com', NULL, N'I would like to be able to see my daughter''s team.  She attends Kent Gardens ES and she is in 4th grade and her name is Maya Chatterjee.  I do not know the division, problem name or coach though as that is still being decided on.  The coordinator there is Mr. Chris Hinton.', CAST(N'2013-12-12T07:57:47.153' AS DateTime), CAST(N'2013-12-12T08:00:26.013' AS DateTime), CAST(N'2014-01-14T17:24:23.390' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (28, 129, N'Van', N'Pham', N'(703) 444-9350', N'(703) 444-9350', N'(703) 861-3845', N'thuvann@aol.com', NULL, N'Forestville Elementary School Team A, Division 2, Problem 5, LeLoup.', CAST(N'2013-12-12T19:50:38.247' AS DateTime), CAST(N'2013-12-12T19:52:42.427' AS DateTime), CAST(N'2014-01-09T08:53:10.827' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9) AppleWebKit/537.71 (KHTML, like Gecko) Version/7.0 Safari/537.71')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (29, 38, N'Yiyan', N'Gao', N'(709) 517-4426', N'(703) 517-4426', N'(703) 517-4426', N'yiyangao@yahoo.com', NULL, N'Academy of Christian Education', CAST(N'2013-12-13T10:12:14.557' AS DateTime), CAST(N'2013-12-13T10:15:07.240' AS DateTime), CAST(N'2013-12-16T12:06:33.043' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (30, 44, N'Min', N'Feng', N'(571) 830-6715', N'(703) 965-1239', N'(703) 965-1239', N'mmfeng@hotmail.com', NULL, N'Mantua ES - It''s How we Rule, Division 1
team #42492', CAST(N'2013-12-13T14:57:55.187' AS DateTime), CAST(N'2013-12-13T15:00:55.623' AS DateTime), CAST(N'2013-12-17T19:02:50.253' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (32, 46, N'Anne', N'Emig', N'(703) 292-7241', N'(703) 759-0302', N'(571) 383-6593', N'wanemig@gmail.com', N'This is my first time being a judge for Odyssey.', N'School: Westbriar Elementary School (#42145)
1) Westbriar 3-5, Problem 1: The Driver''s Test, Coach: Behnaz Hossein
2) Westbriar 3-5, Problem 2: The Not-So-Haunted House, Coach: Katie Clark-Wedding
3) Westbriar K-2, Primary, The World''s First Art Festival, Coach: Mahaveer Nabiraj

(The first of these is my daughter''s team and therefore my highest priority)', CAST(N'2013-12-14T20:28:58.520' AS DateTime), CAST(N'2013-12-14T20:45:33.777' AS DateTime), CAST(N'2013-12-18T08:44:07.120' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/534.59.10 (KHTML, like Gecko) Version/5.1.9 Safari/534.59.10')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (34, 36, N'Purvi', N'Bhatt', N'(703) 648-0009', N'(703) 648-0009', N'(703) 648-0009', N'bhattpurvi@hotmail.com', N'In the event, I have difficulty to attend for some reason, my team has other volunteers, who can fill me in.', N'Would like to see my daughter''s performance. So would like to perform my volunteer duties immediately before or after.', CAST(N'2013-12-15T18:40:48.750' AS DateTime), CAST(N'2013-12-15T18:45:22.477' AS DateTime), CAST(N'2013-12-15T19:04:14.427' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (35, 80, N'Frank', N'Shi', N'(703) 648-3782', N'(310) 437-3618', N'(310) 437-3518', N'frankshi_99@yahoo.com', N'First time volunteering for this event', N'
I can be present the whole day. (NOTE - email said would like to see son compete on team #80)', CAST(N'2013-12-15T18:40:59.373' AS DateTime), CAST(N'2013-12-15T18:44:33.243' AS DateTime), CAST(N'2013-12-31T12:23:18.107' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (36, 42, N'Elina', N'Fragiskopoulou', N'(703) 839-5455', N'(703) 839-5455', N'(703) 839-5455', N'efragi@yahoo.com', NULL, NULL, CAST(N'2013-12-16T03:16:59.280' AS DateTime), CAST(N'2013-12-16T03:20:49.703' AS DateTime), CAST(N'2013-12-17T13:39:05.993' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (38, 61, N'yining', N'shen', N'(703) 981-8368', N'(703) 981-8368', N'(703) 981-8368', N's.yining@gmail.com', N'No previous experience.', N'The team has AidAn Shen Zhang.', CAST(N'2013-12-16T06:27:15.253' AS DateTime), CAST(N'2013-12-16T06:28:25.190' AS DateTime), CAST(N'2013-12-26T13:11:44.310' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (40, 94, N'Dana', N'Lu', N'(703) 678-3012', N'(703) 678-3012', N'(703) 678-3012', N'huxd123@gmail.com', NULL, N'I would like to see Canterbury Woods Elementary school. Coach''s name: KESSLER, LESLEYANN. The team project:" Not so haunted house." 

I will be available after 8am until noon. 
', CAST(N'2013-12-16T07:27:14.120' AS DateTime), CAST(N'2013-12-16T10:14:56.673' AS DateTime), CAST(N'2014-01-03T07:46:23.007' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (41, 49, N'Kiran', N'Bakshi', N'(703) 989-4108', N'(703) 729-0554', N'(703) 989-4108', N'kiran.bakshi@gmail.com', NULL, NULL, CAST(N'2013-12-16T11:13:26.263' AS DateTime), CAST(N'2013-12-16T11:14:12.860' AS DateTime), CAST(N'2013-12-20T08:09:53.913' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (42, NULL, N'Daly', N'Chin', N'(703) 298-3531', N'(703) 689-4920', N'(703) 298-3531', N'daly@dalychin.com', N'Please put me on the same problems as Georgina Chin.

I would like to volunteer as a score keeper.', NULL, CAST(N'2013-12-16T16:13:29.293' AS DateTime), CAST(N'2013-12-16T16:16:07.187' AS DateTime), NULL, N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (43, 43, N'Marisol ', N'Hernandez', N'(703) 459-0377', N'(571) 375-2279', N'(703) 459-0377', N'marisol.d.hernandez@gmail.com', N'I volunteer last year on Oakton High school', N'School: Lake Anne Elementary School 
Division 1
Problem: It''s how we rule
Coach: Aurian H. Lotter', CAST(N'2013-12-17T05:28:19.147' AS DateTime), CAST(N'2013-12-17T05:33:34.153' AS DateTime), CAST(N'2013-12-17T15:23:38.323' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (44, 60, N'Kathleen', N'Castro', N'(240) 276-6834', N'(703) 391-8948', N'(703) 501-8948', N'kmc12300@hotmail.com', N'This is my first experience with OM and first time volunteering for the tournament.  ', N'The team I would like to see during the day is:  
Rachel Carson Middle School
Problem 1  Drivers Test
Coaches:  Nikki and Knand Kothari', CAST(N'2013-12-17T06:29:31.420' AS DateTime), CAST(N'2013-12-17T06:36:39.920' AS DateTime), CAST(N'2013-12-25T17:06:38.237' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (47, 53, N'Muhibo', N'Yusuf', N'(415) 672-6006', N'(703) 333-5879', N'(415) 672-6006', N'muhuboyusuf@gmail.com', N'I was a tournament Judge last year and I really enjoyed. It was great experience...just didn''t like missing the kids performance.', N'I would like to watch the following teams:
School: Belevedere Elementary School (in Falls Church, VA)
Problem: Driving Test
Coach: George Lamb

School: Belvedere Elementary School
Problem: Primary
Coach: Laura Doughty', CAST(N'2013-12-17T08:22:55.450' AS DateTime), CAST(N'2013-12-17T08:28:52.827' AS DateTime), CAST(N'2013-12-22T12:27:07.930' AS DateTime), N'Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:25.0) Gecko/20100101 Firefox/25.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (48, 170, N'Terri', N'Lamb', N'(703) 538-6065', N'(703) 538-6065', N'(703) 395-2790', N'terri.lamb@cox.net', NULL, N'Belvedere Elementary, Division I, Driver''s Test, Coach George Lamb', CAST(N'2013-12-17T08:35:39.677' AS DateTime), CAST(N'2013-12-17T09:10:08.923' AS DateTime), CAST(N'2014-01-13T19:38:32.320' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (49, 64, N'Jean', N'King', N'(703) 605-1744', N'(703) 750-1880', N'(703) 505-6734', N'Jeanking88@hotmail.com', NULL, N'Belvedere 4th grade 
coach George Lamb 
driver''s test', CAST(N'2013-12-17T10:46:25.280' AS DateTime), CAST(N'2013-12-17T10:50:38.387' AS DateTime), CAST(N'2013-12-29T06:44:02.407' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_2 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A501 Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (50, 66, N'Staci', N'Alexander', N'202-306-0874', N'202-306-0874', N'202-306-0874', N'staci.j.alexander@cox.net', NULL, N'NOVA North, Belvedere Elementary School, Problem 5, Coach Shannon Inkpen', CAST(N'2013-12-17T13:23:44.590' AS DateTime), CAST(N'2013-12-17T13:29:12.500' AS DateTime), CAST(N'2013-12-29T10:36:04.307' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (56, 70, N'Thuc', N'Hoang', N'(202) 586-7050', N'(571) 228-4452', N'(571) 228-4452', N'liz_hoang@yahoo.com', N'I have no prior experience with OM.', N'- School: Belvedere E.S.
- Division: Grades 3-5 (?)
- Coach: George Lamb
- Problem: Driver''s Test #1', CAST(N'2013-12-18T15:04:05.133' AS DateTime), CAST(N'2013-12-18T15:08:40.997' AS DateTime), CAST(N'2013-12-29T16:34:59.303' AS DateTime), N'Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (57, 47, N'Miranda', N'Thomas', N'(202) 205-8138', N'(703) 627-8420', N'(703) 627-8420', N'Randa777@yahoo.com', NULL, N'Belvedere, problem 5, coach- Shannon Inkpen', CAST(N'2013-12-18T15:17:37.407' AS DateTime), CAST(N'2013-12-18T15:19:55.833' AS DateTime), CAST(N'2013-12-19T05:49:51.360' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (61, 197, N'Jana', N'King Allen', N'(703) 405-9533', N'(703) 405-9533', N'(703) 405-9533', N'jkingallengeneva@gmail.com', NULL, N'I would like to see the team from Kent Gardens with Keira Allen (my daughter).
School:  Kent Gardens Elementary School;  Division: 1;  Problem Name:  Seeing is Believing; 
Coach:  Chris Hinton', CAST(N'2013-12-20T12:51:21.657' AS DateTime), CAST(N'2013-12-20T14:08:44.707' AS DateTime), CAST(N'2014-01-14T17:16:46.793' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (63, 240, N'Meera', N'Rahul', N'(703) 980-8668', N'(703) 980-8668', N'(703) 980-8668', N'meerahul@gmail.com', NULL, N'I have 2 kids participating this year. 
Child 1 - Navy Elementary School, Primary Division, The World’s First Art Festival, Coached by Mr Arunabho Das

Child 2 - Navy Elementary School, Division 1 , Problem 2: The Not-So-Haunted House, Coached by Mrs. Sheela Ramnathan.

I''d like to see both their performance and am available to volunteer between 9 am and 1.30 pm. ', CAST(N'2013-12-22T07:44:30.140' AS DateTime), CAST(N'2013-12-22T08:11:49.247' AS DateTime), CAST(N'2014-01-15T11:21:05.440' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (65, 59, N'Tricia', N'Ratliff', N'(703) 593-6444', N'(703) 593-6444', N'(703) 593-6444', N'triciasart@gmail.com', N'I''ll be volunteering for the first time. It might be useful to know that I''m energized by things like like welcoming people (registration etc.), organizing name-tags, explaining directions/schedules and helping people feel comfortable with activities. ', N'Teams I''d like to see perform: Fox Mill Elementary School Team - Coach is Jennifer Wagner, Problem = "How we Rule"
If possible it would be a bonus if I can see the Oak Hill team because a close family friend''s child is on that team. My son is on the fox mill team so Fox Mill is the primary team I''d like to see. ', CAST(N'2013-12-22T15:42:55.967' AS DateTime), CAST(N'2013-12-23T05:36:11.627' AS DateTime), CAST(N'2013-12-24T12:07:03.747' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/536.25 (KHTML, like Gecko) Version/6.0 Safari/536.25')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (66, 55, N'Rujuta', N'Shrotriya', N'(703) 932-8768', N'(703) 932-8768', N'(703) 932-8768', N'rujutashro@gmail.com', NULL, N'Team: We the Horror

Dhruv Shrotriya

Pesandi Gunasekara

Vihini Gunasekara

Ayush Sundararaman

Vikram Achuthan

Tarun Sivanandan

Dhruv Sundararaman

Coach- Sowmya Sundararaman

MOSBY WOODS ELE SCH 34282 ', CAST(N'2013-12-22T18:46:28.587' AS DateTime), CAST(N'2013-12-22T19:14:48.290' AS DateTime), CAST(N'2013-12-22T20:13:05.180' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (68, 65, N'Ann', N'Malekzadeh', N'(703) 242-0340', N'(703) 242-0340', N'(703) 217-9571', N'annmalekzadeh@gmail.com', NULL, N'Colvin Run ES, primary division, prehistoric art festival problem, coach is Melody Akhavan (son''s name is Koorosh Aryavand).   Thank you!!', CAST(N'2013-12-24T09:51:58.990' AS DateTime), CAST(N'2013-12-26T08:02:18.150' AS DateTime), CAST(N'2013-12-29T09:22:28.430' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.71 (KHTML, like Gecko) Version/6.1 Safari/537.71')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (69, 75, N'Melissa', N'Romano', N'(703) 899-2432', N'(703) 899-2432', N'(703) 899-2432', N'villabacio@yahoo.com', NULL, N'Lake Anne Elementary 2nd Grade (Coach Tyler Maxey) and 4th Grade (Coach Glenn ?)', CAST(N'2013-12-25T15:43:58.417' AS DateTime), CAST(N'2013-12-25T15:46:37.900' AS DateTime), CAST(N'2013-12-31T04:41:18.880' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (70, 211, N'Stacey', N'Brooks', N'(202) 358-1890', N'(703) 437-3453', N'(202) 222-5891', N'comtnclimr@gmail.com', N'I did the check-in/registration table last year. ', N'Kent Gardens Elementary School-Child''s name is Jocelyn Brooks.  Not sure what her team name is, but the Coach is Chris Hinton.  ', CAST(N'2013-12-26T15:43:44.443' AS DateTime), CAST(N'2013-12-26T15:48:32.667' AS DateTime), CAST(N'2014-01-14T18:45:32.507' AS DateTime), N'Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (71, NULL, N'Anne-Marie', N'O''Brien', N'(508) 505-1922', N'(703) 978-0010', N'(508) 505-1922', N'amobnp@gmail.com', NULL, N'Canterbury Woods Elementary, 5th grade, Car, Diane Lindquist', CAST(N'2013-12-27T08:14:57.443' AS DateTime), CAST(N'2013-12-27T08:17:34.357' AS DateTime), NULL, N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (73, 88, N'Pooja', N'Sawhney', N'(703) 622-0049', N'(703) 622-0049', N'(703) 622-0049', N'sawhneypooja@hotmail.com', N'Please assigned a time before my child''s tournament starts so I can join her after my volunteering time is over. ', N'School - Forest Edge Ele Sch  
Problem - The World’s First Art Festival
Coach - Barbara Peters', CAST(N'2013-12-27T10:49:16.717' AS DateTime), CAST(N'2013-12-27T11:13:35.183' AS DateTime), CAST(N'2014-01-02T11:34:01.023' AS DateTime), N'Mozilla/5.0 (Windows; Windows NT 6.1; rv:10.0)Gecko/20100101 Firefox/9.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (74, 71, N'sravan-kumar', N'sunku', N'7037131043', N'7037131043', N'7037131043', N'srujana.sunku@gmail.com', NULL, NULL, CAST(N'2013-12-27T12:44:36.987' AS DateTime), CAST(N'2013-12-27T12:44:51.557' AS DateTime), CAST(N'2013-12-30T13:29:14.787' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (75, 139, N'Phil', N'Meade', N'(703) 845-6929', N'(703) 868-4453', N'(703) 868-4453', N'pwmcem@fastmail.fm', N'I have coached three times and judged three times in the past.  This is my first time in the role of tournament volunteer.', N'Langston Hughes Middle School, Division 2, Problem 3: It''s How We Rule, Christine Meade', CAST(N'2013-12-28T08:21:02.577' AS DateTime), CAST(N'2013-12-28T08:25:25.683' AS DateTime), CAST(N'2014-01-10T16:16:58.593' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (76, 72, N'Sherry', N'Yang', N'(703) 456-3779', N'(703) 647-6395', N'(703) 647-6395', N'sxbyang@yahoo.com', NULL, N'Academy of Christian Education', CAST(N'2013-12-28T17:18:15.043' AS DateTime), CAST(N'2013-12-28T17:20:43.810' AS DateTime), CAST(N'2013-12-30T09:46:57.990' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (77, 68, N'Lata', N'Viswanathan', N'(703) 628-6798', N'(703) 318-7968', N'(703) 628-6798', N'latamv@gmail.com', N'Have been a volunteer for last 3 years', N'Forest Edge, Division II, Driver''s Test, Vish Viswanathan', CAST(N'2013-12-29T13:16:33.643' AS DateTime), CAST(N'2013-12-29T13:18:30.240' AS DateTime), CAST(N'2013-12-29T13:31:28.883' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (80, 74, N'Alice', N'Gray', N'(571) 426-7837', N'(703) 742-6762', N'(571) 426-7837', N'anygray@hotmail.com', N'No prior experience.', N'Forest Edge Elementary School, Division I, Problem 4: Stackable Structure, coach: Justin Gray', CAST(N'2013-12-30T08:27:17.623' AS DateTime), CAST(N'2013-12-31T07:10:06.040' AS DateTime), CAST(N'2013-12-30T13:03:23.683' AS DateTime), N'Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.69 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (81, 73, N'judith ', N'scyrkels', N'(703) 476-5105', N'(703) 476-5105', N'(713) 206-8417', N'mollyscyrkels@yahoo.com', NULL, N'Emmanuel Lutheran Church', CAST(N'2013-12-30T10:23:49.500' AS DateTime), CAST(N'2013-12-30T10:25:03.790' AS DateTime), CAST(N'2013-12-30T10:31:01.777' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/537.73.11 (KHTML, like Gecko) Version/6.1.1 Safari/537.73.11')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (82, 106, N'Brian ', N'Silvestri', N'(703) 433-9813', N'(703) 627-2371', N'(703) 627-2371', N'brian.silvestri@gmail.com', N'I volunteered at the registration table last year.', N'Forestville Elementary Balsa Wood Team B Membership # 32449', CAST(N'2013-12-30T16:14:22.213' AS DateTime), CAST(N'2013-12-30T16:16:05.910' AS DateTime), CAST(N'2014-01-05T15:42:42.193' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (83, 82, N'Peter', N'Brown', N'(703) 582-6716', N'(703) 582-6716', N'(703) 582-6716', N'Pdbrown@deloitte.com', NULL, N'Anusha Iyer team', CAST(N'2013-12-30T17:47:03.733' AS DateTime), CAST(N'2013-12-30T17:48:46.930' AS DateTime), CAST(N'2013-12-31T16:48:08.583' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 6_1_3 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10B329 Safari/8536.25')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (84, 76, N'Kishore', N'Puvvada', N'(703) 679-5352', N'(703) 793-3580', N'(703) 798-6905', N'pkishore@hotmail.com', N'I cannot lift or move heavy weights due to a health condition.  I served as a stage area judge 2 years ago, helping out a team at my neighborhood school.  I am open for any type of volunteer duty.', N'Fox Mill Elementary School, 
Primary division, 
Art Festival, 
Coach : Tiffany Hallman', CAST(N'2013-12-31T07:19:19.113' AS DateTime), CAST(N'2013-12-31T07:23:00.457' AS DateTime), CAST(N'2013-12-31T08:45:25.923' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (85, 77, N'Anil', N'Shah', N'703-420-8987', N'703-620-0805', N'703-420-8987', N'anilshah@yahoo.com', N'I have worked as volunteer judge in 2013 competetion.', N'Hunters Woods Elementary School, Division 1, Problem: "Not So Haunted House", Coach - Neelesh Katiyar', CAST(N'2013-12-31T10:23:58.477' AS DateTime), CAST(N'2013-12-31T10:31:52.470' AS DateTime), CAST(N'2013-12-31T10:51:41.503' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; InfoPath.2; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (86, 78, N'Kathryn', N'Thurston', N'(706) 860-5667', N'(703) 860-5667', N'(703) 403-7972', N'thurstons4@mac.com', N'This will be my first OM experience, so please place me with someone WITH experience.', N'My son is on a team with 3rd-5th graders from Navy Elementary, The Not-So-Haunted House problem, lead by Gang Yang. Thanks!', CAST(N'2013-12-31T11:14:42.450' AS DateTime), CAST(N'2013-12-31T11:17:19.860' AS DateTime), CAST(N'2013-12-31T12:36:29.980' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (89, 84, N'Rachelle', N'Stefanik', N'(703) 635-3698', N'(703) 635-3698', N'(703) 635-3698', N'Rachelle@kstef.com', NULL, NULL, CAST(N'2013-12-31T19:04:26.683' AS DateTime), CAST(N'2013-12-31T19:06:14.670' AS DateTime), CAST(N'2013-12-31T19:17:40.293' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Mobile/11B554a')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (90, 85, N'Tracy', N'McHone', N'(703) 242-6222', N'(703) 242-6222', N'(703) 242-6222', N'crkitten@hotmail.com', NULL, N'Wolftrap ES, Division I, ???Problem, Coach Anna Pane
Wolftrap ES, Division II, It''s How We Rule, Coach Rachelle Stefanik', CAST(N'2013-12-31T19:25:12.890' AS DateTime), CAST(N'2013-12-31T19:29:49.223' AS DateTime), CAST(N'2013-12-31T19:51:31.780' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (91, 86, N'Anne Marie', N'O''Brien', N'(508) 505-1922', N'(508) 505-1922', N'(508) 505-1922', N'Tigon1996@netzero.com', NULL, N'Canterbury woods elem school division 1, driver''s test, Lindquist team

', CAST(N'2013-12-31T20:54:03.457' AS DateTime), CAST(N'2013-12-31T20:57:32.117' AS DateTime), CAST(N'2013-12-31T21:02:49.277' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (94, 91, N'Amana', N'Rashid', N'(312) 285-3098', N'(703) 435-4117', N'(312) 285-3098', N'a_rashid24@hotmail.com', NULL, N'Herndon-Reston Homeschoolers, Division 2, Its how we rule, Jean Mctigue', CAST(N'2014-01-02T11:13:09.963' AS DateTime), CAST(N'2014-01-02T11:19:21.227' AS DateTime), CAST(N'2014-01-02T12:24:24.527' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (95, 89, N'Abdul-Malik', N'Ahmad', N'7035018528', N'7035018528', N'7035018528', N'malik199@gmail.com', NULL, N'Herndon-Reston Homeschoolers, Division 2, Seeing is Believing, Sadaf Hussain', CAST(N'2014-01-02T11:21:11.070' AS DateTime), CAST(N'2014-01-02T11:24:12.117' AS DateTime), CAST(N'2014-01-02T11:50:51.147' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (96, 90, N'Salma', N'Naseer', N'(703) 347-5162', N'(703) 347-5162', N'(703) 347-5162', N'salma.momof3@gmail.com', NULL, N'Herndon-Reston Homeschoolers, Division 2, Driving Test, Rahima Ullah
Herndon-Reston Homeschoolers, Division 2, Its How we Rule, Jean Mctigue

Prefer afternoon shift', CAST(N'2014-01-02T11:30:31.413' AS DateTime), CAST(N'2014-01-02T11:34:42.753' AS DateTime), CAST(N'2014-01-02T12:08:06.197' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (97, 93, N'Peggy', N'Marshall', N'(703) 204-2104', N'(703) 204-2104', N'(703) 302-0412', N'ecclesmt@cox.net', NULL, NULL, CAST(N'2014-01-02T19:41:07.677' AS DateTime), CAST(N'2014-01-02T19:42:24.213' AS DateTime), CAST(N'2014-01-02T19:49:06.677' AS DateTime), N'Mozilla/5.0 (X11; Linux x86_64; rv:18.0) Gecko/20100101 Firefox/18.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (98, 113, N'Rita', N'Monner', N'(571) 236-0780', N'(703) 281-1858', N'(571) 236-0780', N'rmonner@aol.com', N'I have do not have any previous experience volunteering at Odyssey tournaments.  ', N'Marshall Road Elementary School,  Division 1, The Not So Haunted House, Coach Kristi Zimmerman', CAST(N'2014-01-03T06:40:53.650' AS DateTime), CAST(N'2014-01-03T07:26:52.383' AS DateTime), CAST(N'2014-01-06T16:21:40.387' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_1) AppleWebKit/537.73.11 (KHTML, like Gecko) Version/7.0.1 Safari/537.73.11')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (99, 105, N'Leslie', N'Hutchinson', N'(571) 426-5953', N'(703) 433-0023', N'(571) 426-5953', N'LeslieHutch@aol.com', NULL, N'I have two children performing for Forestville Elementary School under membership code # 25649 

The teams are:

Division 1, Stackable Structure - Coach Mark Spoto
Division 2, Seeing is Believing - Coach Jeff LeLoup', CAST(N'2014-01-03T07:53:30.603' AS DateTime), CAST(N'2014-01-03T07:59:17.467' AS DateTime), CAST(N'2014-01-05T13:56:16.633' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; AOL 9.7; AOLBuild 4343.19; Windows NT 6.1; WOW64; Trident/6.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (101, 97, N'PATTY', N'WHELPLEY', N'(703) 938-5244', N'(703) 938-5244', N'(703) 629-2156', N'pattywhelpley@cox.net', NULL, N'My son is on the Capital Baptist Homeschool team, Division 1, Problem 1: Driver''s test, coach: Betsy (Elizabeth) Argauer', CAST(N'2014-01-03T12:57:38.847' AS DateTime), CAST(N'2014-01-03T13:06:11.607' AS DateTime), CAST(N'2014-01-03T13:18:25.053' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (103, 107, N'Suma', N'Kolli', N'(703) 793-0128', N'(703) 793-0128', N'(703) 955-0841', N'Shkolli@yahoo.com', NULL, N'School: forest edge elementary school', CAST(N'2014-01-03T13:29:10.980' AS DateTime), CAST(N'2014-01-04T07:09:46.977' AS DateTime), CAST(N'2014-01-05T16:08:54.670' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A465 Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (108, 136, N'Naureen ', N'Huda', N'(703) 740-7690', N'(703) 738-4879', N'(703) 740-7690', N'Naureenhuda@hotmail.com', NULL, N'The Schools is Al-Fatih Academy and the coaches name is Tania Ullah.', CAST(N'2014-01-04T06:07:41.240' AS DateTime), CAST(N'2014-01-04T06:10:29.103' AS DateTime), CAST(N'2014-01-10T08:41:17.253' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 6_1_4 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10B350 Safari/8536.25')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (109, 98, N'Mitchell ', N'Thompson', N'(703) 329-7854', N'(703) 329-7854', N'(571) 228-1927', N'mitchellandsuzy@juno.com', N'Afternoon is better for me', N'Capital Baptist Homeschool Division II, Not-So-Haunted House, Thompson', CAST(N'2014-01-04T07:59:38.767' AS DateTime), CAST(N'2014-01-04T08:01:20.903' AS DateTime), CAST(N'2014-01-04T08:12:13.430' AS DateTime), N'Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.4 Safari/533.20.27')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (111, 101, N'Meenakshi', N'Rajagopalan', N'(703) 855-0345', N'(703) 542-6059', N'(703) 855-0345', N'meenakshir_va@yahoo.com', NULL, N'I would prefer anytime between 8am - 1pm.', CAST(N'2014-01-04T08:48:14.580' AS DateTime), CAST(N'2014-01-04T08:51:46.560' AS DateTime), CAST(N'2014-01-04T11:47:11.617' AS DateTime), N'Mozilla/5.0 (Windows NT 6.3; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (113, 102, N'Sujata', N'Nakhre', N'(703) 929-7976', N'(703) 929-7976', N'(703) 929-7976', N'SUJATA_N@YAHOO.COM', NULL, N'Rachel Carson, Division 2, Not so haunted house, Olivia Peterkin', CAST(N'2014-01-04T11:13:45.847' AS DateTime), CAST(N'2014-01-04T11:17:41.557' AS DateTime), CAST(N'2014-01-05T08:18:22.667' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (115, 140, N'Ken', N'Liu', N'(703) 761-5008', N'(703) 938-2860', N'(703) 863-7591', N'ki-liu@juno.com', N'This is my first time volunteering at an Odyssey event.', N'I would like to watch my son Jason Liu''s team perform.  His team is the Division 2 team from Louise Archer ES led by Mrs. Pragya Nawlakhe. ', CAST(N'2014-01-04T20:57:40.753' AS DateTime), CAST(N'2014-01-04T21:12:12.637' AS DateTime), CAST(N'2014-01-10T19:08:11.323' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (116, 104, N'Peng', N'Warweg', N'7032206499', N'7032206499', N'7032206499', N'penglbj@yahoo.com', NULL, N'Wakefield Forest Elementary School
Problem: The World''s First Art Festival
Grade: K-2
Coach: Angelos Keromytis', CAST(N'2014-01-04T21:34:05.210' AS DateTime), CAST(N'2014-01-04T21:42:02.527' AS DateTime), CAST(N'2014-01-05T18:15:30.810' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.9; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (117, 125, N'Robin', N'Ergener', N'(703) 272-8873', N'(703) 272-8873', N'(310) 213-6855', N'niborerg@aol.com', NULL, N'I would like to see the performance Division 2, Kilmer MS, Not so Haunted House, Coach: Anna Pane', CAST(N'2014-01-05T07:23:38.740' AS DateTime), CAST(N'2014-01-05T07:30:15.807' AS DateTime), CAST(N'2014-01-09T05:58:26.163' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (118, 108, N'Sarah ', N'Gabriel', N'(703) 868-6949', N'(703) 543-6800', N'(703) 868-6949', N'Sarah@gabrielcorp.com', NULL, N'Hunters Woods, Division 1, "Seeing is Believing", Coach Fred Briden. ', CAST(N'2014-01-05T07:47:51.890' AS DateTime), CAST(N'2014-01-05T19:01:23.100' AS DateTime), CAST(N'2014-01-06T02:36:41.923' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (119, 218, N'Julie', N'Livingston', N'(703) 425-4418', N'(703) 425-4418', N'(703) 425-4418', N'julie.livingston@bates.com', NULL, N'Frost Middle School - Problem 3 - Division 3
Mantua ES - Problem 3 - Division 1', CAST(N'2014-01-05T11:01:02.827' AS DateTime), CAST(N'2014-01-05T11:03:43.280' AS DateTime), CAST(N'2014-01-15T03:56:18.680' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; MAAU; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (122, 149, N'Michelle ', N'Pham', N'(571) 358-3264', N'(703) 946-9623', N'(703) 946-9623', N'michelleminhthu@yahoo.com', NULL, N'Canterbury Woods Elementary School, Team Kokkinis, we are division 2, and we are doing problem number 3.', CAST(N'2014-01-05T13:38:43.187' AS DateTime), CAST(N'2014-01-05T13:46:36.197' AS DateTime), CAST(N'2014-01-12T11:32:51.993' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (123, 158, N'Ingrid', N'Badia', N'(703) 627-6916', N'(703) 433-2078', N'(703) 627-6916', N'ingrid.badia@yahoo.com', NULL, N'Lake Anne Elementary School, Division 1, Problem 4: The Stackable Structure, Renee Boyce', CAST(N'2014-01-05T16:43:20.050' AS DateTime), CAST(N'2014-01-05T16:46:13.450' AS DateTime), CAST(N'2014-01-13T08:45:49.317' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (125, 111, N'Ranjeev', N'Pamnani', N'(703) 314-9009', N'(703) 314-9009', N'(703) 314-9009', N'ranjeev_pamnani@yahoo.com', N'Last year, I was assigned the following job as a volunteer. I can do it again.

Team Check-in.  Collect team registration tickets from coaches and exchange them for team registration packets.', N'I would like to see perform the following team:

School: Navy Elementary School
Division: Primary
Problem Name: The world''s 1st Arts Festival
Coach Name: Elizabeth Dougherty', CAST(N'2014-01-05T17:21:56.647' AS DateTime), CAST(N'2014-01-05T17:26:52.350' AS DateTime), CAST(N'2014-01-06T10:38:41.297' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (126, 151, N'Rebecca', N'Musy', N'(406) 579-3610', N'(703) 759-3082', N'(406) 579-3610', N'rmusy@vt.edu', NULL, N'Colvin Run ES
Division 4? (4th grade) 
How We Rule
Amy Leone and Sadia Zubairi', CAST(N'2014-01-05T18:02:34.883' AS DateTime), CAST(N'2014-01-05T18:12:38.673' AS DateTime), CAST(N'2014-01-12T16:23:58.577' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (127, 156, N'Nisha', N'Sankar', N'(571) 436-5756', N'(703) 421-2345', N'(571) 436-5756', N'nisha.sankar@gmail.com', N'My two kids are participating under above mentioned coaches and would like to see them perform.
', N'Forestville Elementary School, Coach: Michelle Stjohn
Forestville Elementary School, Coach: Joe Carr', CAST(N'2014-01-05T18:17:06.910' AS DateTime), CAST(N'2014-01-05T18:26:07.300' AS DateTime), CAST(N'2014-01-13T07:44:16.690' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (128, 141, N'Erin ', N'Williams', N'(703) 568-9240', N'(703) 478-6056', N'(703) 568-9240', N'dflavin@griffinowens.com', NULL, N'Lake Anne Elementary, Primary, Worlds First Art Festival, Coach Daniel Flavin', CAST(N'2014-01-05T18:58:00.683' AS DateTime), CAST(N'2014-01-05T19:04:07.717' AS DateTime), CAST(N'2014-01-11T07:01:04.303' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (130, 231, N'Sebnem', N'Kalemli-Ozcan', N'(832) 495-1068', N'(832) 495-1068', N'(832) 495-1068', N'kalemli@econ.umd.edu', N'This is my first time.', N'I need to watch my son''s team, Potomac school, 42228, teacher is Ms Webster.', CAST(N'2014-01-06T05:37:10.547' AS DateTime), CAST(N'2014-01-06T05:39:02.947' AS DateTime), CAST(N'2014-01-15T09:29:47.027' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (133, 145, N'Stephanie', N'Laubner', N'215-932-7252', N'215-932-7252', N'215-932-7252', N'stephlaubner@yahoo.com', NULL, N'Louise Archer Elementary, Team A, youngest Division (grades K-2). Problem: "World''s First Arts Festival."', CAST(N'2014-01-06T07:17:11.987' AS DateTime), CAST(N'2014-01-06T07:21:20.690' AS DateTime), CAST(N'2014-01-11T18:27:55.117' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB7.5; InfoPath.3; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (136, 227, N'Tricia', N'Crane', N'(703) 658-0104', N'(703) 658-0104', N'(202) 251-7324', N'pcrane1@aol.com', NULL, N'Bailey''s Elementary School
Division: Primary
Problem: "World''s First Art Festival"
Coach: Sharon Obias', CAST(N'2014-01-06T08:22:26.397' AS DateTime), CAST(N'2014-01-06T08:42:09.887' AS DateTime), CAST(N'2014-01-15T07:29:17.567' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.0 AOL/9.7 AOLBuild/4343.2039.US Safari/537.1')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (137, 212, N'Christine', N'Erbacher', N'(571) 280-8314', N'(703) 598-8801', N'(703) 598-8801', N'cerbacher@aol.com', N'no previous experience with OOM', N'School:  Bailey''s Elementary School
Division: Primary
Problem: "The World''s First Art Festival"
Coach:  Raphael Laufer     ', CAST(N'2014-01-06T08:47:00.090' AS DateTime), CAST(N'2014-01-06T08:48:34.803' AS DateTime), CAST(N'2014-01-14T19:05:13.893' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (138, 241, N'katya', N'Somanchi', N'(703) 391-2098', N'(571) 431-2353', N'(571) 431-2353', N'katyasomanchi@hotmail.com', NULL, N'Team from Hunters woods ES - Coach Nancy Shah', CAST(N'2014-01-06T09:24:08.323' AS DateTime), CAST(N'2014-01-06T09:26:02.020' AS DateTime), CAST(N'2014-01-15T11:15:00.573' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (139, 159, N'Jiangfeng', N'Ye', N'(571) 276-5394', N'(571) 612-3268', N'(571) 276-5394', N'yejeff@hotmail.com', N'No prior volunteering with Odyssey tournaments. I volunteered as a cub scout den leader these days.', N'FI like to see performance from Forestville Elementary school - coach Leigh Freund''s team', CAST(N'2014-01-06T09:58:27.387' AS DateTime), CAST(N'2014-01-06T10:10:53.920' AS DateTime), CAST(N'2014-01-13T09:11:29.237' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0; )')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (140, 112, N'Eleanor', N'Jones', N'(703) 795-3912', N'(703) 795-3912', N'(703) 795-3912', N'montee@georgetown.edu', N'First time parent and volunteer', N'Wakefield Forest ES (WFES)
Jenny Champagne and Liz Smith (Coaches)
Our long term problem:
The World’s First Art Festival

James Champagne (2)
Isaac Cook (2)
Richard "Chance" Jones (2)
Reese Jones (1)
Shannon Smith (1)
Sarah Champagne (1)', CAST(N'2014-01-06T11:14:44.133' AS DateTime), CAST(N'2014-01-06T11:20:31.497' AS DateTime), CAST(N'2014-01-06T15:51:39.500' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (143, 245, N'Karen', N'Giusti', N'(703) 641-8684', N'(703) 786-6347', N'(703) 786-6347', N'birdykg1@hotmail.com', NULL, N'Camelot Elementary, Division II, Problem 1:  Driver''s Test, Joan Goldfarb 

Thanks for trying to ensure I see my child''s team.  ', CAST(N'2014-01-06T12:02:33.483' AS DateTime), CAST(N'2014-01-06T12:07:09.927' AS DateTime), CAST(N'2014-01-15T12:35:32.250' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (144, 243, N'Erin', N'Lobato', N'(301) 922-2545', N'(301) 922-2545', N'(301) 922-2545', N'erin_lobato@hotmail.com', NULL, N'Forestville Elementary School, Division 2 (6th graders), the problem involving building a vehicle that must drive a course using two propulsion systems.  Coach is Anna Phan', CAST(N'2014-01-06T13:00:36.987' AS DateTime), CAST(N'2014-01-06T13:02:32.493' AS DateTime), CAST(N'2014-01-15T11:34:04.427' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (145, 217, N'Day', N'Leary', N'(571) 331-2474', N'(571) 331-2474', N'(571) 331-2474', N'Dayleary@cox.net', NULL, N'Pine Spring div 1 Webb', CAST(N'2014-01-06T13:51:39.697' AS DateTime), CAST(N'2014-01-06T15:19:43.747' AS DateTime), CAST(N'2014-01-15T03:10:35.547' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (146, 165, N'Maria', N'Pavco', N'(301) 451-8315', N'(703) 787-6952', N'(845) 764-1656', N'maria.pavco@gmail.com', NULL, N'Forest Edge Elementary School, Division I, Not So Haunted House, Coach: Shannon Geary ', CAST(N'2014-01-06T14:24:48.400' AS DateTime), CAST(N'2014-01-06T14:27:50.833' AS DateTime), CAST(N'2014-01-13T14:29:05.217' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_1) AppleWebKit/537.73.11 (KHTML, like Gecko) Version/7.0.1 Safari/537.73.11')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (147, 118, N'Ralph', N'Pacheco', N'(914) 623-2359', N'(914) 623-2359', N'(914) 623-2359', N'jcheckster@gmail.com', NULL, N'Marshall Road Elementary School Division 1 The Not So Haunter House coach Kristi Zimmerman
Luther Jackson Middle School Division 2 It''s How We Rule coach Ashley Pacheco

 ', CAST(N'2014-01-06T15:41:40.637' AS DateTime), CAST(N'2014-01-06T15:47:35.520' AS DateTime), CAST(N'2014-01-07T09:13:16.233' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/537.73.11 (KHTML, like Gecko) Version/6.1.1 Safari/537.73.11')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (149, 114, N'Steve ', N'Smith ', N'(571) 236-5957', N'(571) 236-5957', N'(571) 236-5957', N'smith@ahcinc.org', NULL, NULL, CAST(N'2014-01-06T17:19:29.833' AS DateTime), CAST(N'2014-01-06T17:21:12.407' AS DateTime), CAST(N'2014-01-06T17:38:41.563' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (150, 176, N'Raymond', N'Wallace', N'(703) 928-2345', N'(703) 440-8505', N'(703) 791-9646', N'ray@wallace3.com', N'I have not volunteered before, but was a judge last year.  I would like to be involved but want to see my son''s performance this year.', N'Wakefield Forest Elementary School, 
Division I (3rd grade)
The Not So Haunted House
Karen Wallace & Janet Hensley - coaches', CAST(N'2014-01-06T18:06:26.757' AS DateTime), CAST(N'2014-01-06T18:09:19.313' AS DateTime), CAST(N'2014-01-14T08:39:02.963' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (152, 173, N'Mahalakshmi', N'Venkataraman', N'(703) 430-4827', N'(703) 430-4827', N'(703) 200-5442', N'vmahalakshmi@yahoo.com', NULL, N'Louise Archer - Division 1 team - Coach Name  - Malini Iyer', CAST(N'2014-01-06T18:13:06.740' AS DateTime), CAST(N'2014-01-06T18:17:34.027' AS DateTime), CAST(N'2014-01-14T06:38:44.987' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:24.0) Gecko/20100101 Firefox/24.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (153, 201, N'Sabrina', N'Pabilonia', N'(703) 850-6903', N'(703) 658-5228', N'(703) 850-6903', N'sabrinapab@yahoo.com', N'I was a coach 5 years ago.', N'School:  Bailey''s Elementary School
Division: Division 1
Problem: "Driver''s Test"
Coach:  Monica Perz-Waddington  ', CAST(N'2014-01-06T18:38:19.373' AS DateTime), CAST(N'2014-01-06T18:39:57.737' AS DateTime), CAST(N'2014-01-14T18:09:28.863' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.73.11 (KHTML, like Gecko) Version/6.1.1 Safari/537.73.11')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (154, 137, N'Parag', N'Majethia', N'(703) 724-3192', N'(703) 383-1375', N'(571) 294-2223', N'chivalry13@yahoo.com', NULL, N'Navy Elementary, Haunted House, Clarice - coach''s name
', CAST(N'2014-01-06T20:38:19.690' AS DateTime), CAST(N'2014-01-08T14:24:58.597' AS DateTime), CAST(N'2014-01-10T08:13:48.893' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (155, 223, N'Amanda', N'Jackson', N'(703) 300-4055', N'(703) 848-0862', N'(703) 300-4055', N'jackson.amanda@gmail.com', N'I have scheduling challenges the morning of the tournament, and would prefer to have a shift that starts at 10am or later.', N'School: Potomac School, grade 4/5 team, Coach: Joy Webster, Membership Number:  42228', CAST(N'2014-01-06T20:41:41.657' AS DateTime), CAST(N'2014-01-06T20:45:41.780' AS DateTime), CAST(N'2014-01-15T06:24:09.977' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (157, 127, N'Kristin', N'Prillaman', N'(703) 242-7936', N'(703) 314-0470', N'(703) 314-0470', N'Kristinprillaman@verizon.net', NULL, N'Division 1 Pane/Gowda', CAST(N'2014-01-07T05:27:28.480' AS DateTime), CAST(N'2014-01-07T05:29:46.687' AS DateTime), CAST(N'2014-01-09T06:20:37.520' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (160, 116, N'ken', N'nottingham', N'(703) 624-9310', N'(703) 624-9310', N'(703) 624-9310', N'pinkarla@aol.com', N'n/a', N'Wakefield Forest Elementary School
Division 1
It''s How We Rule
Karla Eggert/Josh Vichness', CAST(N'2014-01-07T06:43:07.293' AS DateTime), CAST(N'2014-01-07T06:47:17.077' AS DateTime), CAST(N'2014-01-07T07:22:28.913' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_5) AppleWebKit/537.71 (KHTML, like Gecko) Version/6.1 Safari/537.71')
GO
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (161, 135, N'Michelle', N'Lucas', N'(703) 503-7952', N'(703) 503-7952', N'(703) 346-0891', N'Mwsmile20@yahoo.com', N'This is my first time at this event.  ', N'Wakefield Forest ES 4th grade team', CAST(N'2014-01-07T06:55:46.950' AS DateTime), CAST(N'2014-01-07T07:00:06.870' AS DateTime), CAST(N'2014-01-09T19:19:01.780' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (162, 119, N'Jill', N'Mitchell', N'(305) 926-3632', N'(703) 425-1468', N'(305) 926-3632', N'jillmunzmitchell@gmail.com', NULL, N'Wakefield Forest ES, Grades K-2, The World''s First Art Festival, Katrena Henderson', CAST(N'2014-01-07T06:55:50.350' AS DateTime), CAST(N'2014-01-07T07:01:41.817' AS DateTime), CAST(N'2014-01-07T12:02:39.510' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (163, 150, N'David', N'Buddendeck', N'(202) 379-0349', N'(703) 533-2336', N'(703) 887-3003', N'kathy_dave@verizon.net', N'I have previously served as an Odyssey judge. ', N'I have 2 children on the same Beech Tree Elementary team and would like to see them perform. ', CAST(N'2014-01-07T07:18:04.997' AS DateTime), CAST(N'2014-01-07T07:19:40.893' AS DateTime), CAST(N'2014-01-12T14:13:42.987' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (164, 120, N'Yan', N'Wei', N'(703) 663-0159', N'(703) 663-0159', N'(703) 663-0159', N'weiwei4834@gmail.com', N'I don''t have any previous experience volunteering at Odyssey tournaments. I''d be happy to help with anything as needed.', N'Forestville Elementary School
Primary: The World’s First Art Festival 
Coaches: Joseph Carr & Mary Merritt

I will not be available to serve as a volunteer before 11:30AM on the day of the tournament.', CAST(N'2014-01-07T07:18:42.673' AS DateTime), CAST(N'2014-01-07T07:30:33.060' AS DateTime), CAST(N'2014-01-07T17:29:58.530' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (165, NULL, N'a', N'a', N'(123) 123-2222', N'(123) 123-1234', N'(123) 123-1234', N'ddd@gmail.com', NULL, NULL, CAST(N'2014-01-07T07:52:36.897' AS DateTime), CAST(N'2014-01-07T07:54:13.027' AS DateTime), NULL, N'Mozilla/5.0 (X11; Linux x86_64; rv:24.0) Gecko/20100101 Firefox/24.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (166, 117, N'bela', N'Sastry', N'(202) 441-1779', N'(202) 441-1779', N'(202) 441-1779', N'belasastry@gmail.com', NULL, N'Colvin Run, Division 1, Not-so-haunted house, Coach Hari Sastry', CAST(N'2014-01-07T08:19:07.440' AS DateTime), CAST(N'2014-01-07T08:20:24.383' AS DateTime), CAST(N'2014-01-07T08:27:27.657' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (167, 250, N'Revathi', N'Rengarajan', N'(703) 981-1468', N'(703) 920-3110', N'(703) 981-1468', N'revathirrajan@hotmail.com', NULL, N'Prefer to volunteer after the performance is over for the below team.

School: Waples Mill Elementary
Coach''s Name: Renu Singh', CAST(N'2014-01-07T09:32:16.577' AS DateTime), CAST(N'2014-01-07T09:36:27.183' AS DateTime), CAST(N'2014-01-15T13:17:18.607' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (168, 155, N'Bahareh', N'Sadoughi', N'(571) 216-0230', N'(703) 560-1715', N'(571) 216-0230', N'Bahareoali@yahoo.com', NULL, N'Beech tree elementary school', CAST(N'2014-01-07T11:07:32.823' AS DateTime), CAST(N'2014-01-07T11:09:26.083' AS DateTime), CAST(N'2014-01-13T05:01:11.220' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (169, 225, N'Kim', N'Anderson', N'7034340875', N'7034340875', N'7034340875', N'kimberlylue@hotmail.com', NULL, NULL, CAST(N'2014-01-07T11:22:17.377' AS DateTime), CAST(N'2014-01-07T11:23:08.937' AS DateTime), CAST(N'2014-01-15T06:50:13.877' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; InfoPath.2; .NET4.0C)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (171, 175, N'Rashi', N'Govil', N'(703) 263-0203', N'(703) 263-0203', N'(703) 855-8343', N'anurashi@yahoo.com', N'Anything is fine', N'Navy elementary -division 1- stackable structure- Kristina Coleman
Rachel Carson Middle school - its how we rule- Srinivas C
', CAST(N'2014-01-07T12:21:46.437' AS DateTime), CAST(N'2014-01-07T18:18:48.620' AS DateTime), CAST(N'2014-01-14T08:35:40.670' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (172, 193, N'wendy', N'connell', N'(703) 786-3249', N'(703) 786-3249', N'(703) 786-3249', N'wendyjconnell@aol.com', NULL, N'Colvin Run Elementary, Division I, Driver''s Test, Coaches Connell and Martell', CAST(N'2014-01-07T12:32:32.143' AS DateTime), CAST(N'2014-01-07T12:35:51.503' AS DateTime), CAST(N'2014-01-14T16:51:50.990' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (173, 260, N'Rahath ', N'Sultana', N'(703) 738-4563', N'(703) 738-4563', N'(703) 470-1931', N'rahathajaz@hotmail.com', N'Since this is my first time volunteering at an Odyssey tournament, I would be able to see if I am interested in doing this again after volunteering once. ', N'TJHSST, Division 3, Problem 1, Asmita Patel.



', CAST(N'2014-01-07T12:44:50.163' AS DateTime), CAST(N'2014-01-07T15:35:57.260' AS DateTime), CAST(N'2014-01-15T19:54:18.430' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (175, NULL, N'k', N'k', N'(349) 579-4325', N'(387) 931-4572', N'(923) 458-4935', N'k', NULL, N'k', CAST(N'2014-01-07T12:52:45.090' AS DateTime), CAST(N'2014-01-07T12:57:50.240' AS DateTime), NULL, N'Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (181, 134, N'Sarah', N'Jones', N'(703) 323-3662', N'(703) 323-3662', N'(703) 731-5829', N'sarahsjones@yahoo.com', NULL, N'Canterbury Woods, Coach Amy Williams', CAST(N'2014-01-07T13:38:54.447' AS DateTime), CAST(N'2014-01-07T13:40:16.833' AS DateTime), CAST(N'2014-01-09T13:52:56.663' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; EIE10;ENUSWOL; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (182, 167, N'Kelly', N'George', N'(703) 378-3020', N'(703) 378-3020', N'(703) 850-9938', N'Kellygeorge@gmail.com', N'Would love to do volunteer check in again, thanks!', N'Daughter is on this team- would like to see this performance!
Oakton High
Division 3
No so haunted house
Coach Keith George ', CAST(N'2014-01-07T13:52:50.983' AS DateTime), CAST(N'2014-01-07T13:58:30.593' AS DateTime), CAST(N'2014-01-13T16:13:44.533' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (184, 171, N'Beth', N'Chung', N'(703) 869-8698', N'(703) 448-3324', N'(703) 869-8698', N'mygreenexpert@gmail.com', NULL, N'Thomas Jefferson HSST, high school, Problem 5: Seeing is Believing, Sangeeta Agarwal', CAST(N'2014-01-07T13:57:38.627' AS DateTime), CAST(N'2014-01-07T14:03:41.357' AS DateTime), CAST(N'2014-01-13T20:30:07.607' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.41 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (185, 192, N'Alexandra', N'Ptasienski', N'(813) 716-0208', N'(813) 716-0208', N'(813) 716-0208', N'Alexjgf@hotmail.com', N'No experience', N'Kent Gardens Elementary School with  Raphaelle Ptasienski
If possible I would prefer the earliest time that would not interfere with me seeing my daughter.', CAST(N'2014-01-07T15:42:16.730' AS DateTime), CAST(N'2014-01-07T15:45:29.163' AS DateTime), CAST(N'2014-01-14T16:41:22.567' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (186, 166, N'Karoline', N'Rettl', N'(793) 772-4177', N'(703) 772-4117', N'(703) 772-4117', N'karoline.rettl@gmx.at', N'I have not volunteered for Odyssey tournaments before. ', N'Would like to see Forestville elementary, Farifax Division, team 32449 Team B perform, coach Wootton.  I am available for 2 hours in the morning or early afternoon between 9 and 2. ', CAST(N'2014-01-07T17:00:16.957' AS DateTime), CAST(N'2014-01-07T17:07:22.947' AS DateTime), CAST(N'2014-01-13T14:48:42.107' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (187, NULL, N'Almira', N'Saifi', N'(518) 423-4221', N'(518) 423-4221', N'(518) 423-4221', N'Asaifi@alfatih.org', N'I have never volunteered before, and I would like to do something like this again', N'Al Fatih Academy
Coach: Tania Ullah', CAST(N'2014-01-07T17:18:30.123' AS DateTime), CAST(N'2014-01-07T17:21:40.730' AS DateTime), NULL, N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (188, 233, N'Marc', N'Gori', N'(703) 324-2421', N'(703) 969-3494', N'(703) 969-3494', N'marcogori001@yahoo.com', NULL, N'Waples Mill Elementary school; Division I; coaches: Deepa Joshi and Laura Gori', CAST(N'2014-01-07T17:38:03.413' AS DateTime), CAST(N'2014-01-07T17:57:40.930' AS DateTime), CAST(N'2014-01-15T09:48:32.580' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A465 Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (189, 236, N'Yasmeen', N'Khan', N'(816) 694-6784', N'(816) 694-6784', N'(816) 694-6784', N'yasmeenk23@gmail.com', NULL, NULL, CAST(N'2014-01-07T17:51:00.383' AS DateTime), CAST(N'2014-01-07T17:51:56.843' AS DateTime), CAST(N'2014-01-15T10:46:48.953' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:25.0) Gecko/20100101 Firefox/25.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (190, 244, N'Juliana', N'Carter', N'(703) 244-1971', N'(703) 244-1971', N'(703) 244-1971', N'julianacarter@gmail.com', NULL, N'Lake Anne Elementary - The Not so Haunted House - Coach Karen Donis and Emma Golden berg', CAST(N'2014-01-07T18:46:44.937' AS DateTime), CAST(N'2014-01-07T18:48:22.907' AS DateTime), CAST(N'2014-01-15T12:04:08.573' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.71 (KHTML, like Gecko) Version/6.1 Safari/537.71')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (191, 215, N'Denesh', N'Malaveetil', N'(703) 235-0750', N'(571) 236-1426', N'(571) 236-1426', N'denesh.malaveetil@gmail.com', NULL, N'I would like to see my niece''s team participate:
School: Mosby Woods Elementary
Division: Division I
Problem: Not so Haunted House
Co-Coaches: Malaveetil and Geissler

', CAST(N'2014-01-07T20:11:37.687' AS DateTime), CAST(N'2014-01-07T20:18:56.220' AS DateTime), CAST(N'2014-01-14T20:41:05.740' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (193, 179, N'Babar', N'Mansoor', N'(832) 483-2252', N'(832) 483-2252', N'(832) 483-2252', N'mansoorhomeva@gmail.com', NULL, N'I would like to see the Pinnacle Academy Primary Team''s Problem: The World’s First Art Festival.  The Coach''s name is  Aysen Ozlem Adiyaman. ', CAST(N'2014-01-08T10:01:39.403' AS DateTime), CAST(N'2014-01-08T10:07:52.023' AS DateTime), CAST(N'2014-01-14T10:36:08.640' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (194, 222, N'Margaret', N'Nguyen', N'703-955-2649', N'703-955-2649', N'703-955-2649', N'margaretnguyen@gmail.com', NULL, N'Pine Spring/Div I/Webb', CAST(N'2014-01-08T10:48:35.840' AS DateTime), CAST(N'2014-01-08T10:49:39.960' AS DateTime), CAST(N'2014-01-15T06:03:47.763' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3; MS-RTC LM 8)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (195, 246, N'Katy', N'Boswell', N'(703) 975-1669', N'(703) 975-1669', N'(703) 975-1669', N'griffith.katy@gmail.com', N'I am a coach and coordinator for our school. I have volunteered in the past and am a former OMer. ', N'Terraset Elementary School Teams- Pet Project, Not So Haunted House, and ', CAST(N'2014-01-08T12:03:28.470' AS DateTime), CAST(N'2014-01-08T12:05:55.460' AS DateTime), CAST(N'2014-01-15T12:18:01.053' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8) AppleWebKit/536.25 (KHTML, like Gecko) Version/6.0 Safari/536.25')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (196, 235, N'Brett', N'Rose', N'602-737-7175', N'602-737-7175', N'602-737-7175', N'cmcbane@yahoo.com', NULL, N'Pine Spring ES/Div I/ Rose', CAST(N'2014-01-08T14:43:02.557' AS DateTime), CAST(N'2014-01-08T14:47:41.450' AS DateTime), CAST(N'2014-01-15T10:43:34.337' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (197, 182, N'Tomeka', N'Williams', N'(703) 655-2157', N'7036552157', N'(703) 655-2157', N'tomeka.williams@gmail.com', NULL, N'Division I
Seeing is Believing
Pinnacle Academy
Ms. T. Gulsen', CAST(N'2014-01-08T16:30:01.893' AS DateTime), CAST(N'2014-01-08T16:31:54.687' AS DateTime), CAST(N'2014-01-14T10:54:01.727' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.72 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (199, 126, N'Alison', N'Stobie', N'(703) 472-4593', N'(703) 472-4593', N'(703) 472-4593', N'marymich@gmail.com', NULL, N'Terraset Elementary, it''s how we rule', CAST(N'2014-01-09T05:50:54.497' AS DateTime), CAST(N'2014-01-09T05:52:06.977' AS DateTime), CAST(N'2014-01-09T06:06:42.093' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (200, NULL, N'Mike', N'Carter', N'(703) 555-1212', N'(703) 555-1212', N'(703) 555-1212', N'mcarter@gmail.com', N'sdfg', N'adf', CAST(N'2014-01-09T09:17:35.210' AS DateTime), CAST(N'2014-01-09T09:18:13.947' AS DateTime), NULL, N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.9; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (202, 183, N'Hulya', N'Bicak', N'(443) 854-0431', N'(443) 854-0431', N'(443) 854-0431', N'hbicak@pinnacleacademyva.com', N'I was a volunteer last year too. I was on the Spontaneous desk.It was a good experience for me. I prefer to do that again.But it is okay if you need other help.This is really amazing organization for the children. Thank you very much!', N'Pinnacle Academy,Primary team,First Art Festival,Ozlem Adiyaman
Pinnacle Academy,Division 2,Seeing is Believing,Shaunda Trimner', CAST(N'2014-01-09T11:31:43.793' AS DateTime), CAST(N'2014-01-09T12:07:11.777' AS DateTime), CAST(N'2014-01-14T11:06:43.690' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (205, NULL, N'L', N'B', N'(702) 345-6789', N'(781) 231-2323', N'(781) 231-2323', N't@t.com', NULL, NULL, CAST(N'2014-01-09T16:39:32.403' AS DateTime), CAST(N'2014-01-09T16:40:10.703' AS DateTime), NULL, N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (206, 264, N'Lorena', N'Chaves', N'(703) 975-3041', N'(703) 854-1243', N'(703) 975-3041', N'lorechaves@yahoo.com', NULL, N'School:  Colvin Run Elementary School
Problem:  Driver´s Test
Coach:  Mr. Lalit Bedi

', CAST(N'2014-01-09T17:05:10.217' AS DateTime), CAST(N'2014-01-09T17:09:04.477' AS DateTime), CAST(N'2014-01-15T16:47:33.797' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.0; WOW64; Trident/5.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (207, NULL, N'Michael', N'Ptasienski', N'(813) 716-0208', N'(813) 716-0208', N'(813) 716-0208', N'Mtptas@hotmail.com', N'Would like to work with my wife Akexandra Ptasienski and volunteer in the morning.', N'Kent Gardens elementary School Raphaelle Ptasienski', CAST(N'2014-01-09T18:42:55.280' AS DateTime), CAST(N'2014-01-09T18:45:53.687' AS DateTime), NULL, N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (208, 219, N'Daniela', N'Raik', N'(571) 212-6485', N'(571) 212-6485', N'(571) 212-6485', N'danielaraik@gmail.com', NULL, N'Mason Crest
1st Grade
Art Festival in Prehistoric Times
Rebecca Adye', CAST(N'2014-01-09T19:30:31.453' AS DateTime), CAST(N'2014-01-09T19:32:14.197' AS DateTime), CAST(N'2014-01-15T04:13:33.327' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (210, 203, N'Mariasol', N'Herrera', N'(202) 623-1765', N'(202) 431-3640', N'(202) 431-3640', N'Mariasolh@verizon.net', NULL, NULL, CAST(N'2014-01-10T07:05:57.977' AS DateTime), CAST(N'2014-01-10T10:22:32.687' AS DateTime), CAST(N'2014-01-14T18:18:51.930' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_3 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B511 Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (211, 254, N'Jigna', N'Shah', N'571-271-8175', N'412-894-9036', N'571-271-8175', N'shah.jigna@yahoo.com', N'I am voluntering for the first time.', N'Team: Pine Spring,Primary 1
Head Coach: Uttara Kant', CAST(N'2014-01-10T09:06:37.140' AS DateTime), CAST(N'2014-01-10T09:11:17.967' AS DateTime), CAST(N'2014-01-15T14:23:50.783' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; MS-RTC LM 8; InfoPath.2)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (212, 164, N'Bethany', N'Bond', N'(571) 228-4361', N'(571) 228-4361', N'(571) 228-4361', N'bjbond7@gmail.com', NULL, N'The Seven Sisters
Spring Hill Elementary 
Gray Mosby and Rob Carter', CAST(N'2014-01-10T11:19:33.960' AS DateTime), CAST(N'2014-01-10T12:21:37.683' AS DateTime), CAST(N'2014-01-13T14:17:04.593' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_1) AppleWebKit/537.73.11 (KHTML, like Gecko) Version/7.0.1 Safari/537.73.11')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (213, 138, N'Patrick', N'Horn', N'703 598 1502', N'703 598 1502', N'703 598 1502', N'phorn30@gmail.com', NULL, N'Team does not yet have a name; coached by Stephanie Berg/Wakefield Forest Elementary.', CAST(N'2014-01-10T11:41:54.490' AS DateTime), CAST(N'2014-01-10T11:43:31.633' AS DateTime), CAST(N'2014-01-10T11:52:11.757' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; MS-RTC LM 8; .NET4.0C; .NET4.0E; DI7SP2)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (214, 172, N'Nagarjuna', N'Chimata', N'2403288465', N'7036526291', N'2403288465', N'nchimata@yahoo.com', NULL, N'1. Navy Elementary, Division 1, Driver''s Test, Rachakonda
2. Rachel Carson Middle, Division 2, It''s How We Rule, Chennamaraja', CAST(N'2014-01-10T15:15:48.230' AS DateTime), CAST(N'2014-01-10T15:21:18.087' AS DateTime), CAST(N'2014-01-14T03:51:18.920' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E; MS-RTC LM 8; InfoPath.2)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (216, 143, N'Ashwin', N'Anjutgi', N'(703) 507-0475', N'(703) 507-0475', N'(703) 507-0475', N'ashvini_anjutgi@yahoo.com', NULL, NULL, CAST(N'2014-01-11T16:59:12.163' AS DateTime), CAST(N'2014-01-11T17:01:30.617' AS DateTime), CAST(N'2014-01-11T17:12:37.870' AS DateTime), N'Mozilla/5.0 (Windows NT 6.2; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (217, 239, N'Akila', N'Narayanan', N'(703) 967-9021', N'(703) 967-9021', N'(703) 967-9021', N'akila.narayanan@gmail.com', N'I have volunteered at the past three Odyssey tournaments (NOVA North), would like to be at the registration desk, if that option is available.', N'Hunters Wood Elementary School, Division II, Problem 3 : It''s How We Rule
Coach : Nancy Shah', CAST(N'2014-01-12T06:28:25.423' AS DateTime), CAST(N'2014-01-12T06:32:15.860' AS DateTime), CAST(N'2014-01-15T11:03:40.280' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (220, NULL, N'David', N'Johnson', N'(703) 568-5383', N'(703) 568-5383', N'(703) 568-5383', N'David.Johnson@drel.us', N'I am signing up as a volunteer as a place holder for our team and to go through the process so I can explain it to prospective volunteers.', N'Terraset Elementary School, Division 9, Seeing is Believing, Coaches David Johnson and Cynthia Headrick.', CAST(N'2014-01-12T08:34:23.477' AS DateTime), CAST(N'2014-01-12T08:41:28.280' AS DateTime), NULL, N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (223, 152, N'Charles', N'Baumstark', N'(571) 242-3579', N'(703) 255-9842', N'(571) 242-3579', N'arend@verizon.net', NULL, N'Marshall Road ES, Div. II, Problem 1: The Driver''s Test, Coach Natalie Baumstark', CAST(N'2014-01-12T16:28:27.750' AS DateTime), CAST(N'2014-01-12T16:30:23.397' AS DateTime), CAST(N'2014-01-12T16:42:02.520' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (224, 169, N'Tina ', N'Zhao', N'(571) 641-6035', N'(703) 396-7578', N'(571) 641-6035', N'tinaok@yahoo.com', N'It is my first volunteering at Odyssey tournaments, yes, I would like to again if I could.', N'This is the team my two sons are in: 
1.Spring Hill Elementary, Div 1, problem 5, coach Amy Montie.
2.Spring Hill Elementary, Primary Division, and Alice Guo.
It would be available at time after 9:00.


', CAST(N'2014-01-12T16:59:43.003' AS DateTime), CAST(N'2014-01-12T17:07:15.233' AS DateTime), CAST(N'2014-01-13T17:50:31.147' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (227, 154, N'Jennifer', N'McCombs', N'703-413-1100', N'703-599-6355', N'703-599-6355', N'jennifer_mccombs@yahoo.com', NULL, N'School: Camelot Elementary School
Division: Primary
Problem: Worlds First Art Festival
Coach:  Jamie Steider', CAST(N'2014-01-12T18:00:26.023' AS DateTime), CAST(N'2014-01-12T18:01:50.547' AS DateTime), CAST(N'2014-01-12T18:31:37.503' AS DateTime), N'Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (229, 162, N'Sridevi', N'Rangavajjula', N'(732) 762-1449', N'(732) 762-1449', N'(732) 762-1449', N'srangavajjula@gmail.com', NULL, N'Would like to help our team to arrange the props for getting ready for their performance. -- Navy Elementary School, Team -3, Division III , Problem 5: Seeing is Believing, Coach - Kevin Shen', CAST(N'2014-01-12T18:54:56.473' AS DateTime), CAST(N'2014-01-12T19:04:24.380' AS DateTime), CAST(N'2014-01-13T09:37:45.570' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:17.0) Gecko/20100101 Firefox/17.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (230, 214, N'stacy', N'bushee', N'(703) 648-7454', N'(703) 888-8060', N'(703) 888-8060', N'scbushee@yahoo.com', NULL, N'Lake Anne Elementary School, Beth Hoyos coach', CAST(N'2014-01-13T06:07:08.767' AS DateTime), CAST(N'2014-01-13T09:18:42.747' AS DateTime), CAST(N'2014-01-14T20:11:42.747' AS DateTime), N'Mozilla/5.0 (Linux; U; Android 4.0.4; en-us; ADR6410LVW 4G Build/IMM76D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (231, 160, N'sonal', N'doran', N'(571) 283-5715', N'(703) 865-7888', N'(571) 283-5715', N'sonaldoran@gmail.com', NULL, N'VES primary', CAST(N'2014-01-13T07:31:02.247' AS DateTime), CAST(N'2014-01-13T07:32:04.203' AS DateTime), CAST(N'2014-01-13T09:31:02.137' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (233, 157, N'Lona', N'Alam', N'(703) 909-7045', N'(703) 909-7045', N'(703) 909-7045', N'raymik02@yahoo.com', NULL, NULL, CAST(N'2014-01-13T07:59:13.520' AS DateTime), CAST(N'2014-01-13T08:00:18.200' AS DateTime), CAST(N'2014-01-13T08:12:28.790' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (234, 255, N'Jeff', N'Bladek', N'(703) 898-0327', N'(703) 464-4844', N'(703) 898-0327', N'jeffbladek@msn.com', NULL, N'Sunrise Valley Elementary School, coached by Jessica Gill.', CAST(N'2014-01-13T11:19:56.310' AS DateTime), CAST(N'2014-01-13T11:21:34.313' AS DateTime), CAST(N'2014-01-15T15:46:55.347' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (235, 163, N'Lora', N'Eskandary', N'(202) 646-2717', N'(703) 916-7747', N'(703) 946-2913', N'leskandary@hotmail.com', NULL, N'School - Mason Crest
Problem name - Driving Test
Coach - Lu Huynh
I do not have any limitations on time of day other
then during their performance', CAST(N'2014-01-13T11:54:24.167' AS DateTime), CAST(N'2014-01-14T20:25:21.840' AS DateTime), CAST(N'2014-01-13T12:37:29.140' AS DateTime), N'Mozilla/5.0 (Linux; U; Android 4.1.2; en-us; DROID RAZR Build/9.8.2O-72_VZW-16) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (236, 180, N'Ursula ', N'Griessel', N'(703) 467-0678', N'(703) 467-0678', N'(571) 426-0752', N'Ursdeon@gmail.com', N'The same job would be fine :)', N'Forest edge elementary
Div 1
Problem no 5
Rebecca Castagna', CAST(N'2014-01-13T16:51:34.770' AS DateTime), CAST(N'2014-01-13T16:58:41.463' AS DateTime), CAST(N'2014-01-14T10:16:42.213' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (237, 271, N'Sreenivas', N'Ratna', N'(571) 241-0094', N'(703) 793-1725', N'(571) 241-0094', N'sratna01@gmail.com', NULL, N'I would like to see the below team perform. 
School:Carson Middle School(RCMS)
Division : 2
Problem 4: Stackable Structure
Coach: Sunil Taori', CAST(N'2014-01-13T17:39:07.183' AS DateTime), CAST(N'2014-01-13T17:44:48.777' AS DateTime), CAST(N'2014-01-15T18:33:24.647' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.160 Safari/537.22')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (238, NULL, N'Chalapathi', N'Kotnana', N'(571) 969-2527', N'(571) 969-2527', N'(571) 969-2527', N'KOTNANA@YAHOO.COM', N'Volunteered as spontaneous problem judge', NULL, CAST(N'2014-01-13T18:05:22.657' AS DateTime), CAST(N'2014-01-13T18:07:00.737' AS DateTime), NULL, N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.72 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (239, 202, N'Anne', N'Warrell', N'(703) 399-0193', N'(703) 399-0193', N'(703) 399-0193', N'awarrell@hotmail.com', N'No experience with Odessey tournaments, but I am outgoing and good organizer.  I am happy to help as needed.', N'School is Kent Gardens Elementary, coaches name is Mr. Chris Hinton, and children''s ages are 6 (1st grade) and 9 (3rd grade).  Apologies that I don''t have the additional information.  I''ll query now and email with an update, as I would enjoy seeing our children in action. ', CAST(N'2014-01-13T18:11:49.460' AS DateTime), CAST(N'2014-01-13T18:17:09.520' AS DateTime), CAST(N'2014-01-14T18:03:14.650' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (242, 206, N'Jinfeng', N'Cai', N'(703) 742-0304', N'(703) 742-0304', N'(703) 624-6707', N'jinfengcai@hotmail.com', N'This is my first time as a Odyssey tournament volunteer.', N'South Lakes High School, Division 9, coach RB Bhandari', CAST(N'2014-01-13T18:17:07.413' AS DateTime), CAST(N'2014-01-13T18:22:00.270' AS DateTime), CAST(N'2014-01-14T18:25:52.570' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (244, 186, N'Erik', N'Stamps', N'(571) 229-6406', N'(571) 229-6406', N'(571) 229-6406', N'erikstmps@gmail.com', N'Preference would be a morning assignment - preferred but not a requirement', N'Springhill Elementary Div 1 Seeing is Believing (Amy Montie)
Springhill Elementary Div 2 Drivers Test (Joe Montie)', CAST(N'2014-01-13T18:39:34.433' AS DateTime), CAST(N'2014-01-13T18:43:51.983' AS DateTime), CAST(N'2014-01-14T12:26:07.333' AS DateTime), N'Mozilla/5.0 (Windows NT 6.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (245, NULL, N'ruby', N'Sharma', N'(571) 274-1491', N'(571) 274-1491', N'(571) 274-1491', N'r_sharma1@yahoo.com', N'No experience.', N'Rachel Carson Middle School team. Division 2, in the Stackable Structure problem. The coach''s name is Sunil Taori. The morning is preffered.', CAST(N'2014-01-13T18:56:57.083' AS DateTime), CAST(N'2014-01-13T19:02:14.320' AS DateTime), NULL, N'Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (247, 207, N'Beth', N'Harshman', N'(703) 624-5021', N'(703) 764-1976', N'(703) 624-5021', N'hokiesx2@hotmail.com', NULL, N'Frost Middle School, Problem #5: Seeing is Believing, Jane Lichter is the coach of my son''s OM Team.', CAST(N'2014-01-14T06:51:16.627' AS DateTime), CAST(N'2014-01-14T07:00:42.117' AS DateTime), CAST(N'2014-01-14T18:43:21.570' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.72 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (248, 210, N'Samantha', N'Speier', N'(240) 477-1833', N'(202) 413-4092', N'(202) 413-4092', N'sam.speier@gmail.com', NULL, N'Division 1: Seeing is Believing', CAST(N'2014-01-14T07:59:29.567' AS DateTime), CAST(N'2014-01-14T08:00:49.240' AS DateTime), CAST(N'2014-01-14T18:53:08.993' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:17.0) Gecko/20100101 Firefox/17.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (250, 178, N'Paul', N'Kaplan', N'(703) 620-3007', N'(703) 620-3007', N'(703) 620-3007', N'aekaplan@verizon.net', N'This is my first time volunteering. Please send me or the team''s Coach any additional information that you think may be helpful.', N'Terraset Elementary, Region 9, Division 1, Seeing is Believing ', CAST(N'2014-01-14T09:46:57.677' AS DateTime), CAST(N'2014-01-14T09:54:23.977' AS DateTime), CAST(N'2014-01-14T10:02:46.317' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (251, 181, N'Gunita', N'Makkar', N'(703) 321-6942', N'(703) 321-6942', N'(703) 321-6942', N'gunita_makkar@yahoo.com', N'No prior experience with Odyssey of Mind.', N'Team- Demigods of the Future; School- Haycock Elementary;  Division- I; Coach- Alicia Hoadley; Problem- Haunted House', CAST(N'2014-01-14T10:03:01.950' AS DateTime), CAST(N'2014-01-14T10:06:15.257' AS DateTime), CAST(N'2014-01-14T10:43:22.087' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (252, NULL, N'Jyothi', N'Naroor', N'(703) 713-0081', N'(703) 713-0081', N'(571) 432-9443', N'jyothiviva@hotmail.com', N'This is my first time volunteering but willing to do any part.', N'School - RCMS
Problem - seeing is believing
Coach - Girish
Division- 
Team', CAST(N'2014-01-14T10:49:38.187' AS DateTime), CAST(N'2014-01-14T10:54:21.447' AS DateTime), NULL, N'Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (253, 258, N'Sarah', N'Tweed', N'(703) 860-1061', N'(703) 860-1061', N'(571) 426-2797', N'Olehsarah@verizon.net', N'Have no experience whatsoever with Odyssey of the Mind, so would request a position that requires very little knowledge of the program itself. Thank you!', N'Kent Gardens ES. Do not know division. Problem is creating way to propel vehicle.  Coach is Chris Hinton.
Kent Gardens ES.  Do not know division or problem.  Children in grades 1-2.  Coach is Ms. Fickes.', CAST(N'2014-01-14T11:02:41.460' AS DateTime), CAST(N'2014-01-14T12:21:08.257' AS DateTime), CAST(N'2014-01-15T15:37:50.933' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 6_1_3 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10B329 Safari/8536.25')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (254, 234, N'Craig', N'Meiser', N'(703) 933-6647', N'(703) 560-5354', N'(703) 850-0611', N'cmmeiser@cox.net', N'I was a judge at the 2013 ODM tournament at Oakton High School.', N'Two teams:
  Mason Crest Elementary School, Division I, Problem #3 - It''s How we Rule, & the coach is Sandra Miracle
  
  Mason Crest Elementary School, Division I, Problem #1 - Driver''s Team Test, & the coach is Lu Hunyh', CAST(N'2014-01-14T11:23:04.533' AS DateTime), CAST(N'2014-01-14T11:55:28.273' AS DateTime), CAST(N'2014-01-15T10:09:20.720' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (255, 185, N'Diana', N'Reing', N'(703) 477-2157', N'(703) 848-0676', N'(703) 477-2157', N'gracescience@hotmail.com', NULL, N'Langley School, Middle School Division, The Stackable Structure, Bill Musgrove', CAST(N'2014-01-14T11:44:17.853' AS DateTime), CAST(N'2014-01-14T11:47:21.567' AS DateTime), CAST(N'2014-01-14T12:11:46.790' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (258, 220, N'Kelly', N'Aylward', N'(202) 624-8195', N'(202) 285-6023', N'(202) 285-6023', N'kaylward@wcs.org', N'First Time Volunteer', N'Bailey''s Division I
Coach: John Siodlarz
Problem:  Not So Haunted Hoiuse', CAST(N'2014-01-14T11:46:02.987' AS DateTime), CAST(N'2014-01-14T11:48:54.640' AS DateTime), CAST(N'2014-01-15T04:29:23.887' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.72 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (259, 187, N'Joe', N'Musgrove', N'(703) 477-2116', N'(703) 848-0676', N'(703) 477-2116', N'mcleanhsvb@gmail.com', NULL, N'Langley School, Middle School Division, Seeing is Believing, Coach Ryan McKinney', CAST(N'2014-01-14T11:51:04.263' AS DateTime), CAST(N'2014-01-14T11:52:42.797' AS DateTime), CAST(N'2014-01-14T12:23:20.237' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (260, 188, N'Lisa', N'Mayr', N'(703) 477-2157', N'(703) 848-0676', N'(703) 477-2157', N'willbdunn@hotmail.com', NULL, N'Langley School, Division 2, Not-So-Haunted House, Bill Musgrove coach', CAST(N'2014-01-14T12:23:59.970' AS DateTime), CAST(N'2014-01-14T12:25:47.567' AS DateTime), CAST(N'2014-01-14T12:31:56.490' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (261, 190, N'Carol', N'Marren', N'(703) 533-2642', N'(703) 837-8852', N'(703) 623-8589', N'cmmarren@fcps.edu', N'I would like to work around the time that Longfellow''s team performs, but not during that time.  Before or after would be great.', N'Longfellow Middle School
Division 2
Problem 5
Andrew Scudder, coach', CAST(N'2014-01-14T12:38:09.487' AS DateTime), CAST(N'2014-01-14T13:42:22.163' AS DateTime), CAST(N'2014-01-14T14:45:37.790' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (262, 230, N'Kimberly ', N'Tischler', N'(703) 855-4828', N'(703) 758-7825', N'(703) 855-4828', N'kimberly@tischler.net', NULL, N'Waples Mill Elementary
1st grade
Kalpana ( first name ) is coach
Not So a Haunted House
I would like to work AFTER my son''s performance has ended so that I can attend to him and tge team before.', CAST(N'2014-01-14T13:52:59.230' AS DateTime), CAST(N'2014-01-14T14:00:32.630' AS DateTime), CAST(N'2014-01-15T09:15:27.700' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_2 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A501 Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (263, 213, N'Anu', N'Girish', N'(703) 989-3147', N'(703) 989-3147', N'(703) 989-3147', N'girishfamily@gmail.com', N'I was an odyssey coach. Would like to volunteer early AM for setup.  ', N'Rachel Carson Middle School 
Problem 5 -> Seeing is Believing
Coach -> Girish', CAST(N'2014-01-14T15:28:53.260' AS DateTime), CAST(N'2014-01-14T15:31:00.297' AS DateTime), CAST(N'2014-01-14T19:05:28.820' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.72 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (264, 196, N'John ', N'Kraden', N'(703) 242-4503', N'(703) 242-4503', N'(703) 242-4503', N'lkraden@verizon.net', NULL, N'Team Astell
Oakton Elementary School
Division 1
The Not So Haunted House
Wendy Astell, Christine Wisnewski', CAST(N'2014-01-14T16:34:04.723' AS DateTime), CAST(N'2014-01-14T16:37:49.043' AS DateTime), CAST(N'2014-01-14T17:14:48.167' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (265, 195, N'Shalini', N'Rajesh', N'(703) 938-2664', N'(703) 938-2664', N'(703) 938-2664', N'vijaykumar.shalini@gmail.com', NULL, N'Team Reid
Oakton Elementary School
Division 1
Seeing is Believing
Lyn Reid, Paul Reid', CAST(N'2014-01-14T16:39:18.840' AS DateTime), CAST(N'2014-01-14T16:41:02.240' AS DateTime), CAST(N'2014-01-14T16:55:50.787' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (266, 199, N'Debbie', N'Saylor', N'(703) 938-7924', N'(703) 938-7924', N'(571) 236-0010', N'debbiesaylor@hotmail.com', NULL, N'Team Woldow
Oakton Elementary School
Division 1
The Not So Haunted House
Kristy Woldow', CAST(N'2014-01-14T16:41:49.510' AS DateTime), CAST(N'2014-01-14T16:43:23.613' AS DateTime), CAST(N'2014-01-14T17:32:38.273' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (267, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2014-01-14T17:44:42.033' AS DateTime), NULL, NULL, N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (268, 205, N'Arlene', N'Tredeau', N'(703) 842-3324', N'(703) 842-3324', N'(571) 480-2046', N'atredeau@yahoo.com', N'No previous experience helping with Odyssey. ', N'Mason Crest Division I, The Not So Haunted House, coach Patrick DeMent', CAST(N'2014-01-14T18:08:11.717' AS DateTime), CAST(N'2014-01-14T18:12:42.153' AS DateTime), CAST(N'2014-01-14T18:28:28.303' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (269, 226, N'Elisheva', N'Barkay', N'(703) 623-3535', N'(703) 289-3030', N'(703) 623-3535', N'ebarkay@gmail.com', NULL, N'Luther Jackson MS. Region #9.
Problem 2: The Not-So-Haunted House
Coach: Suzanne Webb', CAST(N'2014-01-14T18:39:33.460' AS DateTime), CAST(N'2014-01-14T18:43:43.177' AS DateTime), CAST(N'2014-01-15T06:52:55.950' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (270, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2014-01-14T18:59:09.307' AS DateTime), NULL, NULL, N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (271, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2014-01-14T20:47:10.153' AS DateTime), NULL, NULL, N'Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (272, 277, N'Sohini', N'Jindal', N'(202) 680-3363', N'(202) 680-3363', N'(202) 680-3363', N'Sohinigupta333@gmail.com', NULL, N'Colvin run ', CAST(N'2014-01-14T21:26:45.720' AS DateTime), CAST(N'2014-01-14T21:28:03.067' AS DateTime), CAST(N'2014-01-16T19:50:17.497' AS DateTime), N'Mozilla/5.0 (iPhone; CPU iPhone OS 7_0_2 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A501 Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (273, 249, N'Dana', N'Lebkisher', N'770-655-7093', N'770-655-7093', N'770-655-7093', N'lebkisher@aol.com', NULL, N'Flaming Archers', CAST(N'2014-01-15T04:41:40.207' AS DateTime), CAST(N'2014-01-15T04:44:34.667' AS DateTime), CAST(N'2014-01-15T12:56:05.787' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (274, 221, N'Erin', N'Martin', N'(703) 319-2030', N'(703) 319-2030', N'(703) 319-2030', N'angusmartin@verizon.net', NULL, N'Oakton Elementary School
Division 1
Driver''s Test
Monique Baroudi', CAST(N'2014-01-15T05:40:15.770' AS DateTime), CAST(N'2014-01-15T05:42:02.087' AS DateTime), CAST(N'2014-01-15T05:51:08.623' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (275, NULL, N'Philip', N'Freeman', N'703-648-6447', N'703-437-4004', N'703-432-7164', N'phil_freeman@verizon.net', N'I directed traffic in the parking at the back entrance near the beginning of the day.  That was fine with me.  ', N'I have 2 sons at Armstrong ES participating this year.  I am volunteering to fulfill a requirement for:
Armstrong ES, team number is 29186, coach David Geissler, problem involves Balsa wood
My 3rd grader is in coach Tanya Williams group, problem involves the self-propelled vehicle', CAST(N'2014-01-15T05:43:23.037' AS DateTime), CAST(N'2014-01-15T05:49:21.853' AS DateTime), NULL, N'Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (276, 242, N'Jane', N'Torok', N'(703) 244-7390', N'(703) 237-2791', N'(703) 244-7390', N'ttorok@verizon.net', N'I am helpful! But I''ve never been to an Odyssey competition before.', N'Pine Spring division #1, Coach: Webb', CAST(N'2014-01-15T05:46:39.527' AS DateTime), CAST(N'2014-01-15T05:51:38.530' AS DateTime), CAST(N'2014-01-15T11:15:40.403' AS DateTime), N'Mozilla/5.0 (Linux; U; Android 2.3.4; en-us; DROID3 Build/5.5.1_84_D3G-66_M2-10) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (277, NULL, N'Yitao', N'Lu', N'(571) 230-8097', N'(571) 230-8097', N'(571) 230-8097', N'luyitao33@yahoo.com', NULL, N'The team I''d like to see perform: Newton-Lee elementary school,  division:14, coach: Wei Sun', CAST(N'2014-01-15T06:39:10.380' AS DateTime), CAST(N'2014-01-15T07:07:52.860' AS DateTime), NULL, N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.72 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (278, 252, N'Loi', N'Nguyen', N'(703) 622-0920', N'(704) 622-0920', N'(703) 622-0920', N'loi.nguyen1@verizon.net', NULL, N'Armstrong ES
primary division
problem: World''s First Art Festival
coach-Rebecca Castagna

prefer to volunteer during the morning or afternoon that team is performing so as not to have to stay entire day.', CAST(N'2014-01-15T06:52:12.033' AS DateTime), CAST(N'2014-01-15T08:27:04.630' AS DateTime), CAST(N'2014-01-15T14:41:40.173' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; Trident/7.0; BOIE9;ENUS; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (279, 237, N'Susan', N'Graham', N'703-560-0318', N'703-560-0318', N'703-508-4161', N'susanbgraham06@aol.com', N'First time volunteer.', N'Pine Spring/Div I/Webb/ Seeing is Believing', CAST(N'2014-01-15T07:52:25.850' AS DateTime), CAST(N'2014-01-15T07:55:40.857' AS DateTime), CAST(N'2014-01-15T10:56:18.423' AS DateTime), N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; MathPlayer 2.10d; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (280, NULL, N'Monica ', N'Buckhorn', N'(703) 333-2970', N'(703) 333-2970', N'(202) 329-5893', N'monica.buckhorn@verizon.net', N'I''ve been a monitor for spontaneous in the past. ', N'Mason Crest ES - Primary Team - Coach Adye (#219)
Mason Crest ES - Not So Haunted House - Coach DeMent
Mason Crest ES - Driver''s Test - Coach Huynh', CAST(N'2014-01-15T08:05:10.390' AS DateTime), CAST(N'2014-01-15T08:08:49.530' AS DateTime), NULL, N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/534.59.10 (KHTML, like Gecko) Version/5.1.9 Safari/534.59.10')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (281, 228, N'raina', N'hoang', N'(703) 283-5464', N'(703) 283-5464', N'(703) 283-5464', N'rnhoang@gmail.com', N'Please schedule me before my son''s team performs.
thanks', N'Pinecrest School
Division 2
It''s How We rule
Kim Berestecki


', CAST(N'2014-01-15T09:19:40.350' AS DateTime), CAST(N'2014-01-15T09:23:42.580' AS DateTime), CAST(N'2014-01-15T09:30:52.877' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_1) AppleWebKit/537.73.11 (KHTML, like Gecko) Version/7.0.1 Safari/537.73.11')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (282, 232, N'Michael', N'Sayers', N'(703) 981-0338', N'(703) 981-0338', N'(703) 981-0338', N'msayers@mac.com', NULL, N'Haycock, Driver''s test, Kim Pinkston', CAST(N'2014-01-15T09:24:45.123' AS DateTime), CAST(N'2014-01-15T09:26:09.243' AS DateTime), CAST(N'2014-01-15T09:38:18.243' AS DateTime), N'Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (283, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2014-01-15T09:44:24.483' AS DateTime), NULL, NULL, N'Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (284, 257, N'Emily ', N'Kobb', N'(571) 294-8555', N'(571) 294-8555', N'(571) 294-8555', N'ejkobb@fcps.edu', NULL, NULL, CAST(N'2014-01-15T11:50:10.977' AS DateTime), CAST(N'2014-01-15T17:27:39.083' AS DateTime), CAST(N'2014-01-15T14:53:37.707' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (285, 247, N'Karla', N'Haworth', N'(202) 321-2297', N'(202) 321-2297', N'(202) 321-2297', N'Cuffkiddies@gmail.com', N'No experience.
Attended last year''s tournament.
Whatever needs to be done is fine with me.', N'Mason Crest Ele Sch (41477), Primary division, The World''s First Art Festival, coach Cuff', CAST(N'2014-01-15T11:58:49.150' AS DateTime), CAST(N'2014-01-15T12:04:39.320' AS DateTime), CAST(N'2014-01-15T12:22:57.573' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/534.59.10 (KHTML, like Gecko) Version/5.1.9 Safari/534.59.10')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (286, 248, N'Michael', N'Zesk', N'(315) 794-3751', N'(315) 794-3751', N'(315) 794-3751', N'mpzesk@gmail.com', NULL, N'Westminster School, both teams', CAST(N'2014-01-15T12:36:20.840' AS DateTime), CAST(N'2014-01-15T12:37:11.097' AS DateTime), CAST(N'2014-01-15T13:03:12.617' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (287, NULL, N'HYRE', N'BYSAL', N'(301) 817-4088', N'(301) 830-1858', N'(301) 830-1858', N'hayrib@gmail.com', N'I volunteered for the concession stand before, but am open to anything.
I also volunteered as a judge before.', N'Luther Jackson Middle School
Coach: Neslihan Esen
Team Name: Esen ', CAST(N'2014-01-15T12:54:23.120' AS DateTime), CAST(N'2014-01-15T13:12:18.233' AS DateTime), NULL, N'Mozilla/5.0 (Windows NT 5.1; rv:25.0) Gecko/20100101 Firefox/25.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (288, 251, N'Michael', N'Zesk', N'(315) 794-3751', N'(315) 794-3751', N'(315) 794-3751', N'mzesk@westminsterschool.com', NULL, NULL, CAST(N'2014-01-15T13:09:00.510' AS DateTime), CAST(N'2014-01-15T13:09:35.987' AS DateTime), CAST(N'2014-01-15T13:22:48.700' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (289, 253, N'Sylvia', N'Lindholm', N'(202) 905-0487', N'(202) 905-0487', N'(202) 905-0487', N'acglennmail@gmail.com', NULL, NULL, CAST(N'2014-01-15T13:30:51.287' AS DateTime), CAST(N'2014-01-15T13:31:53.113' AS DateTime), CAST(N'2014-01-15T13:39:21.617' AS DateTime), N'Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (290, 261, N'Jennifer ', N'Taylor', N'(703) 856-2224', N'(703) 675-7661', N'(703) 856-2224', N'jennifer.i.taylor@hptmail.com', NULL, N'Armstrong Elementary- 4th grade team ', CAST(N'2014-01-15T15:48:42.257' AS DateTime), CAST(N'2014-01-15T15:51:05.470' AS DateTime), CAST(N'2014-01-15T16:04:18.810' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; NP06; rv:11.0) like Gecko')
GO
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (291, 259, N'Wedad', N'Elmaghraby', N'(703) 587-8244', N'(703) 761-3959', N'(705) 587-8244', N'welmaghr@rhsmith.umd.edu', NULL, N'Spring Hill Elementary Division 1 Haunted House', CAST(N'2014-01-15T15:50:26.487' AS DateTime), CAST(N'2014-01-15T15:52:05.987' AS DateTime), CAST(N'2014-01-15T17:25:04.293' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_3 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Mobile/11B511')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (292, 262, N'Stayce', N'Head', N'(703) 481-5884', N'(703) 481-5884', N'(703) 481-5884', N'srhead1@aol.com', NULL, N'Armstrong Elementary- third grade team ', CAST(N'2014-01-15T15:55:41.103' AS DateTime), CAST(N'2014-01-15T15:57:21.040' AS DateTime), CAST(N'2014-01-15T16:15:44.737' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; NP06; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (293, 263, N'Paul', N'Hogan', N'(703) 242-0092', N'(703) 242-0092', N'(703) 242-0092', N'paulalanhogancollege@gmail.com', N'Odyssey alum, now a college student.', N'Luther Jackson Middle School, Division II, problem 5, Esen/Bligh.', CAST(N'2014-01-15T15:58:14.427' AS DateTime), CAST(N'2014-01-15T16:00:06.127' AS DateTime), CAST(N'2014-01-15T16:36:36.123' AS DateTime), N'Mozilla/5.0 (Windows NT 5.1; rv:26.0) Gecko/20100101 Firefox/26.0')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (294, 267, N'Andrew', N'Webb', N'571-730-4896', N'571-730-4896', N'703-966-7589', N'suzannedwebb@hotmail.com', N'rookie', N'Luther Jackson, Div II, Webb, Not So Haunted House (daughter)
Pine Spring, Div I, webb, Seeing is Believing (son)', CAST(N'2014-01-15T16:38:55.577' AS DateTime), CAST(N'2014-01-15T16:55:21.783' AS DateTime), CAST(N'2014-01-15T18:12:20.747' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (295, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2014-01-15T17:02:40.147' AS DateTime), NULL, NULL, N'Mozilla/5.0 (Linux; Android 4.4.2; Nexus 4 Build/KOT49H) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.59 Mobile Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (296, 266, N'Marjan', N'Zare', N'(919) 889-2861', N'(703) 352-2685', N'(919) 889-2861', N'ssaadat2000@yahoo.com', NULL, NULL, CAST(N'2014-01-15T17:14:56.510' AS DateTime), CAST(N'2014-01-15T17:16:00.457' AS DateTime), CAST(N'2014-01-15T17:28:00.050' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (297, 272, N'Tara', N'Murthy', N'(703) 860-5132', N'(703) 860-5132', N'(202) 812-4402', N'moshaugh2@gmail.com', NULL, NULL, CAST(N'2014-01-15T17:17:34.637' AS DateTime), CAST(N'2014-01-15T17:18:22.063' AS DateTime), CAST(N'2014-01-15T18:56:58.433' AS DateTime), N'Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.72 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (298, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2014-01-15T17:47:51.577' AS DateTime), NULL, NULL, N'Mozilla/5.0 (compatible; MSIE 9.10; Windows NT 6.1; WOW64; Trident/6.0; SiteKiosk 7.8 Build 332)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (299, 269, N'Meherun', N'Nahar', N'(703) 371-9381', N'(703) 371-9381', N'(703) 371-9381', N'nahar1216@gmail.com', NULL, NULL, CAST(N'2014-01-15T17:50:47.473' AS DateTime), CAST(N'2014-01-15T17:52:10.640' AS DateTime), CAST(N'2014-01-15T18:01:40.763' AS DateTime), N'Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (300, 270, N'Ved', N'Pinjarkar', N'(703) 859-9200', N'(703) 859-9200', N'(240) 535-5715', N'anujav@hotmail.com', NULL, NULL, CAST(N'2014-01-15T17:52:36.677' AS DateTime), CAST(N'2014-01-15T17:54:09.033' AS DateTime), CAST(N'2014-01-15T18:12:58.097' AS DateTime), N'Mozilla/5.0 (compatible; MSIE 9.10; Windows NT 6.1; WOW64; Trident/6.0; SiteKiosk 7.8 Build 332)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (301, NULL, N'Aryaman', N'Rallapalli', N'(703) 859-9200', N'(703) 859-9200', N'(240) 350-5715', N'rkavitag@yahoo.com', NULL, NULL, CAST(N'2014-01-15T17:55:33.883' AS DateTime), CAST(N'2014-01-15T17:56:29.877' AS DateTime), NULL, N'Mozilla/5.0 (compatible; MSIE 9.10; Windows NT 6.1; WOW64; Trident/6.0; SiteKiosk 7.8 Build 332)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (302, NULL, N'Amrutha', N'Obbineni', N'(240) 350-5715', N'(240) 350-5715', N'(240) 350-5715', N'arobbineni@gmail.com', NULL, NULL, CAST(N'2014-01-15T17:57:08.797' AS DateTime), CAST(N'2014-01-15T17:57:57.363' AS DateTime), NULL, N'Mozilla/5.0 (compatible; MSIE 9.10; Windows NT 6.1; WOW64; Trident/6.0; SiteKiosk 7.8 Build 332)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (303, NULL, N'Samira', N'Tavassoli', N'(240) 350-5715', N'(240) 350-5715', N'(240) 350-5715', N'i_sadr@yahoo.com', NULL, NULL, CAST(N'2014-01-15T17:58:36.880' AS DateTime), CAST(N'2014-01-15T17:59:41.090' AS DateTime), NULL, N'Mozilla/5.0 (compatible; MSIE 9.10; Windows NT 6.1; WOW64; Trident/6.0; SiteKiosk 7.8 Build 332)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (304, 273, N'Irfan', N'Shaikh', N'(301) 448-0057', N'(301) 448-0057', N'(301) 448-0057', N'Irfan11@gmail.com', N'I do not have previous experience', N'Al Fatih Academy primary and Division 1 A teams. Coaches are Mona Malik, Saira Sheikh, Rashida Nek, and Tania Ullah.', CAST(N'2014-01-15T17:59:53.790' AS DateTime), CAST(N'2014-01-17T09:44:06.573' AS DateTime), CAST(N'2014-01-15T19:19:49.320' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (305, NULL, N'Durga', N'Chilukuri', N'(240) 350-5715', N'(240) 350-5715', N'(240) 350-5715', N'iammounika123@gmail.com', NULL, NULL, CAST(N'2014-01-15T18:00:13.633' AS DateTime), CAST(N'2014-01-15T18:01:23.183' AS DateTime), NULL, N'Mozilla/5.0 (compatible; MSIE 9.10; Windows NT 6.1; WOW64; Trident/6.0; SiteKiosk 7.8 Build 332)')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (306, 274, N'Tracy', N'Grant', N'(703) 732-5127', N'(703) 732-5127', N'(703) 732-5127', N'sillyluc@aol.com', NULL, NULL, CAST(N'2014-01-15T19:48:52.143' AS DateTime), CAST(N'2014-01-15T19:49:34.060' AS DateTime), CAST(N'2014-01-15T19:53:33.607' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_1) AppleWebKit/537.73.11 (KHTML, like Gecko) Version/7.0.1 Safari/537.73.11')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (307, NULL, N'Jennifer ', N'Harding', N'(703) 217-8938', N'(703) 938-4034', N'(703) 217-8938', N'harding3007@verizon.net', NULL, N'Luther Jackson Middle School team with Laura Bligh as coach.', CAST(N'2014-01-15T20:15:03.263' AS DateTime), CAST(N'2014-01-15T20:18:04.260' AS DateTime), NULL, N'Mozilla/5.0 (iPad; CPU OS 7_0_4 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B554a Safari/9537.53')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (308, 275, N'John', N'Dillard', N'(703) 237-8107', N'(240) 515-0864', N'(240) 515-0864', N'wellforddillard@gmail.com', NULL, N'Forest Edge Elementary  Team 30388; Problem 3, division 1', CAST(N'2014-01-16T03:22:29.103' AS DateTime), CAST(N'2014-01-16T03:24:03.457' AS DateTime), CAST(N'2014-01-16T03:47:49.447' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (309, 276, N'Ahmed', N'Elrayah', N'(703) 346-4357', N'(703) 328-6408', N'(703) 328-6408', N'ahmed.elrayah@gmail.com', NULL, N'Al Fatih Academy Teams', CAST(N'2014-01-16T07:52:09.243' AS DateTime), CAST(N'2014-01-16T07:53:47.400' AS DateTime), CAST(N'2014-01-16T15:10:41.480' AS DateTime), N'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9) AppleWebKit/537.71 (KHTML, like Gecko) Version/7.0 Safari/537.71')
INSERT [dbo].[Volunteers] ([VolunteerID], [TeamID], [FirstName], [LastName], [DaytimePhone], [EveningPhone], [MobilePhone], [EmailAddress], [Notes], [VolunteerWantsToSee], [TimeRegistrationStarted], [TimeRegistered], [TimeAssignedToTeam], [UserAgent]) VALUES (310, 278, N'Pamela', N'Fox', N'(703) 734-2721', N'(703) 734-2721', N'(703) 677-7823', N'pamela.fox@cox.net', N'No previous experience', N'First grade team from Spring Hill Elementary School, Mallika DeHaven (coach), Caveman Art Show
', CAST(N'2014-01-17T05:31:54.677' AS DateTime), CAST(N'2014-01-17T05:34:44.380' AS DateTime), CAST(N'2014-01-18T11:54:14.663' AS DateTime), N'Mozilla/5.0 (iPad; CPU OS 7_0_3 like Mac OS X) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11B511 Safari/9537.53')
SET IDENTITY_INSERT [dbo].[Volunteers] OFF
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllTournamentRegistrationRecordsAndResetIdToZero]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================================
-- Author:		Robert Bernstein
-- Create date: 11/19/2013
-- Description:	Uses the TRUNCATE TABLE command to delete all the records
--				in the TournamentRegistration table and reset its identity
--				counter to zero.
-- =======================================================================
CREATE PROCEDURE [dbo].[DeleteAllTournamentRegistrationRecordsAndResetIdToZero]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 TRUNCATE table TournamentRegistration
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteNullJudgeRegistrations]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 12/09/2012
-- Description:	Delete all NULL judge registrations older
--              than the specified number of days.
-- ============================================================
CREATE PROCEDURE [dbo].[DeleteNullJudgeRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE
    FROM [DB_12824_registration].[dbo].[judges]
    WHERE TimeRegistered IS NULL
    AND TimeRegistrationStarted < DATEADD(DAY, -5, GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteNullTournamentRegistrations]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 01/06/2013
-- Description:	Delete all NULL Tournament Registrations older
--              than the specified number of days.
-- ============================================================
CREATE PROCEDURE [dbo].[DeleteNullTournamentRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE
    FROM [DB_12824_registration].[dbo].TournamentRegistration
    WHERE TimeRegistered IS NULL
    AND TimeRegistrationStarted < DATEADD(DAY, -5, GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteNullVolunteerRegistrations]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 01/06/2013
-- Description:	Delete all NULL Volunteer Registrations older
--              than the specified number of days.
-- ============================================================
CREATE PROCEDURE [dbo].[DeleteNullVolunteerRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE
    FROM [DB_12824_registration].[dbo].Volunteers
    WHERE TimeRegistered IS NULL
    AND TimeRegistrationStarted < DATEADD(DAY, -5, GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[DropAndRecreateTournamentRegistrationTable]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DropAndRecreateTournamentRegistrationTable]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    IF EXISTS (
       SELECT *
       FROM sys.tables
       WHERE name = N'TournamentRegistration'
     )
        DROP TABLE [dbo].[TournamentRegistration]

    /****** Object:  Table [dbo].[TournamentRegistration]    Script Date: 12/9/2012 7:10:17 PM ******/
    SET ANSI_NULLS ON

    SET QUOTED_IDENTIFIER ON

    CREATE TABLE [dbo].[TournamentRegistration](
	    [TeamID] [int] IDENTITY(1,1) NOT NULL,
	    [MembershipName] [nvarchar](50) NULL,
	    [MembershipNumber] [nvarchar](50) NULL,
	    [ProblemID] [int] NULL,
	    [Division] [nvarchar](50) NULL,
	    [SchoolID] [int] NULL,
	    [CoachFirstName] [nvarchar](50) NULL,
	    [CoachLastName] [nvarchar](50) NULL,
	    [CoachAddress] [nvarchar](255) NULL,
	    [CoachCity] [nvarchar](50) NULL,
	    [CoachStateOrProvince] [nvarchar](20) NULL,
	    [CoachZipCode] [nvarchar](20) NULL,
	    [CoachDaytimePhone] [nvarchar](30) NULL,
	    [CoachEveningPhone] [nvarchar](30) NULL,
	    [CoachEmail] [nvarchar](50) NULL,
	    [AltCoachFirstName] [nvarchar](50) NULL,
	    [AltCoachLastName] [nvarchar](50) NULL,
	    [AltCoachDaytimePhone] [nvarchar](50) NULL,
	    [AltCoachEveningPhone] [nvarchar](50) NULL,
	    [AltCoachEmail] [nvarchar](50) NULL,
	    [MemberFirstName1] [nvarchar](50) NULL,
	    [MemberLastName1] [nvarchar](50) NULL,
	    [MemberGrade1] [nvarchar](50) NULL,
	    [MemberFirstName2] [nvarchar](50) NULL,
	    [MemberLastName2] [nvarchar](50) NULL,
	    [MemberGrade2] [nvarchar](50) NULL,
	    [MemberFirstName3] [nvarchar](50) NULL,
	    [MemberLastName3] [nvarchar](50) NULL,
	    [MemberGrade3] [nvarchar](50) NULL,
	    [MemberFirstName4] [nvarchar](50) NULL,
	    [MemberLastName4] [nvarchar](50) NULL,
	    [MemberGrade4] [nvarchar](50) NULL,
	    [MemberFirstName5] [nvarchar](50) NULL,
	    [MemberLastName5] [nvarchar](50) NULL,
	    [MemberGrade5] [nvarchar](50) NULL,
	    [MemberFirstName6] [nvarchar](50) NULL,
	    [MemberLastName6] [nvarchar](50) NULL,
	    [MemberGrade6] [nvarchar](50) NULL,
	    [MemberFirstName7] [nvarchar](50) NULL,
	    [MemberLastName7] [nvarchar](50) NULL,
	    [MemberGrade7] [nvarchar](50) NULL,
	    [MemberFirstName8] [nvarchar](50) NULL,
	    [MemberLastName8] [nvarchar](50) NULL,
	    [MemberGrade8] [nvarchar](50) NULL,
	    [Spontaneous] [bit] NULL,
	    [Notes] [nvarchar](max) NULL,
	    [SpecialConsiderations] [nvarchar](max) NULL,
	    [SchedulingIssues] [nvarchar](max) NULL,
	    [Paid] [smallint] NULL,
	    [JudgeID] [smallint] NULL,
	    [TeamRegistrationFee] [nvarchar](20) NULL,
	    [VolunteerID] [int] NULL,
	    [TimeRegistrationStarted] [datetime] NULL,
	    [TimeRegistered] [datetime] NULL,
	    [UserAgent] [nvarchar](max) NULL,
     CONSTRAINT [PK_TournamentRegistration] PRIMARY KEY CLUSTERED 
    (
	    [TeamID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END
GO
/****** Object:  StoredProcedure [dbo].[FindAllJudgesAssignedToMoreThanOneTeam]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robert Bernstein
-- Create date: 1/22/2012
-- Description:	Find all judges assigned to more than one team.
-- =============================================
CREATE PROCEDURE [dbo].[FindAllJudgesAssignedToMoreThanOneTeam] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT t.JudgeID,
		   j.FirstName as 'Judge First Name',
		   j.LastName as 'Judge Last Name',
		   COUNT(t.[TeamID]) as 'TeamCount'
	FROM [DB_12824_registration].[dbo].[TournamentRegistration] as t,
		 [DB_12824_registration].[dbo].[judges] as j
	WHERE j.JudgeID = t.JudgeID
	GROUP BY t.JudgeID, j.FirstName, j.LastName
	HAVING COUNT(t.[TeamID]) > 1
END
GO
/****** Object:  StoredProcedure [dbo].[FindNullJudgeRegistrations]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 01/06/2013
-- Description:	Find NULL Judge Registration records where
--              the TimeRegistered is null, indicating an
--              incomplete registration.
-- ============================================================
CREATE PROCEDURE [dbo].[FindNullJudgeRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM [DB_12824_registration].[dbo].Judges
	WHERE TimeRegistered is null
END
GO
/****** Object:  StoredProcedure [dbo].[FindNullTournamentRegistrations]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 1/22/2012
-- Description:	Find NULL Tournament Registration records where
--              the TimeRegistered is null, indicating an
--              incomplete registration.
-- ============================================================
CREATE PROCEDURE [dbo].[FindNullTournamentRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM [DB_12824_registration].[dbo].[TournamentRegistration]
	WHERE TimeRegistered is null
END
GO
/****** Object:  StoredProcedure [dbo].[FindNullVolunteerRegistrations]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================
-- Author:		Robert Bernstein
-- Create date: 01/06/2013
-- Description:	Find NULL Volunteer Registration records where
--              the TimeRegistered is null, indicating an
--              incomplete registration.
-- ============================================================
CREATE PROCEDURE [dbo].[FindNullVolunteerRegistrations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM [DB_12824_registration].[dbo].Volunteers
	WHERE TimeRegistered is null
END
GO
/****** Object:  StoredProcedure [dbo].[GenerateImportFileForScoringProgram]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robert Bernstein
-- Create date: 1/21/2012
-- Description:	Generate the correct columns and headers for the import file for the Odyssey scoring program
-- =============================================
CREATE PROCEDURE [dbo].[GenerateImportFileForScoringProgram] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT '' as 'Number'
		  ,[ProblemID] as 'Problem'
		  ,[Division] = CASE t.Division WHEN '0' THEN 'Primary' ELSE t.Division END
		  ,s.Name as 'Name'
		  ,'' as 'Homeroom'
		  ,[CoachFirstName] as 'coachFirst'
		  ,[CoachLastName] as 'coachLast'
		  ,[CoachAddress] as 'coach_addr1'
		  ,[CoachCity] as 'coach_city'
		  ,[CoachState] as 'coach_state'
		  ,[CoachZipCode] as 'coach_zip'
		  ,[CoachDaytimePhone] as 'coach_phone'
		  ,[CoachEveningPhone] as 'coach_fax'
		  ,[CoachEmailAddress] as 'coach_email'
	FROM [DB_12824_registration].[dbo].[TournamentRegistration] as t, Schools as s
	WHERE t.SchoolID = s.ID
	ORDER BY ProblemID, Division
END
GO
/****** Object:  StoredProcedure [dbo].[ListAllTeamsRegisteredForTournament]    Script Date: 12/2/2023 8:02:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Robert Bernstein
-- Create date: 1/21/2012
-- Description:	List all teams registered for the tournament.
-- =============================================
CREATE PROCEDURE [dbo].[ListAllTeamsRegisteredForTournament] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [TeamID]
		  ,[MembershipName]
		  ,[MembershipNumber]
		  ,p.ProblemName
		  ,[Division] = CASE t.Division WHEN '0' THEN 'Primary' ELSE t.Division END
		  ,s.Name as 'School Name'
		  ,[CoachFirstName]
		  ,[CoachLastName]
		  ,[CoachAddress]
		  ,[CoachCity]
		  ,[CoachState]
		  ,[CoachZipCode]
		  ,[CoachDaytimePhone]
		  ,[CoachEveningPhone]
		  ,[CoachEmailAddress]
		  ,[AltCoachFirstName]
		  ,[AltCoachLastName]
		  ,[AltCoachDaytimePhone]
		  ,[AltCoachEveningPhone]
		  ,[AltCoachEmailAddress]
		  ,[MemberFirstName1]
		  ,[MemberLastName1]
		  ,[MemberGrade1]
		  ,[MemberFirstName2]
		  ,[MemberLastName2]
		  ,[MemberGrade2]
		  ,[MemberFirstName3]
		  ,[MemberLastName3]
		  ,[MemberGrade3]
		  ,[MemberFirstName4]
		  ,[MemberLastName4]
		  ,[MemberGrade4]
		  ,[MemberFirstName5]
		  ,[MemberLastName5]
		  ,[MemberGrade5]
		  ,[MemberFirstName6]
		  ,[MemberLastName6]
		  ,[MemberGrade6]
		  ,[MemberFirstName7]
		  ,[MemberLastName7]
		  ,[MemberGrade7]
		  ,[Spontaneous]
		  ,t.Notes
		  ,[SpecialConsiderations]
		  ,[SchedulingIssues]
		  ,[Paid]
		  ,[TimeRegistered]
		  ,[JudgeID]
		  ,[TeamRegistrationFee]
		  ,[VolunteerID]
		  ,[TimeRegistrationStarted]
		  ,[UserAgent]
	  FROM TournamentRegistration as t,
		   problem as p,
		   Schools as s
	  WHERE t.ProblemID = p.ProblemID
	  AND t.SchoolID = s.ID
	  ORDER BY t.TeamID
END
GO
USE [master]
GO
ALTER DATABASE [DB_12824_registration] SET  READ_WRITE 
GO

