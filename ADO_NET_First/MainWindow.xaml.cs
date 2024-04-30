using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ADO_NET_First
{
    public partial class MainWindow : Window
    {
        private string connectionString;
        private SqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            connectionString = tbConnectionString.Text;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                tbStatus.Text = "Connection successful!";
                btnConnect.Visibility = Visibility.Collapsed;
                btnDisconnect.Visibility = Visibility.Visible;

                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the database: " + ex.Message);
            }
        }

        private void BtnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                connection.Dispose();

                tbStatus.Text = "Disconnected from the database.";
                btnConnect.Visibility = Visibility.Visible;
                btnDisconnect.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error disconnecting from the database: " + ex.Message);
            }
        }

        private void DisplayData()
        {
            try
            {
                string query = "SELECT * FROM Students";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgData.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying data: " + ex.Message);
            }
        }
    }
}
