using Data;
using System;
using System.Windows;
using System.Windows.Controls;

namespace dz_12_05
{
    public partial class MainWindow : Window
    {
        DBManager dBManager;

        public MainWindow()
        {
            InitializeComponent();
            dBManager = new DBManager();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Connect
        {
            if (!string.IsNullOrEmpty(dBManager.GetConnectionString))
            {
                try
                {
                    dBManager.Connect();
                    MessageBox.Show("Connected successfully");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to connect: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Connection string is null or empty");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Disconnect
        {
            dBManager.GetConnectionString = null;
            dataGrid.ItemsSource = null;
            MessageBox.Show("Disconnected");
        }

        private void TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            dBManager.GetConnectionString = TBString.Text;
        }

        private void LoadData()
        {
            if (!string.IsNullOrEmpty(dBManager.GetConnectionString))
            {
                string query = "SELECT p.ProductName, pt.ProductTypeName, s.SupplierName, i.Quantity, i.Cost, i.SupplyDate " +
                               "FROM dbo.Inventory i " + // Додайте схему, наприклад, dbo
                               "JOIN dbo.Products p ON i.ProductID = p.ProductID " +
                               "JOIN dbo.ProductTypes pt ON p.ProductTypeID = pt.ProductTypeID " +
                               "JOIN dbo.Suppliers s ON p.SupplierID = s.SupplierID";
                var dataTable = dBManager.ExecuteQuery(query);
                dataGrid.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
