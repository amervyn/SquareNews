﻿using System.Collections.Generic;
using System.Linq;
using Dapper;
using SquareNews.Lib.Database;
using SquareNews.Lib.Entities;
namespace SquareNews.Lib.Repository
{
    public class ArticleRepository : IDataRepository<NewsArticle>
    {
        private DbFactory _dbFactory;
        public DbFactory DatabaseFactory
        {
            get
            {
                if (_dbFactory == null)
                {
                    _dbFactory = new SqlDbFactory();
                }

                return _dbFactory;
            }
            set
            {
                _dbFactory = value;
            }
        }

        public NewsArticle Create(NewsArticle obj)
        {
            using (DatabaseFactory.DatabaseConnection)
            {

                string checkExisting = "select count(*) from dbo.Article where Headline=@headline";

                var count = _dbFactory.DatabaseConnection.Query<int>(checkExisting, new { obj.Headline }).FirstOrDefault();

                if (count <= 0)
                {
                    string insertArticle = "insert into dbo.Article (Headline, SourceId, NewsApiSourceId, Description, ImageUrl, Url, Country,  IsVisible, CreatedOn, PublishedOn) values(@Headline, @SourceId, @NewsApiSourceId, @Description, @ImageUrl, @Url, @Country, @IsVisible, @CreatedOn, @PublishedOn)";

                    var result = DatabaseFactory.DatabaseConnection.Execute(insertArticle, new
                    {
                        obj.Headline,
                        obj.SourceId,
                        obj.NewsApiSourceId,
                        obj.Description,
                        obj.ImageUrl,
                        obj.Url,
                        obj.Country,
                        obj.IsVisible,
                        obj.CreatedOn,
                        obj.PublishedOn
                    });
                }

            }

            return obj;
        }

        public void Delete(string key)
        {
            throw new System.NotImplementedException();
        }

        public List<NewsArticle> GetAll()
        {
            using (DatabaseFactory.DatabaseConnection)
            {
                var readAllArticlesSp = "ReadAllArticles";
                var res = _dbFactory.DatabaseConnection.Query<NewsArticle>(readAllArticlesSp, commandType: System.Data.CommandType.StoredProcedure);
                return res.ToList();
            }
        }

        public NewsArticle GetByKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(NewsArticle obj)
        {
            throw new System.NotImplementedException();
        }
    }
}