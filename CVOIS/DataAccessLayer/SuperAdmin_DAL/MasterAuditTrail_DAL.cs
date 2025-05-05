using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.Models.Connection;
using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CVOIS.DataAccessLayer.SuperAdmin_DAL
{
    public class MasterAuditTrail_DAL : IMasterAuditTrail
    {
        private readonly string _connectionString;
        public MasterAuditTrail_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }


        public List<MasterAuditTrailModel> Get_MasterAuditTrail()
        {
            List<MasterAuditTrailModel> objList = new List<MasterAuditTrailModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Get_MasterAuditTrailLogs", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MasterAuditTrailModel obj = new MasterAuditTrailModel
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
    }
}
