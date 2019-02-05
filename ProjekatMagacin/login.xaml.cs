using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ProjekatMagacin
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : MetroWindow
    {
        public login()
        {
            InitializeComponent();
            txtU.Focus();
        }

        private void closeThis(object sender, RoutedEventArgs a)
        {
            this.Close();
        }



        private void opnMain(object sender, RoutedEventArgs e)
        {
            if (txtU.Text == "admin" && txtP.Password == "admin")
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MainWindow main = new MainWindow();
                main.Show();
                //this.ShowMessageAsync("Upps", "Uneli ste pogrešne podatke.");
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Test for Enter key.
            if (e.Key == Key.Enter)
            {
                if (txtU.Text == "admin" && txtP.Password == "admin")
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    this.ShowMessageAsync("Upps", "Uneli ste pogrešne podatke.");
                }
            }
        }
    }
}
