USE [SquareNews]
GO

/****** Object:  Table [dbo].[NewsApiSource]    Script Date: 13/08/2018 22:18:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewsApiSource](
	[ApiSourceId] [int] IDENTITY(1,1) NOT NULL,
	[ApiSourceName] [nvarchar](250) NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[Url] [nvarchar](250) NULL,
	[Category] [nvarchar](50) NULL,
	[Language] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_NewsApiSource] PRIMARY KEY CLUSTERED 
(
	[ApiSourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

