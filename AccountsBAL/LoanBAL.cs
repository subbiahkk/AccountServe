using AccountEntities;
using AccountsDAL;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsBAL
{
    public static class LoanBAL
    {
        public static List<LoanMasterEntity> GetAll(Int32 customerId)
        {
            try
            {
                return LoanDAL.GetAll(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Save(LoanMasterEntity loanMasterEntity)
        {
            try
            {
                return LoanDAL.SaveMaster(loanMasterEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static ObservableCollection<LoanDetailsEntity> GetLoanDetails(Int32 loanId)
        {
            try
            {
                return LoanDAL.GetLoanDetails(loanId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SaveLoanDetails(ObservableCollection<LoanDetailsEntity> loanDetails)
        {
            try
            {
                foreach(LoanDetailsEntity loanDetail in loanDetails)
                {
                    LoanDAL.SaveDetails(loanDetail);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool RemoveLoanDetails(LoanDetailsEntity loanDetail)
        {
            try
            {

                return LoanDAL.RemoveDetail(loanDetail);
               
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
