using CVOIS.Interfaces;
using CVOIS.Models.Connection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CVOIS.DataAccessLayer.HomeDAL
{
    public class User_DAL : IUser
    {
        private readonly string _connectionString;
        public User_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }

        public bool CheckOldPassword(string userId, string oldPassword)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "usp_CheckOldPassword";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Password", oldPassword);

                con.Open();
                object result = cmd.ExecuteScalar();
                int count = result != null ? Convert.ToInt32(result) : 0;

                Console.WriteLine($"CheckOldPassword count: {count}");

                return count > 0;
            }
        }


        public bool UpdateUserPassword(string userId, string newPassword)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "usp_UpdateUserPassword";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Password", newPassword);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine("Rows Affected: " + rowsAffected);
                return rowsAffected > 0;
            }
        }

    }
}
