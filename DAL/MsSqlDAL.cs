using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ys.DAL
{
    class MsSqlDAL : DALBaseClass
    {
        public MsSqlDAL() { }
 
        public MsSqlDAL(string connectionString) 
        {
            this.ConnectionString = connectionString; 
        }
 
        public override IDbConnection GetDataProviderConnection()
        {
            return new SqlConnection();
        }
 
        public override IDbCommand GeDataProviderCommand()
        {
            return new SqlCommand();
        }
 
        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            return new SqlDataAdapter();
        }
    }
}
