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