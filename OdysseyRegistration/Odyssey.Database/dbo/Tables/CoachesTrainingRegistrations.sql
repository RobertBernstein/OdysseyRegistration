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