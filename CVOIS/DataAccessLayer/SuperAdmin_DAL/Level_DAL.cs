using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.Models.Connection;
using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;
using CVOIS.Models.Viewers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CVOIS.DataAccessLayer.SuperAdmin_DAL
{
    public class Level_DAL : ILevel
    {
        private readonly string _connectionString;
        public Level_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }

        public List<OrgLevelModel> Get_Level()
        {
            List<OrgLevelModel> objList = new List<OrgLevelModel>();
            try
            {
                string query = "usp_Get_List_OrganisationLevel";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        OrgLevelModel obj = new OrgLevelModel
                        {
                            sno = Convert.ToInt32(row["sno"]),
                            row_num = Convert.ToInt32(row["row_num"]),
                            Code = row["Code"].ToString(),
                            org_level = row["org_level"].ToString()
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

        public int InsertLevel(OrgLevelModel levelModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    const string query = "usp_MasterLevel_Insert";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Code", levelModel.Code);
                        cmd.Parameters.AddWithValue("@org_level", levelModel.org_level);

                        cmd.Parameters.AddWithValue("@createdBy", levelModel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", levelModel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", levelModel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", levelModel.actionCategory ?? string.Empty);
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
                throw new Exception("A database error occurred while inserting ministry.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting ministry.", ex);
            }
        }

        public OrgLevelModel Get_Level_By_Id(int id)
        {
            OrgLevelModel level = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Get_Level_By_Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sno", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        level = new OrgLevelModel
                        {
                            sno = Convert.ToInt32(reader["sno"]),
                            Code = reader["Code"].ToString(),
                            org_level = reader["org_level"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching Level by ID: " + ex.Message);
            }
            return level;
        }

        public int UpdateLevel(OrgLevelModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_MasterLevel_Update";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sno", model.sno);
                        cmd.Parameters.AddWithValue("@Code", model.Code);
                        cmd.Parameters.AddWithValue("@org_level", model.org_level);

                        cmd.Parameters.AddWithValue("@createdBy", model.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", model.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", model.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", model.actionCategory ?? string.Empty);
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
                throw new Exception("Error while updating Level", ex);
            }
        }

        public int DeleteLevel(int id, string createdBy, string createdByIP, string sessionID, string actionCategory)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_MasterLevel_Delete";
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

        public List<OrgLevelAuditTrailModel> Get_LevelAuditTrail()
        {
            List<OrgLevelAuditTrailModel> objList = new List<OrgLevelAuditTrailModel>();
            try
            {
                string query = "usp_Get_AuditLog_OrganisationLevel";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        OrgLevelAuditTrailModel obj = new OrgLevelAuditTrailModel
                        {
                            auditID = Convert.ToInt32(row["auditID"]),
                            auditDetails = row["auditDetails"].ToString(),
                            createdBy = row["createdBy"].ToString(),
                            createdOn = row["createdOn"].ToString(),
                            createdByIP = row["createdByIP"].ToString(),
                            sessionID = row["sessionID"].ToString(),
                            actionCategory = row["actionCategory"].ToString(),
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
