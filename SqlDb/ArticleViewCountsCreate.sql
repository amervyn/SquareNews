USE [SquareNews]
GO

/****** Object:  Table [dbo].[ArticleViewCount]    Script Date: 13/08/2018 22:18:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArticleViewCount](
	[ArticleId] [int] NOT NULL,
	[ViewCount] [int] NULL,
	[LastUpdated] [smalldatetime] NULL,
	[IsVisible] [bit] NULL,
 CONSTRAINT [PK_ArticleViewCount] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

