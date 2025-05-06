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
    public class MasterCvoServices_DAL: IMasterCvoServices
    {
        private readonly string _connectionString;
        public MasterCvoServices_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }

        public List<MasterCvoServicesModel> Get_MasterCvoServicesModel()
        {
            List<MasterCvoServicesModel> objList = new List<MasterCvoServicesModel>();
            try
            {
                string query = "usp_Get_List_CvoServices";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MasterCvoServicesModel obj = new MasterCvoServicesModel
                        {
                            row_num = Convert.ToInt32(row["sno"]),
                            sno = Convert.ToInt32(row["sno"]),
                            serviceCode = row["serviceCode"].ToString(),
                            serviceDesc = row["serviceDesc"].ToString()
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

        public int InsertMasterCvoServices(MasterCvoServicesModel MasterCvoServicesmodel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    const string query = "usp_MasterCvoService_Insert";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@serviceCode", MasterCvoServicesmodel.serviceCode);
                        cmd.Parameters.AddWithValue("@serviceDesc", MasterCvoServicesmodel.serviceDesc);

                        cmd.Parameters.AddWithValue("@createdBy", MasterCvoServicesmodel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", MasterCvoServicesmodel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", MasterCvoServicesmodel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", MasterCvoServicesmodel.actionCategory?? string.Empty);
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
                throw new Exception("A database error occurred while inserting Master CVO Services.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting Master CVO Services.", ex);
            }
        }

        public MasterCvoServicesModel Get_MasterCvoServices_By_Id(int id)
        {
            MasterCvoServicesModel masterservice = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Get_MasterCvoServices_By_Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sno", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        masterservice = new MasterCvoServicesModel
                        {
                            sno = Convert.ToInt32(reader["sno"]),
                            serviceCode = reader["serviceCode"].ToString(),
                            serviceDesc = reader["serviceDesc"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching Master CVO Services by ID: " + ex.Message);
            }
            return masterservice;
        }

        public int UpdateMasterCvoServices(MasterCvoServicesModel MasterCvoServicesmodel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_MasterCvoService_Update";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sno", MasterCvoServicesmodel.sno);
                        cmd.Parameters.AddWithValue("@serviceCode", MasterCvoServicesmodel.serviceCode);
                        cmd.Parameters.AddWithValue("@serviceDesc", MasterCvoServicesmodel.serviceDesc);

                        cmd.Parameters.AddWithValue("@createdBy", MasterCvoServicesmodel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", MasterCvoServicesmodel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", MasterCvoServicesmodel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", MasterCvoServicesmodel.actionCategory ?? string.Empty);
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
                throw new Exception("Error while updating Master CVO Services", ex);
            }
        }

        public int DeleteMasterCvoServices(int id, string createdBy, string createdByIP, string sessionID, string actionCategory)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_MasterCvoService_Delete";
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

        public List<MasterCVOServicesAuditTrailModel> Get_MasterCvoServicesAuditTrail()
        {
            List<MasterCVOServicesAuditTrailModel> objList = new List<MasterCVOServicesAuditTrailModel>();
            try
            {
                string query = "usp_Get_AuditLog_MasterCvoService";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MasterCVOServicesAuditTrailModel obj = new MasterCVOServicesAuditTrailModel
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


        public List<MasterCVOServicesDeleteAuditTrailModel> Get_MasterCvoServicesDeleteAuditTrail()
        {
            List<MasterCVOServicesDeleteAuditTrailModel> objList = new List<MasterCVOServicesDeleteAuditTrailModel>();
            try
            {
                string query = "select*from Audit_Deleted_MasterCvoServices";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.Text;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MasterCVOServicesDeleteAuditTrailModel obj = new MasterCVOServicesDeleteAuditTrailModel
                        {
                            AuditID = Convert.ToInt32(row["AuditID"]),
                            MasterCvoServices_Id = Convert.ToInt32(row["MasterCvoServices_Id"]),
                            MasterCvoServices_Code= row["MasterCvoServices_Code"].ToString(),
                            MasterCvoServices_Name= row["MasterCvoServices_Name"].ToString(),
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
