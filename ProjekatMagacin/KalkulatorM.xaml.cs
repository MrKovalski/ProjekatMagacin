using MahApps.Metro.Controls;
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

namespace ProjekatMagacin
{
    /// <summary>
    /// Interaction logic for KalkulatorM.xaml
    /// </summary>
    public partial class KalkulatorM : MetroWindow
    {
        public KalkulatorM()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Izracunaj(object sender, RoutedEventArgs e)
        {
           double n, mar, prod, pdv;
            double prc = 0.15;
            double.TryParse(nabavna.Text, out n);

            if(cbx.Text == "15%")
            {
                prc = 0.15;
            }else if (cbx.Text == "16%")
            {
                prc = 0.16;
            }else if (cbx.Text == "17%")
            {
                prc = 0.17;
            } else if (cbx.Text == "18%")
            {
                prc = 0.18;
            }else if (cbx.Text == "19%")
            {
                prc = 0.19;
            }else if (cbx.Text == "20%")
            {
                prc = 0.2;
            }


            mar = n * prc;
            pdv = mar * 0.2;
            prod = n + mar + pdv;

            lbNabavna.Content = n + " din";
            lbPdv.Content = pdv + " din";
            lbProdajna.Content = prod + " din";

        }
    }
}
