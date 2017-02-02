using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ys.DAL
{
    public abstract class DALBaseClass
    {
        private string strConnectionString;
        private IDbConnection connection;
        private IDbCommand command;
        private IDbTransaction transaction;
 
        public string ConnectionString
        {
            get
            {
              if (strConnectionString == string.Empty || strConnectionString.Length == 0)
                    throw new ArgumentException("Invalid database connection string.");
                
              return strConnectionString;
            }
            set
            { strConnectionString = value; }
        }
 
        protected DALBaseClass() { }
 
        public abstract IDbConnection GetDataProviderConnection();
        
        public abstract IDbCommand GeDataProviderCommand();
        
        public abstract IDbDataAdapter GetDataProviderDataAdapter();
 
        #region Database Transaction

        public IDbConnection OpenConnection(string connectionString)
        {
            IDbConnection Response;
            try
            {
             connection = GetDataProviderConnection(); // instantiate a connection object
             connection.ConnectionString = connectionString;
             connection.Open(); // open connection
             Response = connection;
            }
            catch
            {
                connection.Close();
                Response = connection;
            }
            return Response;
        }
 
        #endregion
    }
}
