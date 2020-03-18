CREATE TABLE [dbo].[ContactUsRecipients](
	[ID] [tinyint] NOT NULL,
	[contact_name] [nvarchar](100) NOT NULL,
	[email_address] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_contact_us_recipients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]