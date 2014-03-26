using AccountEntities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDAL
{
    public class LoanDAL
    {
        #region Loan master

        public static List<LoanMasterEntity> GetAll(Int32 customerId)
        {

            List<LoanMasterEntity> loans = new List<LoanMasterEntity>();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("SELECT lm.*,cu.Name,cu.Id FROM accounts.loanmaster lm, customer cu where cu.Id = lm.CustomerId");
                
                if (customerId >0)
                    sbQuery.Append(" and lm.CustomerId = " + customerId.ToString());
                

                AdoHelper db = new AdoHelper();
                {
                    //AdoHelper.ConnectionString = "Server=us3388966w1;port=3306;Database=accounts;Uid=root;Pwd=Password1";
                    using (MySqlDataReader rdr = db.ExecDataReader(sbQuery.ToString(), null))
                    {
                        while (rdr.Read())
                        {
                            LoanMasterEntity loan = new LoanMasterEntity();
                            loan.Id = rdr.GetInt32(0);
                            loan.CustomerId = rdr.GetInt32(1);
                            loan.LoanAmount = rdr.GetInt32(2); // rdr["LoanAmount"] != DBNull.Value ? rdr.GetString(2) : "";
                            loan.TotalGrams = rdr.GetDecimal(3);
                            loan.NrOfItems = rdr.GetInt32(4);
                            loan.ItemDescription = rdr["ItemDescription"] != DBNull.Value ? rdr.GetString(5) : "";//rdr["Phone"] != DBNull.Value ? rdr.GetInt64(3) : 0;
                            loan.Notes = rdr["Notes"] != DBNull.Value ? rdr.GetString(6) : "";
                            loan.CreatedDt = rdr.GetDateTime(7);
                            loan.ModifiedDt = rdr.GetDateTime(8);
                            loan.IsActive = rdr.GetBoolean(9);

                            loan.BillId = rdr["BillId"] != DBNull.Value ? rdr.GetString(10) : "";
                            loan.BillDt = rdr.GetDateTime(11);

                            loan.Customer.Name = rdr["Name"] != DBNull.Value ? rdr.GetString(12) : "";
                            loan.Customer.Id = rdr.GetInt32(13);
                            loans.Add(loan);
                        }
                    }
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }            
            return loans;
        }

        public static bool SaveMaster(LoanMasterEntity loanMaster)
        {
            string query = string.Empty;
            if (loanMaster.Id == 0)
            {
                query = string.Format("Insert into LoanMaster(CustomerId,LoanAmount,TotalGrams,NrOfItems,Notes,CreatedDt,ModifiedDt,IsActive,BillId,BillDt) " +
               " values({0},{1},{2},{3},'{4}','{5}','{6}',{7},'{8}','{9}')",
               loanMaster.CustomerId,
               loanMaster.LoanAmount,
               loanMaster.TotalGrams,
               loanMaster.NrOfItems,
               loanMaster.Notes,              
               DateTime.Now.ToString("yyyy/MM/dd"),
               DateTime.Now.ToString("yyyy/MM/dd"),
               loanMaster.IsActive,
               loanMaster.BillId,
               loanMaster.BillDt.ToString("yyyy/MM/dd")
               );
            }
            else
            {
                query = string.Format("Update LoanMaster set CustomerId = {0}, LoanAmount = {1}, TotalGrams = {2}, NrOfItems = {3},Notes = '{4}', ModifiedDt = '{5}',IsActive={6},BillId = '{7}',BillDt='{8}' where Id = {9} ",
              loanMaster.CustomerId,
               loanMaster.LoanAmount,
               loanMaster.TotalGrams,
               loanMaster.NrOfItems,
               loanMaster.Notes,               
               DateTime.Now.ToString("yyyy/MM/dd"),
               loanMaster.IsActive,
               loanMaster.BillId,
               loanMaster.BillDt.ToString("yyyy/MM/dd"),
               loanMaster.Id
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

        #endregion

        #region Loan Details

        public static ObservableCollection<LoanDetailsEntity> GetLoanDetails(Int32 loanID)
        {

            ObservableCollection<LoanDetailsEntity> loanDetails = new ObservableCollection<LoanDetailsEntity>();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("SELECT ld.*,lm.BillId,cu.Id,cu.Name FROM accounts.loandetails ld, loanmaster lm, customer cu  where lm.Id = ld.LoanId and cu.Id = lm.CustomerId");

                if (loanID > 0)
                    sbQuery.Append(" and ld.LoanId = " + loanID.ToString());


                AdoHelper db = new AdoHelper();
                {
                    //AdoHelper.ConnectionString = "Server=us3388966w1;port=3306;Database=accounts;Uid=root;Pwd=Password1";
                    using (MySqlDataReader rdr = db.ExecDataReader(sbQuery.ToString(), null))
                    {
                        while (rdr.Read())
                        {
                            LoanDetailsEntity loandetail = new LoanDetailsEntity();
                            loandetail.Id = rdr.GetInt32(0);
                            loandetail.LoanId = rdr.GetInt32(1);
                            loandetail.ItemDescription = rdr.GetString(2); // rdr["LoanAmount"] != DBNull.Value ? rdr.GetString(2) : "";
                            loandetail.ItemGrams = rdr.GetDecimal(3);                            
                            loandetail.CreatedDt = rdr.GetDateTime(4);
                            loandetail.ModifiedDt = rdr.GetDateTime(5);
                            loandetail.LoanMaster.Id = rdr.GetInt32(1);
                            loandetail.LoanMaster.BillId = rdr.GetString(6);
                            loandetail.LoanMaster.Customer.Id = rdr.GetInt32(7);
                            loandetail.LoanMaster.Customer.Name = rdr.GetString(8);
                            loanDetails.Add(loandetail);
                        }
                    }
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return loanDetails;
        }




        public static bool SaveDetails(LoanDetailsEntity loanDetails)
        {
            string query = string.Empty;
            if (loanDetails.Id == 0)
            {
                query = string.Format("Insert into loandetails(LoanId,ItemDescription,ItemGrams,CreatedDt,ModifiedDt) " +
               " values({0},'{1}',{2},'{3}','{4}')",
               loanDetails.LoanMaster.Id,
               loanDetails.ItemDescription,
               loanDetails.ItemGrams,              
               DateTime.Now.ToString("yyyy/MM/dd"),
               DateTime.Now.ToString("yyyy/MM/dd")              
               );
            }
            else
            {
                query = string.Format("Update loandetails set ItemDescription = '{0}', ItemGrams = {1},  ModifiedDt = '{2}' where Id = {3} ",
              loanDetails.ItemDescription,
               loanDetails.ItemGrams,               
               DateTime.Now.ToString("yyyy/MM/dd"),
               loanDetails.Id
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

        public static bool RemoveDetail(LoanDetailsEntity loanDetails)
        {
            string query = string.Empty;
            //if (loanDetails.Id == 0)
            {
                query = string.Format("Delete from loandetails  " +
               " where Id ={0}",
               loanDetails.Id              
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

        #endregion

    }
}
