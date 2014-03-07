using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountEntities
{
    public class LoanDetailsEntity : IDataErrorInfo
    {
        public LoanDetailsEntity()
        {
            LoanMaster = new LoanMasterEntity();
        }

        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private int _LoanId;

        public int LoanId
        {
            get { return _LoanId; }
            set { _LoanId = value; }
        }

        private string _ItemDescription;

        public string ItemDescription
        {
            get { return _ItemDescription; }
            set { _ItemDescription = value; }
        }

        private decimal _ItemGrams;

        public decimal ItemGrams
        {
            get { return _ItemGrams; }
            set { _ItemGrams = value; }
        }

        private DateTime _CreatedDt;

        public DateTime CreatedDt
        {
            get { return _CreatedDt; }
            set { _CreatedDt = value; }
        }



        private DateTime _ModifiedDt;

        public DateTime ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }


        private LoanMasterEntity _LoanMaster;

        public LoanMasterEntity LoanMaster
        {
            get { return _LoanMaster; }
            set { _LoanMaster = value; }
        }
        

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "LoanId")
                {
                    if (LoanId <= 0)
                        result = "Please select Loan";
                }
                if (columnName == "ItemGrams")
                {
                    if (ItemGrams <= 0)
                        result = "Please enter Item Grams";
                }
               
                if (columnName == "ItemDescription")
                {
                    if (string.IsNullOrEmpty(ItemDescription) )
                        result = "Please enter Item Description";
                }
               
                return result;
            }
        }
    }
}
