using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WinFormsApp1.Helper
{
    public static class DbHelper
    {
        private static string connectionString;

        static DbHelper()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}