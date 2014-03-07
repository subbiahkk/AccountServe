using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountEntities
{
    public class CustomerEntity : IDataErrorInfo
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Alias { get; set; }
        //public Nullable<long> Phone { get; set; }
        //public Nullable<long> Mobile { get; set; }
        //public string Address1 { get; set; }
        //public string Address2 { get; set; }
        //public string Email { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public System.DateTime CreatedDt { get; set; }
        //public System.DateTime ModifiedDt { get; set; }
        //public Nullable<bool> IsActive { get; set; }
        //public string Notes { get; set; }

        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set 
            { 
                _Name = value;                
            
            }
        }

        private string _Alias;

        public string Alias
        {
            get { return _Alias; }
            set { _Alias = value; }
        }


        private long _Mobile;

        public long Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        


        private long _Phone;

        public long Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        


        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        


        private string _Address1;

        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        

        private string _Address2;

        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        

        private string _City;

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        private string _State;

        public string State
        {
            get { return _State; }
            set { _State = value; }
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



        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name) || Name.Length < 3)
                        result = "Please enter a Name";
                }
                if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City) || City.Length < 0)
                        result = "Please enter a Position";
                }
                if (columnName == "Phone")
                {
                    if (Phone <= 999999999 || Phone >= 10000000000)
                        result = "Please enter a valid Phone Number";
                }
                if (columnName == "Mobile")
                {
                    if (Mobile <= 999999999 || Mobile >= 10000000000)
                        result = "Please enter a valid Mobile Number";
                }
                return result;
            }
        }
    }
}
