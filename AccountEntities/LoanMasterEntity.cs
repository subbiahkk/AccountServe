using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountEntities
{
    public class LoanMasterEntity : IDataErrorInfo
    {
        //public int Id { get; set; }
        //public int CustomerId { get; set; }
        //public int LoanAmount { get; set; }
        //public Nullable<decimal> TotalGrams { get; set; }
        //public Nullable<int> NrOfItems { get; set; }
        //public string ItemDescription { get; set; }
        //public string Notes { get; set; }
        //public Nullable<System.DateTime> CreatedDt { get; set; }
        //public Nullable<System.DateTime> ModifiedDt { get; set; }
        //public Nullable<bool> IsActive { get; set; }
        //public string BillId { get; set; }

        public LoanMasterEntity()
        {
            this.Customer = new CustomerEntity();
        }

        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private int _CustomerId;

        public int CustomerId
        {
            get { return _CustomerId; }
            set 
            { 
                _CustomerId = value;
                //if (_CustomerId == 0)
                //{
                //    string str = this["CustomerId"];
                //}
            }
        }

        private int _LoanAmount;

        public int LoanAmount
        {
            get { return _LoanAmount; }
            set { _LoanAmount = value; }
        }

        private decimal _TotalGrams;

        public decimal TotalGrams
        {
            get { return _TotalGrams; }
            set { _TotalGrams = value; }
        }

        private int _NrOfItems;

        public int NrOfItems
        {
            get { return _NrOfItems; }
            set { _NrOfItems = value; }
        }

        private string _ItemDescription;

        public string ItemDescription
        {
            get { return _ItemDescription; }
            set { _ItemDescription = value; }
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


        private bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }


        private string _Notes;

        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }

        private string _BillId;

        public string BillId
        {
            get { return _BillId; }
            set { _BillId = value; }
        }

        private DateTime _BillDt;

        public DateTime BillDt
        {
            get { return _BillDt; }
            set { _BillDt = value; }
        }
        

        private CustomerEntity _Customer;

        public CustomerEntity Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
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
                if (columnName == "NrOfItems")
                {
                    if (NrOfItems <= 0)
                        result = "Please enter  Nr of Items";
                }
                if (columnName == "TotalGrams")
                {
                    if (TotalGrams <= 0)
                        result = "Please enter Total Grams";
                }
                if (columnName == "LoanAmount")
                {
                    if (LoanAmount <= 0)
                        result = "Please enter Loan Amount";
                }
                if (columnName == "BillId")
                {
                    if (string.IsNullOrEmpty(BillId) || BillId.Length < 5)
                        result = "Please enter Bill Id";
                }
                if (columnName == "BillDt")
                {
                    if (BillDt == null || BillDt.Year ==1)
                        result = "Please Select a date";
                }
                if (columnName == "CustomerId")
                {
                    if (CustomerId == 0)
                        result = "Please select a Customer";
                }
                return result;
            }
        }
        
    }
}
