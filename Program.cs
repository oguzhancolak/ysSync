using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ys.DAL;
using ys.DAO;

namespace ys
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.start();
        }

        private void start()
        {
            Exception exError = null;
            // DAO initialize
            CustomerDAO cDao = new CustomerDAO();
            
            do
            {
                Console.WriteLine("Sync running...");

                // sql'den kayıtları çekiyoruz.
                var msSQLcustomers = cDao.Select(DataProviderType.MsSQL, out exError);

               // mysql'den kayıtları çekiyoruz.
                var mySQLcustomers = cDao.Select(DataProviderType.MySQL, out exError);

                // silinecekleri isDelete olarak işaretliyoruz.
                var toDeleteMsSQL = (from ms in msSQLcustomers
                                     where !mySQLcustomers.Any(n => n.Id == ms.Id)
                                     select new Customer
                                     {
                                         Id = ms.Id,
                                         isDelete = true
                                     }).ToList();

                // tek foreach ile dönmek için birleştiriyoruz.
                mySQLcustomers.AddRange(toDeleteMsSQL);

                foreach (var cust in mySQLcustomers)
                {
                    // Silinecek bir kayıt varsa buraya girip delete 
                    // metoduna girecek.
                    if(cust.isDelete) 
                    {
                        Console.WriteLine("Deleting : " + cust.Id);
                        cDao.Delete(cust.Id, out exError);
                    }
                    else
                    {
                        var foundUser = msSQLcustomers.FirstOrDefault(n => n.Id == cust.Id);

                        // Eğer böyle bir kayıt yoksa create metoduna gidecek.
                        if (foundUser == null)
                        {
                            Console.WriteLine("Creating : " + cust.Id);
                            cDao.Create(cust, out exError);
                        }
                        else
                        {
                            // Reflection ile iki obje arasındaki farka bakıp
                            // güncellenecek alan varsa update metoduna giriyor.
                            foreach (var prop in foundUser.GetType().GetProperties())
                            {
                                var custCheck = cust.GetType().GetProperty(prop.Name).GetValue(cust, null);
                                if (custCheck == null)
                                    continue;
                                if (custCheck.ToString().Trim() != prop.GetValue(foundUser, null).ToString().Trim())
                                {
                                    Console.WriteLine(string.Format("Updating : {0} Id = {1}", prop.Name, cust.Id));
                                    cDao.Update(cust, out exError);
                                }
                            }
                        }
                    }
                }

                // 1sn bekletiyoruz.
                Thread.Sleep(1000);

            } while (true);
        }
    }
}
