using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AccountsBAL;
using AccountsBAL.Common;
using System.Windows.Threading;
using System.IO;
using System.Reflection;

namespace AccountServe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        //Custom main method
        [STAThread]
        public static void Main()
        {
            // Create new instance of application subclass
            App app = new App();

            // Code to register events and set properties that were
            // defined in XAML in the application definition
            app.InitializeComponent();
            string connString = System.Configuration.ConfigurationSettings.AppSettings["connString"];
            Common.SetConnectionString(connString);
            // Start running the application
            app.Run();
        }

        public App()
            : base()
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }


        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            WriteToErrorLog(errorMessage, e.Exception.StackTrace, "Error");
            e.Handled = true;
        }

        public void WriteToErrorLog(string msg, string stkTrace, string title)
        {

            string StartupPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!(System.IO.Directory.Exists(StartupPath + "\\Errors\\")))
            {
                System.IO.Directory.CreateDirectory(StartupPath + "\\Errors\\");
            }
            FileStream fs = new FileStream(StartupPath + "\\Errors\\errlog.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter s = new StreamWriter(fs);
            s.Close();
            fs.Close();
            FileStream fs1 = new FileStream(StartupPath + "\\Errors\\errlog.txt", FileMode.Append, FileAccess.Write);
            StreamWriter s1 = new StreamWriter(fs1);
            s1.Write("Title: " + title + System.Environment.NewLine);
            s1.Write("Message: " + msg + System.Environment.NewLine);
            s1.Write("StackTrace: " + stkTrace + System.Environment.NewLine);
            s1.Write("Date/Time: " + DateTime.Now.ToString() + System.Environment.NewLine);
            s1.Write
            ("============================================" + System.Environment.NewLine);
            s1.Close();
            fs1.Close();
        }
    }


}
