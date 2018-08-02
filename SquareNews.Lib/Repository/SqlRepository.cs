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
    public class SqlRepository<T> : IDataRepository<T> where T : class
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

        public string Create(T obj)
        {
            throw new NotImplementedException();
        }

        T IDataRepository<T>.GetByKey(string key)
        {
            using (DatabaseFactory.DatabaseConnection)
            {
                string readByKey = "select * from NewsSource where SourceId='" + key +"'";

                return (T)_dbFactory.DatabaseConnection.Query<T>(readByKey).FirstOrDefault();
            }
        }

        List<T> IDataRepository<T>.GetAll()
        {
            using (DatabaseFactory.DatabaseConnection)
            {
                string readAllSp = "GetAllNewsSources";

                return _dbFactory.DatabaseConnection.Query<T>(readAllSp, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }
    }
}
