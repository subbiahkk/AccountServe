using AccountEntities;
using AccountsDAL;
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
                return LoanDAL.Save(loanMasterEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static List<LoanDetailsEntity> GetLoanDetails(Int32 loanId)
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

    }
}
