using CVOIS.Interfaces.Admin;
using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.Models.Admin;
using CVOIS.Models.Connection;
using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;
using CVOIS.Models.SuperAdmin.DeleteAuditTrail;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;

namespace CVOIS.DataAccessLayer.SuperAdmin_DAL
{
    public partial class Ministry_DAL : IMinistry
    {
        private readonly string _connectionString;
        public Ministry_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }
        public async Task<List<MinistryModel>> Get_MinistriesAsync(string minCode, string manage)
        {
            List<MinistryModel> objList = new List<MinistryModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Get_List_Ministry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@minCode", minCode ?? string.Empty);
                        cmd.Parameters.AddWithValue("@manage", manage ?? string.Empty);

                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                MinistryModel obj = new MinistryModel
                                {
                                    Min_id = Convert.ToInt32(reader["Min_id"]),
                                    Mincode = reader["MinCode"].ToString(),
                                    Ministry_Name = reader["Ministry_Name"].ToString(),
                                    Min_Status = reader["Min_Status"].ToString(),
                                    Ministry_Type = reader["MinistryType"].ToString()
                                };
                                objList.Add(obj);
                            }
                        }
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
        public async Task<List<SelectListItem>> GetMinistryTypeAsync()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Get_MinistryType_List", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await con.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new SelectListItem
                                {
                                    Value = reader["ministryTypeCode"].ToString(),
                                    Text = reader["ministryType"].ToString()
                                });
                            }
                        }
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

            return list;
        }
        public async Task<int> InsertMinistryAsync(MinistryModel ministryModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    const string query = "usp_Ministry_Insert";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Ministry_Name", ministryModel.Ministry_Name);
                        cmd.Parameters.AddWithValue("@Min_Status", ministryModel.Min_Status);
                        cmd.Parameters.AddWithValue("@MinistryType", ministryModel.Ministry_Type);

                        cmd.Parameters.AddWithValue("@createdBy", ministryModel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", ministryModel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", ministryModel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", ministryModel.actionCategory ?? string.Empty);

                        await con.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null && result.ToString() == "Duplicate Entry")
                        {
                            return -1; // Duplicate
                        }

                        return 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while inserting ministry.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting ministry.", ex);
            }
        }
        public async Task<MinistryModel> Get_Ministry_By_IdAsync(int id)
        {
            MinistryModel ministry = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Get_Ministry_By_Id"; // Stored Procedure
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Min_id", id);

                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                ministry = new MinistryModel
                                {
                                    Min_id = Convert.ToInt32(reader["Min_id"]),
                                    Mincode = reader["MinCode"].ToString(),
                                    Ministry_Name = reader["Ministry_Name"].ToString(),
                                    Min_Status = reader["Min_Status"].ToString(),
                                    Ministry_Type = reader["MinistryType"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching ministry by ID: " + ex.Message);
            }
            return ministry;
        }
        public async Task<int> UpdateMinistryAsync(MinistryModel ministryModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Ministry_Update";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Min_id", ministryModel.Min_id);
                        cmd.Parameters.AddWithValue("@Mincode", ministryModel.Mincode);
                        cmd.Parameters.AddWithValue("@Ministry_Name", ministryModel.Ministry_Name);
                        cmd.Parameters.AddWithValue("@Min_Status", ministryModel.Min_Status);
                        cmd.Parameters.AddWithValue("@MinistryType", ministryModel.Ministry_Type);

                        cmd.Parameters.AddWithValue("@createdBy", ministryModel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", ministryModel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", ministryModel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", ministryModel.actionCategory ?? string.Empty);

                        await con.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null && result.ToString() == "Duplicate Entry")
                        {
                            return -1; // Duplicate
                        }

                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating ministry", ex);
            }
        }
        public async Task<int> DeleteMinistryAsync(int id, string createdBy, string createdByIP, string sessionID, string actionCategory)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Ministry_Delete";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Min_id", id);

                        cmd.Parameters.AddWithValue("@createdBy", createdBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", createdByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", sessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", actionCategory ?? string.Empty);

                        await con.OpenAsync();
                        result = await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            return result;
        }


        public async Task<List<MinistryAuditTrailModel>> Get_MinistryAuditTrailAsync()
        {
            List<MinistryAuditTrailModel> objList = new List<MinistryAuditTrailModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Get_AuditLog_Ministry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                MinistryAuditTrailModel obj = new MinistryAuditTrailModel
                                {
                                    auditID = Convert.ToInt32(reader["auditID"]),
                                    auditDetails = reader["auditDetails"].ToString(),
                                    createdBy = reader["createdBy"].ToString(),
                                    createdOn = reader["createdOn"].ToString(),
                                    createdByIP = reader["createdByIP"].ToString(),
                                    actionCategory = reader["actionCategory"].ToString(),
                                    sessionID = reader["sessionID"].ToString()
                                };
                                objList.Add(obj);
                            }
                        }
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

        public async Task<List<MinistryDeleteAuditTrailModel>> Get_MinistryDeleteAuditTrailAsync()
        {
            List<MinistryDeleteAuditTrailModel> objList = new List<MinistryDeleteAuditTrailModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("select*from Audit_Deleted_Ministry", con))
                    {
                        cmd.CommandType = CommandType.Text;

                        await con.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                MinistryDeleteAuditTrailModel obj = new MinistryDeleteAuditTrailModel
                                {
                                    AuditID = Convert.ToInt32(reader["AuditID"]),

                                    Min_Id = reader["Min_Id"].ToString(),
                                    Mincode = reader["Mincode"].ToString(),
                                    Ministry_Name = reader["Ministry_Name"].ToString(),
                                    Min_Status = reader["Min_Status"].ToString(),
                                    MinistryType = reader["MinistryType"].ToString(),

                                    createdBy = reader["createdBy"].ToString(),
                                    createdByIP = reader["createdByIP"].ToString(),

                                    SessionID = reader["SessionID"].ToString(),
                                    DeletedOn = reader["DeletedOn"].ToString()
                                };
                                objList.Add(obj);
                            }
                        }
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
