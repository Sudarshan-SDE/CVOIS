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
    public class State_DAL : IState
    {
        private readonly string _connectionString;
        public State_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }

        public async Task<List<StateModel>> Get_StateListAsync()
        {
            List<StateModel> objList = new List<StateModel>();
            try
            {
                string query = "usp_Get_List_State";
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            StateModel obj = new StateModel
                            {
                                state_id = reader["state_id"].ToString(),
                                state_name = reader["state_name"].ToString()
                            };
                            objList.Add(obj);
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
        public async Task<int> InsertStateAsync(StateModel statemodel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    const string query = "usp_State_Insert";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@state_id", statemodel.state_id);
                        cmd.Parameters.AddWithValue("@state_name", statemodel.state_name);
                        cmd.Parameters.AddWithValue("@createdBy", statemodel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", statemodel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", statemodel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", statemodel.actionCategory?? string.Empty);

                        await con.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null && result.ToString() == "Duplicate Entry")
                        {
                            return -1; // Indicate a duplicate entry
                        }

                        return 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while inserting State Model.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting State Model.", ex);
            }
        }
        public async Task<StateModel> Get_State_By_IdAsync(string id)
        {
            StateModel state = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Get_State_By_Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@state_id", id);
                        await con.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                state = new StateModel
                                {
                                    state_id = reader["state_id"].ToString(),
                                    state_name = reader["state_name"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching State by ID: " + ex.Message);
            }
            return state;
        }
        public async Task<int> UpdateStateAsync(StateModel statemodel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_State_Update";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@state_id", statemodel.state_id);
                        cmd.Parameters.AddWithValue("@state_name", statemodel.state_name);
                        cmd.Parameters.AddWithValue("@createdBy", statemodel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", statemodel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", statemodel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", statemodel.actionCategory ?? string.Empty);
                        await con.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null && result.ToString() == "Duplicate Entry")
                        {
                            return -1; // Indicate a duplicate entry
                        }

                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating State", ex);
            }
        }
        public async Task<int> DeleteStateAsync(string id, string createdBy, string createdByIP, string sessionID, string actionCategory)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_State_Delete";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@state_id", id);
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
        public async Task<List<StateAuditTrailModel>> Get_StateAuditTrailAsync()
        {
            List<StateAuditTrailModel> objList = new List<StateAuditTrailModel>();
            try
            {
                string query = "usp_Get_AuditLog_State";
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await con.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            StateAuditTrailModel obj = new StateAuditTrailModel
                            {
                                auditID = Convert.ToInt32(reader["auditID"]),
                                auditDetails = reader["auditDetails"].ToString(),
                                createdBy = reader["createdBy"].ToString(),
                                createdOn = reader["createdOn"].ToString(),
                                createdByIP = reader["createdByIP"].ToString(),
                                sessionID = reader["sessionID"].ToString(),
                                actionCategory = reader["actionCategory"].ToString()
                            };
                            objList.Add(obj);
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
        public async Task<List<StateDeleteAuditTrailModel>> Get_StateDeleteAuditTrailAsync()
        {
            List<StateDeleteAuditTrailModel> objList = new List<StateDeleteAuditTrailModel>();
            try
            {
                string query = "select*from  Audit_Deleted_State";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.Text;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        StateDeleteAuditTrailModel obj = new StateDeleteAuditTrailModel
                        {
                            AuditID = Convert.ToInt32(row["AuditID"]),
                            state_id = row["state_id"].ToString(),
                            state_name = row["state_name"].ToString(),
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
