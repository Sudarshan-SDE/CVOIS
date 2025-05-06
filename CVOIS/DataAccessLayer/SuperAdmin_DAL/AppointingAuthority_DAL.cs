using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.Models.Connection;
using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;
using CVOIS.Models.SuperAdmin.DeleteAuditTrail;
using CVOIS.Models.Viewers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CVOIS.DataAccessLayer.SuperAdmin_DAL
{
    public class AppointingAuthority_DAL : IAppointingAuthority
    {
        private readonly string _connectionString;
        public AppointingAuthority_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }


        public List<AppointingAuthorityModel> GetAppointingAuthorityList()
        {
            List<AppointingAuthorityModel> objList = new List<AppointingAuthorityModel>();
            try
            {
                string query = "usp_Get_List_AppointingAuthority";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        AppointingAuthorityModel obj = new AppointingAuthorityModel
                        {
                            row_num = Convert.ToInt32(row["row_num"]),
                            sno = Convert.ToInt32(row["sno"]),
                            AppointingAuthority_Code = row["AppointingAuthority_Code"].ToString(),
                            APPOINTING_AUTHORITY = row["APPOINTING_AUTHORITY"].ToString()
                        };
                        objList.Add(obj);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            return objList;
        }

        public int InsertAppointingAuthority(AppointingAuthorityModel appointingauthorityModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    const string query = "usp_MasterAppointingAuthority_Insert";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AppointingAuthority_Code", appointingauthorityModel.AppointingAuthority_Code);
                        cmd.Parameters.AddWithValue("@APPOINTING_AUTHORITY", appointingauthorityModel.APPOINTING_AUTHORITY);

                        cmd.Parameters.AddWithValue("@createdBy", appointingauthorityModel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", appointingauthorityModel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", appointingauthorityModel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", appointingauthorityModel.actionCategory ?? string.Empty);
                        con.Open();
                        var result = cmd.ExecuteScalar();

                        if (result != null && result.ToString() == "Duplicate Entry")
                        {
                            return -1; // Indicate a duplicate entry
                        }
                        return 1;
                        //return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while inserting APPOINTING_AUTHORITY.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting APPOINTING_AUTHORITY.", ex);
            }
        }

        public AppointingAuthorityModel Get_AppointingAuthority_By_Id(int id)
        {
            AppointingAuthorityModel app_auth = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Get_AppointingAuthority_By_Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sno", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        app_auth = new AppointingAuthorityModel
                        {
                            sno = Convert.ToInt32(reader["sno"]),
                            AppointingAuthority_Code = reader["AppointingAuthority_Code"].ToString(),
                            APPOINTING_AUTHORITY = reader["APPOINTING_AUTHORITY"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching Appointing Authority by ID: " + ex.Message);
            }
            return app_auth;
        }

        public int UpdateAppointingAuthority(AppointingAuthorityModel appointingauthorityModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_MasterAppointingAuthority_Update";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sno", appointingauthorityModel.sno);
                        cmd.Parameters.AddWithValue("@AppointingAuthority_Code", appointingauthorityModel.AppointingAuthority_Code);
                        cmd.Parameters.AddWithValue("@APPOINTING_AUTHORITY", appointingauthorityModel.APPOINTING_AUTHORITY);

                        cmd.Parameters.AddWithValue("@createdBy", appointingauthorityModel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", appointingauthorityModel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", appointingauthorityModel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", appointingauthorityModel.actionCategory ?? string.Empty);
                        con.Open();
                        var result = cmd.ExecuteScalar();

                        if (result != null && result.ToString() == "Duplicate Entry")
                        {
                            return -1; // Indicate a duplicate entry
                        }
                        return 1;
                        //return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating AppointingAuthority", ex);
            }
        }

        public int DeleteAppointingAuthority(int id, string createdBy, string createdByIP, string sessionID, string actionCategory)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_MasterAppointingAuthority_Delete";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sno", id);

                        cmd.Parameters.AddWithValue("@createdBy", createdBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", createdByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", sessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", actionCategory ?? string.Empty);
                        con.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            return result;
        }

        public List<AppointingAuthorityAuditTrailModel> Get_AppointingAuthorityAuditTrail()
        {
            List<AppointingAuthorityAuditTrailModel> objList = new List<AppointingAuthorityAuditTrailModel>();
            try
            {
                string query = "usp_Get_AuditLog_AppointingAuthority";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        AppointingAuthorityAuditTrailModel obj = new AppointingAuthorityAuditTrailModel
                        {
                            auditID = Convert.ToInt32(row["auditID"]),
                            auditDetails = row["auditDetails"].ToString(),
                            createdBy = row["createdBy"].ToString(),
                            createdOn = row["createdOn"].ToString(),
                            createdByIP = row["createdByIP"].ToString(),
                            sessionID = row["sessionID"].ToString(),
                            actionCategory = row["actionCategory"].ToString()
                        };
                        objList.Add(obj);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            return objList;
        }

        public List<AppointingAuthorityDeleteAuditTrailModel> Get_AppointingAuthorityDeleteAuditTrail()
        {
            List<AppointingAuthorityDeleteAuditTrailModel> objList = new List<AppointingAuthorityDeleteAuditTrailModel>();
            try
            {
                string query = "select*from Audit_Deleted_AppointingAuthority";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.Text;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        AppointingAuthorityDeleteAuditTrailModel obj = new AppointingAuthorityDeleteAuditTrailModel
                        {
                            AuditID = Convert.ToInt32(row["AuditID"]),
                            AppointingAuthority_Id = Convert.ToInt32(row["AppointingAuthority_Id"]),
                            AppointingAuthority_Code = row["AppointingAuthority_Code"].ToString(),
                            AppointingAuthority_Name = row["AppointingAuthority_Name"].ToString(),
                            createdBy = row["createdBy"].ToString(),
                            createdByIP = row["createdByIP"].ToString(),
                            SessionID = row["SessionID"].ToString(),
                            DeletedOn = row["DeletedOn"].ToString()
                        };
                        objList.Add(obj);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
            return objList;
        }
    }
}
