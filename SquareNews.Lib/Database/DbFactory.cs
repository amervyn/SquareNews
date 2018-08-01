using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;

namespace SquareNews.Lib.Database
{
    public abstract class DbFactory
    {
        public virtual DbConnection DatabaseConnection { get; set; }

        public abstract IDbDataParameter CreateParametery(string paramName, object paramValue);

        public abstract IDbCommand CreateCommand(string commandText);

        public abstract IDbCommand CreateSPCommand(string spName);

    }
}
