using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SquareNews.NetCore.Lib;

namespace SquareNews.Lib.Database
{
    public class SqlDbFactory : DbFactory
    {
        private DbConnection _connection;
        
        public override DbConnection DatabaseConnection
        {
            get
            {
                _connection = new SqlConnection(new AppSettings().SqlServer);

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
