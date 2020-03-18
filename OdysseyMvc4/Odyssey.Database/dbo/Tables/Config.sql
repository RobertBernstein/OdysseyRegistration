﻿CREATE TABLE [dbo].[Config](
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](800) NULL,
 CONSTRAINT [PK_config] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]