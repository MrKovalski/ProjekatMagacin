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
using System.Data;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ProjekatMagacin
{
    /// <summary>
    /// Interaction logic for Pregled.xaml
    /// </summary>
    public partial class Pregled : Window
    {
        
        private MySqlConnection connection = null;

        public MySqlConnection Connection
        {
            get { return connection; }
        }

        public PersonClass Person = new PersonClass();

        public Pregled()
        {
            InitializeComponent();
            this.DataContext = this;
            // jako bitno

            this.MemorisiDatotekuResursa();
            this.UcitajDatotekuResursa();


        }
        // Create new DataTable and DataSource objects.
        private DataTable table = new DataTable();

        // Declare DataColumn and DataRow variables.
        private DataColumn column;
        private DataRow row;
        private DataView view;



        private void dataGrid1_Loaded(object sender, RoutedEventArgs e)
        {
            // za bazu ,m
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "magacin_db";



            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Ime";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Prezime";
            table.Columns.Add(column);

            // Create new DataRow objects and add to DataTable.    





            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "SELECT col0,col1 FROM korisnici";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ime = reader.GetString(0);
                    string prezime = reader.GetString(1);
                    //Console.WriteLine(ime + "," + prezime);

                    row = table.NewRow();
                    row["Ime"] = ime;
                    row["Prezime"] = prezime;
                    table.Rows.Add(row);


                }
                dbCon.Close();

                // Create a DataView using the DataTable.
                view = new DataView(table);

                // Set a DataGrid control's DataSource to the DataView.
                gridPeople.DataContext = view;


            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
            Refresh();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var cellInfo = gridPeople.SelectedCells[0];

            var content = cellInfo.Column.GetCellContent(cellInfo.Item);

            var row = (DataRowView)content.DataContext;

            //ItemArray returns an object array with single element
            object[] obj = row.Row.ItemArray;


            //PersonClass ime = (PersonClass)gridPeople.SelectedItem;
            //Console.WriteLine(obj[0].ToString());


            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "korisnici";


            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "DELETE FROM korisnici WHERE Ime ='" + obj[0].ToString() + "' and Prezime = '" + obj[1].ToString() + "';";

                Console.WriteLine(query);

                var cmd = new MySqlCommand(query, dbCon.Connection);
                dbCon.Connection.Open();
                MySqlDataReader MyReader2;
                MyReader2 = cmd.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                MessageBox.Show("Deleted Data");



                dbCon.Close();


            }

            Refresh();
        }

        public void Refresh()
        {
            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "korisnici";

            table = new DataTable();


            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Ime";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Prezime";
            table.Columns.Add(column);

            // Create new DataRow objects and add to DataTable.    


            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "SELECT Ime,Prezime FROM korisnici";
                var cmd = new MySqlCommand(query, dbCon.Connection);

                dbCon.Connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string ime = reader.GetString(0);
                    string prezime = reader.GetString(1);
                    //Console.WriteLine(ime + "," + prezime);

                    row = table.NewRow();
                    row["Ime"] = ime;
                    row["Prezime"] = prezime;
                    table.Rows.Add(row);


                }
                dbCon.Close();

                // Create a DataView using the DataTable.
                view = new DataView(table);

                // Set a DataGrid control's DataSource to the DataView.
                gridPeople.DataContext = view;
            }




        }

        public class DataObject
        {
            public string ime { get; set; }
            public string prezime { get; set; }
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var cellInfo = gridPeople.SelectedCells[0];

            var content = cellInfo.Column.GetCellContent(cellInfo.Item);

            var row = (DataRowView)content.DataContext;

            //ItemArray returns an object array with single element
            object[] obj = row.Row.ItemArray;

            UpdateWindow updWindow = new UpdateWindow(obj[0].ToString(), obj[1].ToString());
            updWindow.ShowDialog();

            Refresh();

        }

        private void gridPeople_Initialized(object sender, EventArgs e)
        {

        }

        // SERIJALIZACIJA/DESERIJALIZACIJA IZ DATOTEKE
        private readonly string _datoteka = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datoteka.bin");
        List<PersonClass> lista = new List<PersonClass>();

        private void UcitajDatotekuResursa()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            // TREBA IF ELSE, PROVERI DA LI RADI BEZ LISTA != NULL




            try
            {
                // obsCol ima ugradjen konstuktor samo ubacim listu u njega
                stream = File.Open(_datoteka, FileMode.OpenOrCreate);
                lista = (List<PersonClass>)formatter.Deserialize(stream);

                Console.WriteLine(lista);

                foreach (PersonClass item in lista)
                {
                    Console.WriteLine(item.Ime);
                }

            }
            catch
            {
                // 
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }


        }


        private void MemorisiDatotekuResursa()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            PersonClass osoba = new PersonClass();
            osoba.Ime = "ivan";
            osoba.Prezime = "jancic";

            lista.Add(osoba);
            lista.Add(new PersonClass());

            try
            {

                //lista ima ugradjen konstuktor za obsCol

                stream = File.Open(_datoteka, FileMode.OpenOrCreate);
                formatter.Serialize(stream, lista);
            }
            catch
            {
                // 
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

    }
}
