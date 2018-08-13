USE [SquareNews]
GO

/****** Object:  Table [dbo].[NewsSource]    Script Date: 13/08/2018 22:18:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewsSource](
	[SourceId] [int] IDENTITY(1,1) NOT NULL,
	[SourceName] [nvarchar](50) NULL,
	[Url] [nvarchar](500) NULL,
	[ApiKey] [nvarchar](250) NULL,
	[Enabled] [bit] NULL,
	[LastQueried] [smalldatetime] NULL,
 CONSTRAINT [PK_NewsSource] PRIMARY KEY CLUSTERED 
(
	[SourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

