using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareNews.Lib.Database;
using SquareNews.Lib.Entities;
using Dapper;
using System.Data;

namespace SquareNews.Lib.Repository
{
    public class NewsApiSourceRepository : IDataRepository<NewsApiSource>
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

        public NewsApiSource Create(NewsApiSource obj)
        {
            using (DatabaseFactory.DatabaseConnection)
            {

                string checkExisting = "select count(*) from dbo.NewsApiSource";

                var count = _dbFactory.DatabaseConnection.Query<int>(checkExisting).FirstOrDefault();

                if (count<=0)
                {
                    string readAllSp = "insert into dbo.NewsApiSource values(@ApiSourceName, @Name, @Description, @Url, @Category, @Language, @Country, @Enabled)";

                    var result = DatabaseFactory.DatabaseConnection.Execute(readAllSp, new
                    {
                        obj.ApiSourceName,
                        obj.Name,
                        obj.Description,
                        obj.Url,
                        obj.Category,
                        obj.Language,
                        obj.Country,
                        obj.Enabled
                    });
                }

            }

            return obj;
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public List<NewsApiSource> GetAll(DateTime fromDate)
        {
            using (DatabaseFactory.DatabaseConnection)
            {
                string readAll = "select * from dbo.NewsApiSource";

                return _dbFactory.DatabaseConnection.Query<NewsApiSource>(readAll).ToList();
            }
        }

        public NewsApiSource GetByKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(NewsApiSource obj)
        {
            throw new System.NotImplementedException();
        }
    }
}