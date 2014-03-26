using AccountEntities;
using AccountsBAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AccountServe
{
    /// <summary>
    /// Interaction logic for BuisnessWindow.xaml
    /// </summary>
    public partial class LoanWindow : Window
    {
        public LoanWindow()
        {
            InitializeComponent();
            this.Title = "Buisness";
            this.MainMenu.SetSelectedMenu(this.Title);
            LoadmasterData();
            loanMasterDialog.SetParent(CustomerParent,this);
        }

        public void LoadmasterData()
        {
            dgLoanMaster.ItemsSource = LoanBAL.GetAll(1) ;
            var namelist = CustomerBAL.GetAllLookupValues("Name");           
            txtAutoName.ItemsSource = namelist;
            dgLoanMaster.SelectedIndex = 0;
            
        }

        void LoadDetailsData()
        {
            LoanMasterEntity loanMasterEntity = ((LoanMasterEntity)dgLoanMaster.SelectedItem);
            if (loanMasterEntity != null)
            dgLoanDetails.ItemsSource = LoanBAL.GetLoanDetails(loanMasterEntity.Id);

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            loanMasterDialog.ShowHandlerDialog(new LoanMasterEntity());
            LoadmasterData();
        }

        private void dgLoanMaster_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoanMasterEntity loanMasterEntity = (LoanMasterEntity)dgLoanMaster.SelectedItem;
            var res = loanMasterDialog.ShowHandlerDialog(loanMasterEntity);
            LoadmasterData();
        }

        

        private void dgLoanMaster_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDetailsData();
        }
    }
}
