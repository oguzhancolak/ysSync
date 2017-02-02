using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ys.DAL;

namespace ys.DAO
{
    // inner class'ımızı oluşturup
    // genel class üzerine birleştiriyoruz.
    public class Customer
    {
        /// <summary>
        /// A user Id
        /// </summary>
        public int Id = 0;

        /// <summary>
        /// The FirstName
        /// </summary>
        /// 
        public string FirstName { get; set; }

        /// <summary>
        /// The LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The BirthDate
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// The isDelete
        /// </summary>
        public bool isDelete { get; set; }
    }

    // abstract class üzerinden kalıtım yapıyoruz.
    public class CustomerDAO : IDataMapper<Customer>
    {
        private IDbDataParameter AddParam(IDbCommand command, string paramName, object value)
        {
            var param = command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }

        public override List<Customer> Select(DataProviderType providerType,  out Exception exError)
        {
            List<Customer> returnValue = new List<Customer>();
            exError = null;

            try
            {
                var connectionString = string.Empty;
                var connectionQuery = string.Empty;
                switch (providerType)
                {
                    case DataProviderType.MsSQL: 
                        connectionString = Properties.Resources.MsSQLConnectionString;
                        connectionQuery = Properties.Resources.MsSQLSelect;
                        break;
                    case DataProviderType.MySQL: 
                        connectionString = Properties.Resources.MySQLConnectionString;
                        connectionQuery = Properties.Resources.MySQLSelect;
                        break;
                    default:
                        break;
                }
                DALFactory Factory = new DbDALFactory();
                var DAL = Factory.GetDataAccessLayer(providerType);
                var connection = DAL.OpenConnection(connectionString);

                while (connection.State != ConnectionState.Open)
                {
                    Console.WriteLine("Connecting...");
                    connection = DAL.OpenConnection(connectionString);
                }

                using (IDbCommand command = DAL.GeDataProviderCommand())
                {
                    command.CommandText = connectionQuery;
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Customer()
                            {
                                Id = reader.GetInt32(0),
                                FirstName = !String.IsNullOrEmpty(reader.GetString(1)) ? reader.GetString(1) : null,
                                LastName = !String.IsNullOrEmpty(reader.GetString(2)) ? reader.GetString(2) : null,
                                Email = reader.GetString(3),
                                Password = reader.GetString(4),
                                BirthDate = reader.GetDateTime(5) != null ? reader.GetDateTime(5) : DateTime.MinValue,
                                isDelete = false
                            });
                    }
                    command.Connection.Close();
                }
            }
            catch (InvalidOperationException invalid)
            {
                exError = invalid;
            }
            catch (Exception ex)
            {
                exError = ex;
            }

            return returnValue;
        }

        public override bool Create(Customer instance, out Exception exError)
        {
            exError = null;

            try
            {
                var connectionString = Properties.Resources.MsSQLConnectionString;
                var connectionQuery = Properties.Resources.MsSQLInsert;

                DALFactory Factory = new DbDALFactory();
                var DAL = Factory.GetDataAccessLayer(DataProviderType.MsSQL);
                var connection = DAL.OpenConnection(connectionString);

                while (connection.State != ConnectionState.Open)
                {
                    Console.WriteLine("Connecting...");
                    connection = DAL.OpenConnection(connectionString);
                }

                using (IDbCommand command = DAL.GeDataProviderCommand())
                {
                    command.CommandText = connectionQuery;
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    command.Parameters.Add(AddParam(command, "@Id", instance.Id));
                    command.Parameters.Add(AddParam(command, "@FirstName", instance.FirstName));
                    command.Parameters.Add(AddParam(command, "@LastName", instance.LastName ?? ""));
                    command.Parameters.Add(AddParam(command, "@Email", instance.Email));
                    command.Parameters.Add(AddParam(command, "@Password", instance.Password));
                    command.Parameters.Add(AddParam(command, "@BirthDate", instance.BirthDate));
                    IDataReader reader = command.ExecuteReader();
                    
                    command.Connection.Close();
                }
            }
            catch (InvalidOperationException invalid)
            {
                exError = invalid;
                return false;
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }

            return true;
        }

        public override Customer Read(int ID, out Exception exError)
        {
            throw new NotImplementedException();
        }

        public override Customer Read(Customer instance, out Exception exError)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Customer instance, out Exception exError)
        {
            exError = null;

            try
            {
                var connectionString = Properties.Resources.MsSQLConnectionString;
                var connectionQuery = Properties.Resources.MsSQLUpdate;

                DALFactory Factory = new DbDALFactory();
                var DAL = Factory.GetDataAccessLayer(DataProviderType.MsSQL);
                var connection = DAL.OpenConnection(connectionString);

                while (connection.State != ConnectionState.Open)
                {
                    Console.WriteLine("Connecting...");
                    connection = DAL.OpenConnection(connectionString);
                }

                using (IDbCommand command = DAL.GeDataProviderCommand())
                {
                    command.CommandText = connectionQuery;
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    command.Parameters.Add(AddParam(command, "@Id", instance.Id));
                    command.Parameters.Add(AddParam(command, "@FirstName", instance.FirstName));
                    command.Parameters.Add(AddParam(command, "@LastName", instance.LastName ?? ""));
                    command.Parameters.Add(AddParam(command, "@Email", instance.Email));
                    command.Parameters.Add(AddParam(command, "@Password", instance.Password));
                    command.Parameters.Add(AddParam(command, "@BirthDate", instance.BirthDate));
                    IDataReader reader = command.ExecuteReader();

                    command.Connection.Close();
                }
            }
            catch (InvalidOperationException invalid)
            {
                exError = invalid;
                return false;
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }

            return true;
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                var connectionString = Properties.Resources.MsSQLConnectionString;
                var connectionQuery = Properties.Resources.MsSQLDelete;

                DALFactory Factory = new DbDALFactory();
                var DAL = Factory.GetDataAccessLayer(DataProviderType.MsSQL);
                var connection = DAL.OpenConnection(connectionString);

                while (connection.State != ConnectionState.Open)
                {
                    Console.WriteLine("Connecting...");
                    connection = DAL.OpenConnection(connectionString);
                }

                using (IDbCommand command = DAL.GeDataProviderCommand())
                {
                    command.CommandText = connectionQuery;
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    command.Parameters.Add(AddParam(command, "@Id", ID));
                    IDataReader reader = command.ExecuteReader();

                    command.Connection.Close();
                }
            }
            catch (InvalidOperationException invalid)
            {
                exError = invalid;
                return false;
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }

            return true;
        }

        public override bool Delete(Customer instance, out Exception exError)
        {
            throw new NotImplementedException();
        }
    }
}
