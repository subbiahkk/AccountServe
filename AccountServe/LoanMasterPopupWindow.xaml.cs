using AccountEntities;
using AccountsBAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for LoanDetailsWindow.xaml
    /// </summary>
    public partial class LoanMasterPopupWindow : UserControl
    {
        public LoanMasterPopupWindow()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
           // BindLoanMaster();
            dgLoanDetails.RowEditEnding += dgLoanDetails_RowEditEnding;
            dgLoanDetails.CellEditEnding += dgLoanDetails_CellEditEnding;
            
        }

        void dgLoanDetails_CellEditEnding(object sender, Microsoft.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            
        }

        void dgLoanDetails_RowEditEnding(object sender, Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            
        }

        LoanMasterEntity _LoanMasterEntity = new LoanMasterEntity();
        private int _errors = 0;
        private int _errorsDetails = 0;
        private bool _hideRequest = false;
        private bool _result = false;
        private UIElement _parent;
        private ObservableCollection<LoanDetailsEntity> LoanDetails;
        private LoanWindow _LoanWindow;


        void LoadCustomerDetails()
        {
            cbCustomer.ItemsSource = CustomerBAL.GetAllCustomers();
            cbCustomer.SelectedValuePath = "Key";
            cbCustomer.DisplayMemberPath = "Value";
            
        }

        public void LoadDetailsData(int loanMasterID)
        {
            if (loanMasterID != 0)
            { LoanDetails = LoanBAL.GetLoanDetails(loanMasterID); }
            else
            {
                LoanDetails = new ObservableCollection<LoanDetailsEntity>();
            }
                
            dgLoanDetails.ItemsSource = LoanDetails;

        }

        void BindLoanMaster()
        {
            grid_LoanData.DataContext = _LoanMasterEntity;
          
            
            if (_LoanMasterEntity != null)
            {
                LoadCustomerDetails();
                cbCustomer.SelectedValue = _LoanMasterEntity.Customer.Id;
                dtBill.DisplayDate = _LoanMasterEntity.BillDt;
                dtBill.Text = _LoanMasterEntity.BillDt.ToString();
                chkActive.IsChecked = _LoanMasterEntity.IsActive;
                // txtBillNr.Text = _LoanMasterEntity.BillId.ToString();
                txtTotalGrams.Text = _LoanMasterEntity.TotalGrams != 0 ? _LoanMasterEntity.TotalGrams.ToString() : "";
                txtLoanAmount.Text = _LoanMasterEntity.LoanAmount != 0 ? _LoanMasterEntity.LoanAmount.ToString() : "";
                txtNrofItems.Text = _LoanMasterEntity.NrOfItems != 0 ? _LoanMasterEntity.NrOfItems.ToString() : "";
                txtNotes.Selection.Text = _LoanMasterEntity.Notes != null ? _LoanMasterEntity.Notes : "";
               
            }
            else
            {
                dtBill.DisplayDate = DateTime.Now;
                dtBill.Text = DateTime.Now.ToString();
            }
        }

        void SaveLoanMaster()
        {
            // try
            {
                if (_LoanMasterEntity.Id == 0)
                {
                    _LoanMasterEntity = new AccountEntities.LoanMasterEntity();
                }
                _LoanMasterEntity.IsActive = (bool)chkActive.IsChecked;
                _LoanMasterEntity.CustomerId = Convert.ToInt32(cbCustomer.SelectedValue);
                _LoanMasterEntity.TotalGrams = Convert.ToDecimal(txtTotalGrams.Text);
                _LoanMasterEntity.LoanAmount = Convert.ToInt32(txtLoanAmount.Text);
                _LoanMasterEntity.NrOfItems = Convert.ToInt32(txtNrofItems.Text);
                _LoanMasterEntity.BillDt = dtBill.DisplayDate;
                _LoanMasterEntity.BillId = txtBillNr.Text;
                
                var textRange = new TextRange(txtNotes.Document.ContentStart, txtNotes.Document.ContentEnd);
                _LoanMasterEntity.Notes = textRange.Text;
                LoanBAL.Save(_LoanMasterEntity);
                HideHandlerDialog();
            }
            //  catch(Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //SaveCustomer();
            SaveLoanMaster();
            //HideHandlerDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            HideHandlerDialog();
        }


        public bool ShowHandlerDialog(LoanMasterEntity loanMasterEntity)
        {

            Visibility = Visibility.Visible;
            this._LoanMasterEntity = loanMasterEntity;
            BindLoanMaster();
            LoadDetailsData(_LoanMasterEntity.Id);
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

        public void SetParent(UIElement parent, LoanWindow loanWindow)
        {
            _parent = parent;
            _LoanWindow = loanWindow;
        }

        private void HideHandlerDialog()
        {
            _hideRequest = true;
            Visibility = Visibility.Hidden;
            _parent.IsEnabled = true;
            _LoanWindow.LoadmasterData();
        }

        #region Validation Events
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
           // e.CanExecute = cbCustomer.SelectedValue != null;
            e.CanExecute = _errors == 0;
            e.Handled = true;
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Employee emp = grid_EmployeeData.DataContext as Employee;            
            //_LoanMasterEntity = new LoanMasterEntity();
            //grid_LoanData.DataContext = _LoanMasterEntity;
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

        private void btnAddDetails_Click(object sender, RoutedEventArgs e)
        {
            if (LoanDetails.Last().ItemDescription != null && LoanDetails.Last().ItemGrams > 0)
            {
                LoanDetails.Add(new LoanDetailsEntity(_LoanMasterEntity.Id));
            }
           
         
        }

        private void btnDeleteDetails_Click(object sender, RoutedEventArgs e)
        {
            if (dgLoanDetails.SelectedItem != null)
            {
                LoanDetailsEntity loaDetailEntity = (LoanDetailsEntity)dgLoanDetails.SelectedItem;
                if (loaDetailEntity.Id == 0)
                { LoanDetails.Remove((LoanDetailsEntity)dgLoanDetails.SelectedItem); }
                else
                {
                    if (MessageBox.Show("Do you want to remove this item?",
  "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        LoanBAL.RemoveLoanDetails(loaDetailEntity);
                        LoadDetailsData(loaDetailEntity.LoanMaster.Id);
                    }
                    else
                    {
                        // Do not close the window
                    } 
                }
                
            }
        }

        private void btnSaveDetails_Click(object sender, RoutedEventArgs e)
        {
            LoanBAL.SaveLoanDetails(LoanDetails);
            HideHandlerDialog();
        }

       
    }


    //public static class CommandLibrary
    //{
    //    private static RoutedUICommand add = new RoutedUICommand("Add", "Add", typeof(CommandLibrary));

    //    private static RoutedUICommand remove = new RoutedUICommand("Remove", "Remove", typeof(CommandLibrary));

    //    public static RoutedUICommand AddListItem
    //    {
    //        get { return add; }
    //    }

    //    public static RoutedUICommand RemoveListItem
    //    {
    //        get { return remove; }
    //    }

    //}
}
