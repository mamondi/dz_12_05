using Microsoft.Data.SqlClient;
using System.Data;

namespace Data
{
    public class DBManager
    {
        public string? GetConnectionString { get; set; } = null;

        public DBManager() { }

        public DBManager(string? connectionString = null)
        {
            GetConnectionString = connectionString;
        }

        public void Connect()
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString))
            {
                connection.Open();
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
}
