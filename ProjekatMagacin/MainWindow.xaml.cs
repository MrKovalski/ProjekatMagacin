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
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace ProjekatMagacin
{
   
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void opnUnos(object sender, RoutedEventArgs e)
        {
            Unos unos = new Unos();
            unos.Show();
        }

        private void opnPomoc(object sender, RoutedEventArgs e)
        {
            Pomoc pomoc = new Pomoc();
            pomoc.Show();
        }

        private void opnAbout(object sender, RoutedEventArgs e)
        {
            About abt = new About();
            abt.Show();
        }

       






    }
}
