using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

namespace SquareNews.Lib.Database
{
    public class SqlDbFactory : DbFactory
    {
        private DbConnection _connection;

        public override DbConnection DatabaseConnection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlserver"].ConnectionString);
                }
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        public override IDbCommand CreateCommand(string commandText)
        {
            return null;
        }

        public override IDbDataParameter CreateParametery(string paramName, object paramValue)
        {
            return null;
        }

        public override IDbCommand CreateSPCommand(string spName)
        {
            return null;
        }

    }
}
