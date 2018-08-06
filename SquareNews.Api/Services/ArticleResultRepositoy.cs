using SquareNews.Api.Models;
using SquareNews.Lib.Database;
using SquareNews.Lib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using SquareNews.Lib.Entities;

namespace SquareNews.Api.Services
{
    public class ArticleResultRepositoy : IDataRepository<ArticleResult>
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

        public ArticleResult Create(ArticleResult obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public List<ArticleResult> GetAll(DateTime fromDate, int rowCount)
        {
            using (DatabaseFactory.DatabaseConnection)
            {
                var readAllArticlesSp = "ReadAllArticles";
                var p = new DynamicParameters();
                p.Add("@fromDate", fromDate);
                p.Add("@rowCount", rowCount < 0 ? 20 : rowCount);
                using (var res = _dbFactory.DatabaseConnection.QueryMultiple(readAllArticlesSp, p, commandType: System.Data.CommandType.StoredProcedure))
                {

                    var count = res.Read<int>().First();
                    var articles = res.Read<NewsArticle>().ToList();

                    return new ArticleResult
                    {
                        TotalResults = count,
                        Articles = articles
                    };
                }
            }
        }

        public ArticleResult GetByKey(string key)
        {
            return null;
        }

        public bool Update(ArticleResult obj)
        {
            return null;
        }
    }
}