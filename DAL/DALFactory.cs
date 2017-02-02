using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ys.DAL
{
    public enum DataProviderType
    {
        MsSQL,
        MySQL
    }
    public abstract class DALFactory
    {
        public abstract DALBaseClass GetDataAccessLayer(DataProviderType dataProviderType);
    }
}
