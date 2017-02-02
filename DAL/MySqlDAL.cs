using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ys.DAL
{
    class MySqlDAL : DALBaseClass
    {
        public MySqlDAL() { }

        public MySqlDAL(string connectionString) 
        {
            this.ConnectionString = Properties.Resources.MySQLConnectionString; 
        }
 
        public override IDbConnection GetDataProviderConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection();
        }
 
        public override IDbCommand GeDataProviderCommand()
        {
            return new MySql.Data.MySqlClient.MySqlCommand();
        }
 
        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            return new MySql.Data.MySqlClient.MySqlDataAdapter();
        }
    }
}
