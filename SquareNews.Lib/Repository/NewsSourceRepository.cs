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
    public class NewsSourceRepository : IDataRepository<NewsSource>
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


        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public NewsSource Create(NewsSource obj)
        {
            throw new NotImplementedException();
        }

        public NewsSource GetByKey(string key)
        {
            using (DatabaseFactory.DatabaseConnection)
            {
                string readByKey = "select * from NewsSource where SourceId='" + key + "'";

                return (NewsSource)_dbFactory.DatabaseConnection.Query<NewsSource>(readByKey).FirstOrDefault();
            }
        }

        public List<NewsSource> GetAll(DateTime fromDate)
        {
            using (DatabaseFactory.DatabaseConnection)
            {
                string readAllSp = "GetAllNewsSources";

                return _dbFactory.DatabaseConnection.Query<NewsSource>(readAllSp, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public bool Update(NewsSource obj)
        {
            throw new NotImplementedException();
        }
    }
}
