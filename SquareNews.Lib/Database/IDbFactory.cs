using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareNews.Lib.Database
{
    public abstract class IDbFactory
    {
        public string connectionString;

        public abstract IDbConnection DbConnection();

        public abstract IDbDataParameter CreateParametery(string paramName, object paramValue);

        public abstract IDbCommand CreateCommand(string commandText, IDbConnection connection);

        public abstract IDbCommand CreateSPCommand(string spName, IDbCommand connection);

    }
}
