using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountEntities;
using AccountsDAL;
namespace AccountsBAL
{
    public static class CustomerBAL
    {
        public static List<CustomerEntity> GetAll(string name, string city)
        {
            try
            {
                return CustomerDAL.GetAll(name,city);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<string> GetAllLookupValues(string columnName)
        {
            try
            {
                return CustomerDAL.GetLookUpValues(columnName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Dictionary<Int32, string> GetAllCustomers()
        {
            try
            {
                return CustomerDAL.GetAllCustomers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Save(CustomerEntity customer)
        {
            try
            {
                return CustomerDAL.Save(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
