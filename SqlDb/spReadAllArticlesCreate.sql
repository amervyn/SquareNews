USE [SquareNews]
GO

/****** Object:  StoredProcedure [dbo].[ReadAllArticles]    Script Date: 13/08/2018 22:18:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ReadAllArticles]
	@fromDate smalldatetime
	,@rowCount int
	,@rowStart int
	,@country nvarchar(10) = null	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select COUNT(*) from dbo.Article
	where CreatedOn>=@fromDate
		
	SELECT  *
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY CreatedOn desc, PublishedOn desc ) AS RowNum, 
		a.[ArticleId]
      ,[SourceId]
      ,[NewsApiSourceId]
      ,[Headline]
      ,[Description]
      ,[ImageUrl]
      ,[Url]
	  ,c.ViewCount
	  ,c.LastUpdated
      ,[Country]
      ,a.[IsVisible]
      ,[PublishedOn]
      ,[CreatedOn]
  FROM [dbo].[Article] a
  left join dbo.ArticleViewCount c on
  a.ArticleId=c.ArticleId
          WHERE     CreatedOn >= @fromDate and (@country is null OR Country=@country)
        ) AS RowConstrainedResult
WHERE   RowNum >= @rowStart
    AND RowNum < @rowStart + @rowCount
ORDER BY RowNum

	--SELECT a.[ArticleId]
 --     ,[SourceId]
 --     ,[NewsApiSourceId]
 --     ,[Headline]
 --     ,[Description]
 --     ,[ImageUrl]
 --     ,[Url]
	--  ,c.ViewCount
	--  ,c.LastUpdated
 --     ,[Country]
 --     ,a.[IsVisible]
 --     ,[PublishedOn]
 --     ,[CreatedOn]
 -- FROM [dbo].[Article] a
 -- left join dbo.ArticleViewCount c on
 -- a.ArticleId=c.ArticleId
 -- where CreatedOn>=@fromDate
END

GO

