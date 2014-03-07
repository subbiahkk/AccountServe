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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using AccountsBAL;
using AccountEntities;
using AccountsBAL.Common;
using System.Text.RegularExpressions;
using System.Globalization;
namespace AccountServe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        AccountEntities.CustomerEntity customer;
        public CustomerWindow()
        {
            InitializeComponent();
            this.Title = "Customers";
           this.MainMenu.SetSelectedMenu(this.Title);
            LoadData();
            customerDialog.SetParent(CustomerParent);
            //btnsave.IsEnabled = false;
           // LoadData();
        }
        

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // Class1.GetCustomers();

            LoadData();
            // dgCustomer.ItemsSource = CustomerDAL.GetAll();
        }     


        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            customer = (CustomerEntity)dgCustomer.SelectedItem;
            var res = customerDialog.ShowHandlerDialog(customer);
            LoadData();
            //EnableControls(true);
        }

     

        void LoadData()
        {           
            dgCustomer.ItemsSource = CustomerBAL.GetAll(txtAutoName.Text.Trim().Length > 0 ? txtAutoName.Text : null, txtAutoCity.Text.Trim().Length > 0 ? txtAutoCity.Text : null); ;
            var namelist = CustomerBAL.GetAllLookupValues("Name");
            var citylist = CustomerBAL.GetAllLookupValues("City");
            txtAutoName.ItemsSource = namelist;
            txtAutoCity.ItemsSource = citylist;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {           
            customerDialog.ShowHandlerDialog(new CustomerEntity());
            LoadData();
        }

        private void dgCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            customer = (CustomerEntity)dgCustomer.SelectedItem;
            var res = customerDialog.ShowHandlerDialog(customer);
            LoadData();
        }

       

       

        
    }

   
    
}
