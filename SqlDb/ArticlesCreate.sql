USE [SquareNews]
GO

/****** Object:  Table [dbo].[Article]    Script Date: 13/08/2018 22:17:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Article](
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[SourceId] [int] NULL,
	[NewsApiSourceId] [nvarchar](50) NULL,
	[Headline] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](1000) NULL,
	[Url] [nvarchar](1000) NULL,
	[Country] [nvarchar](50) NULL,
	[IsVisible] [bit] NULL,
	[PublishedOn] [smalldatetime] NULL,
	[CreatedOn] [smalldatetime] NULL,
 CONSTRAINT [PK_Article_1] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

