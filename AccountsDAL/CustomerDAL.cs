using AccountEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDAL
{
    public class CustomerDAL
    {
        public static List<CustomerEntity> GetAll(string name, string city)
        {

            List<CustomerEntity>  customers = new List<CustomerEntity>();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("Select * from Customer c");
                sbQuery.Append(" Where 1=1  ");
                if (name != null)
                    sbQuery.Append(" and c.Name like '%" + name + "%'");
                if (city != null)
                    sbQuery.Append("and c.City like '%" + city + "%'"); 

                AdoHelper db = new AdoHelper();
                {
                    //AdoHelper.ConnectionString = "Server=us3388966w1;port=3306;Database=accounts;Uid=root;Pwd=Password1";
                    using (MySqlDataReader rdr = db.ExecDataReader(sbQuery.ToString(), null))
                    {
                        while (rdr.Read())
                        {
                            CustomerEntity customer = new CustomerEntity();
                            customer.Id = rdr.GetInt32(0);
                            customer.Name = rdr.GetString(1);
                            customer.Alias = rdr["Alias"] != DBNull.Value ? rdr.GetString(2) : "";
                            customer.Phone = rdr["Phone"] != DBNull.Value ? rdr.GetInt64(3) : 0;
                            customer.Mobile = rdr["Mobile"] != DBNull.Value ? rdr.GetInt64(4) : 0;
                            
                            customer.Address1 = rdr["Address1"] != DBNull.Value ? rdr.GetString(5) : "";
                            customer.Address2 = rdr["Address2"]  != DBNull.Value? rdr.GetString(6):"";
                            customer.City = rdr.GetString(7);
                            customer.State = rdr.GetString(8);
                          //  customer.CreatedDt = rdr["CreatedDt"] != DBNull.Value ?rdr.GetDateTime(9):DateT;
                           // customer.ModifiedDt = rdr["ModifiedDt"] != DBNull.Value ?rdr.GetDateTime(10):null;
                            customer.IsActive = rdr.GetBoolean(11);
                            customer.Notes = rdr["Notes"] != DBNull.Value ? rdr.GetString(12) : "";
                            customer.Email = rdr["Email"] != DBNull.Value ? rdr.GetString(13) : "";
                            customers.Add(customer);
                        }
                    }
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            //customers[0].ModifiedDt = DateTime.Now; ;  //DateTime.Parse( DateTime.Now.ToString("yyyy-MM-dd h:mm tt"));// DateTime.Now;
            //Save(customers[0], 1);
            return customers;
        }

        public static bool Save(CustomerEntity customer)
        {
            string query = string.Empty;
            if (customer.Id == 0)
            {
                query = string.Format("Insert into Customer(Name,Alias,Phone,Mobile,Address1,Address2,City,State,CreatedDt,ModifiedDt,IsActive,Notes,Email) " +
               " values('{0}','{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}')",
               customer.Name,
               customer.Alias,
               customer.Phone,
               customer.Mobile,
               customer.Address1,
               customer.Address2,
               customer.City,
               customer.State,
               DateTime.Now.ToString("yyyy/MM/dd"),
               DateTime.Now.ToString("yyyy/MM/dd"),
               customer.IsActive,
               customer.Notes,
               customer.Email
               );
            }
            else
            {
                query = string.Format("Update Customer set Name = '{0}', Alias = '{1}', Phone = {2}, Mobile = {3},Address1 = '{4}', Address2 = '{5}',City = '{6}',State = '{7}',ModifiedDt = '{8}',IsActive={9},Notes = '{10}',Email='{12}' where Id = {11} ", 
              customer.Name,
              customer.Alias,
              customer.Phone,
              customer.Mobile,
              customer.Address1,
              customer.Address2,
              customer.City,
              customer.State,
              DateTime.Now.ToString("yyyy/MM/dd"),
              customer.IsActive,
              customer.Notes,
              customer.Id, customer.Email
              );
            }

            try
            {
                AdoHelper db = new AdoHelper();
                int result = db.ExecNonQuery(query, null);
                db.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public static List<string> GetLookUpValues(string columnName)
        {
            //"select distinct Name from Customer"
            string qry = "Select  distinct " + columnName + " from Customer";
            List<string> distinctData = new List<string>();
            AdoHelper db = new AdoHelper();
            {
                //AdoHelper.ConnectionString = "Server=us3388966w1;port=3306;Database=accounts;Uid=root;Pwd=Password1";
                using (MySqlDataReader rdr = db.ExecDataReader(qry, null))
                {
                    while (rdr.Read())
                    {
                        distinctData.Add(rdr.GetString(0));
                    }
                }
            }
            return distinctData;
        }

        public static Dictionary<Int32, string> GetAllCustomers()
        {
            //"select distinct Name from Customer"
            string qry = "Select  Id,Name  from Customer where IsActive = 1";
            Dictionary<Int32, string> customerList = new Dictionary<Int32, string>() ;
            customerList.Add(0, "----------Select----------");
            AdoHelper db = new AdoHelper();
            {
                //AdoHelper.ConnectionString = "Server=us3388966w1;port=3306;Database=accounts;Uid=root;Pwd=Password1";
                using (MySqlDataReader rdr = db.ExecDataReader(qry, null))
                {
                    while (rdr.Read())
                    {
                        customerList.Add(rdr.GetInt32(0),rdr.GetString(1));
                    }
                }
            }
            return customerList;
        }
    }
}

