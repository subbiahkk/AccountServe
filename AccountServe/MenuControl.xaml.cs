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
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            InitializeComponent();
            
        }

        public void SetSelectedMenu(string title)
        {
           // Window window = Window.GetWindow(this);
            switch (title)
            {
                case "Customers":
                    MenuItem mi = ((MenuItem)this.menu1.Items.GetItemAt(1));
                    mi.IsChecked = true;
                    break;
                case "Buisness":
                    MenuItem miBU = ((MenuItem)this.menu1.Items.GetItemAt(2));
                    miBU.IsChecked = true;
                    break;

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Window window = Window.GetWindow(this);
            switch (menuItem.Header.ToString())
            {
                case "Home":
                    break;
                case "Customer":
                   // CloseAllWindows();
                   
          
                    CustomerWindow mainWindow = new CustomerWindow();
                   
                    mainWindow.Show();
                    window.Close();
                    break;
                case "Buisness":
                    
                    LoanWindow buisnessWindow = new LoanWindow();
                   
                    buisnessWindow.Show();
                    window.Close();
                    
                    break;
                case "Logout":
                   // CloseAllWindows();
                    Application curApp = Application.Current;
                    curApp.Shutdown();
                    break;
            }
        }

        private void CloseAllWindows()
        {
           Window window =  Window.GetWindow(this);
           window.Hide();

            //for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 1; intCounter--)
            //    App.Current.Windows[intCounter].Close();
        }
    }
}
