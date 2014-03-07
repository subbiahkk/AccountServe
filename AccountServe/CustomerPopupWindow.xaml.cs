using AccountEntities;
using AccountsBAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AccountServe
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class CustomerPopupWindow :UserControl
    {

        public CustomerPopupWindow()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
            BindCustomer();
        }

        CustomerEntity _customer = new CustomerEntity();
        private int _errors = 0;
        private bool _hideRequest = false;
        private bool _result = false;
        private UIElement _parent;

        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveCustomer();
            HideHandlerDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            HideHandlerDialog();
        }


        #region Customer Methods
        void BindCustomer()
        {
            grid_CustomerData.DataContext = _customer;
            if (_customer != null)
            {
                chkActive.IsChecked = _customer.IsActive;
                txtPhone.Text = _customer.Phone != 0 ? _customer.Phone.ToString() : "";
                txtMobile.Text = _customer.Mobile != 0 ? _customer.Mobile.ToString() : "";
                txtNotes.Selection.Text = _customer.Notes != null ? _customer.Notes : "";
            }
        }

        void SaveCustomer()
        {
            // try
            {                
                if (_customer.Id == 0)
                {
                    _customer = new AccountEntities.CustomerEntity();
                }
                _customer.IsActive = (bool)chkActive.IsChecked;
                _customer.Name = txtName.Text;
                _customer.Alias = txtAlias.Text;
                _customer.Mobile = Convert.ToInt64(txtMobile.Text);
                _customer.Phone = Convert.ToInt64(txtPhone.Text);
                _customer.Email = txtEmail.Text;
                _customer.State = txtState.Text;
                _customer.City = txtCity.Text;                
                _customer.Address1 = txtAddress1.Text;
                _customer.Address2 = txtAddress2.Text;
                var textRange = new TextRange(txtNotes.Document.ContentStart, txtNotes.Document.ContentEnd);
                _customer.Notes = textRange.Text;
                CustomerBAL.Save(_customer);
                HideHandlerDialog();
            }
            //  catch(Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }


        void ShowCustomerDetails(CustomerEntity customer)
        {
            if (customer != null)
            {
                _customer = customer;
            }
            //txtName.Text = customer.Name;
            //txtAlias.Text = customer.Alias;
            //txtPhone.Text = customer.Phone.ToString();
            //txtMobile.Text = customer.Mobile.ToString();
            //txtAddress1.Text = customer.Address1;
            //txtAddress2.Text = customer.Address2;
            //txtCity.Text = customer.City;
            //txtState.Text = customer.State;
            //txtNotes.Selection.Text = customer.Notes != null ? customer.Notes : "";
            //txtEmail.Text = customer.Email;
            //chkActive.IsChecked = customer.IsActive;
        }
        #endregion


        #region Modal Methods and events
        public void SetParent(UIElement parent)
        {
            _parent = parent;
        }

        public bool ShowHandlerDialog(CustomerEntity customer)
        {

            Visibility = Visibility.Visible;
            this._customer = customer;
            BindCustomer();
            _parent.IsEnabled = false;

            _hideRequest = false;
            while (!_hideRequest)
            {
                // HACK: Stop the thread if the application is about to close
                if (this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished)
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                this.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return _result;
        }

        private void HideHandlerDialog()
        {
            _hideRequest = true;
            Visibility = Visibility.Hidden;
            _parent.IsEnabled = true;
        }
        #endregion

        #region Validation Events
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _errors == 0;
            e.Handled = true;
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Employee emp = grid_EmployeeData.DataContext as Employee;            
            _customer = new CustomerEntity();
            grid_CustomerData.DataContext = _customer;
            e.Handled = true;
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _errors++;
            else
                _errors--;
        }
        #endregion

    }
}
