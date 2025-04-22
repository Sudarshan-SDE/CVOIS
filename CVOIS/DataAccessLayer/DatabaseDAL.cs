using CVOIS.Interfaces.ISuperAdmin;
using Microsoft.Data.SqlClient;

namespace CVOIS.DataAccessLayer
{
    public class DatabaseDAL: IDatabaseDAL
    {
        private readonly string _connectionString;

        public DatabaseDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int TestDatabaseConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    return 1; // Success
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection error: {ex.Message}");
                return 0; // Failed
            }
        }
    }
}
