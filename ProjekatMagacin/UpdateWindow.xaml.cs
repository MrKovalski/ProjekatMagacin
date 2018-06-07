using MySql.Data.MySqlClient;
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
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private string staroIme;
        private string staroPrezime;

        public UpdateWindow(String ime, String prezime)
        {


            InitializeComponent();
            textBoxIme.Text = ime;
            textBoxPrezime.Text = prezime;

            this.staroIme = ime;
            this.staroPrezime = prezime;

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "magacin_db";


            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "UPDATE korisnici" +
                               " SET Ime = '" + textBoxIme.Text + "', Prezime= '" + textBoxPrezime.Text + "'" +
                               " WHERE Ime ='" + this.staroIme + "' and Prezime = '" + this.staroPrezime + "';";

                Console.WriteLine(query);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                dbCon.Connection.Open();
                MySqlDataReader MyReader2;
                MyReader2 = cmd.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                MessageBox.Show("Update Data");



                dbCon.Close();

                this.Close();

            }
        }
    }
}
