USE [SquareNews]
GO

/****** Object:  Table [dbo].[ArticleImage]    Script Date: 13/08/2018 22:17:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArticleImage](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImagePath] [nvarchar](250) NULL,
	[IsVisible] [bit] NULL,
 CONSTRAINT [PK_ArticleImage] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

