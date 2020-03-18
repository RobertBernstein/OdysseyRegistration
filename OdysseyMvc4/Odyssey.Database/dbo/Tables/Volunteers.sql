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