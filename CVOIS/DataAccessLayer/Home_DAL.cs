using CVOIS.Interfaces;
using CVOIS.Models.Connection;
using CVOIS.Models.Home;
using CVOIS.Models.SuperAdmin;
using CVOIS.Models.Viewers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CVOIS.DataAccessLayer
{
    public class Home_DAL : IHome
    {
        private readonly string _connectionString;
        public Home_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }

        public int Forgotpassword(ForgotPasswordModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    const string query = "usp_ForgetPassword";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        con.Open();
                        var result = cmd.ExecuteScalar();
                        return 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while forgetting password.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while forgetting password.", ex);
            }
        }

    }
}
