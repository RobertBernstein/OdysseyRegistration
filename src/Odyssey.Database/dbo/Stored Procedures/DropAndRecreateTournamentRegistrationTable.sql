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