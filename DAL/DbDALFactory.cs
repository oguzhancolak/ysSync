using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ys.DAL
{
    public class DbDALFactory : DALFactory
    {
        public override DALBaseClass GetDataAccessLayer(DataProviderType dataProviderType)
        {
            switch (dataProviderType)
            {
                case DataProviderType.MsSQL:
                    return new MsSqlDAL(Properties.Resources.MsSQLConnectionString);

                case DataProviderType.MySQL:
                    return new MySqlDAL(Properties.Resources.MySQLConnectionString);

                default:
                    throw new ArgumentException("Invalid DAL provider type.");
            }
        }
    }
}
