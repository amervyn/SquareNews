USE [SquareNews]
GO

/****** Object:  StoredProcedure [dbo].[UpdateArticleCountry]    Script Date: 13/08/2018 22:18:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateArticleCountry]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 
 update SquareNews.dbo.Article
  set Country=n.Country
  from Article a inner join NewsApiSource n
	on a.NewsApiSourceId=n.ApiSourceName
	where a.Country is null
END

GO

