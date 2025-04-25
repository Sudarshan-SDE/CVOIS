using CVOIS.Interfaces.Admin;
using CVOIS.Models.Admin;
using CVOIS.Models.Connection;
using CVOIS.Models.SuperAdmin;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using System.Data;

namespace CVOIS.DataAccessLayer.Admin
{
    public class Report_Admin_DAL : IReport_Admin
    {
        private readonly string _connectionString;
        public Report_Admin_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }
        public List<MinistriesModel> Get_Ministries(string minCode, string manage)
        {
            List<MinistriesModel> objList = new List<MinistriesModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Get_List_Ministry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@minCode", minCode ?? string.Empty);
                        cmd.Parameters.AddWithValue("@manage", manage ?? string.Empty);

                        con.Open(); // Use synchronous for now

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MinistriesModel obj = new MinistriesModel
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
        public List<DepartmentsModel> Get_Departments()
        {
            List<DepartmentsModel> objList = new List<DepartmentsModel>();
            string query = "usp_Get_List_Department";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DepartmentsModel obj = new DepartmentsModel
                    {
                        SNo = Convert.ToInt32(reader["row_num"]),
                        Ministry_Name = reader["Ministry_Name"].ToString(),
                        DeptName = reader["DeptName"].ToString(),
                        Dept_status = reader["Dept_status"].ToString()
                    };
                    objList.Add(obj);
                }
                con.Close();
            }
            return objList;
        }
        public List<OrganizationsModel> Get_Organization()
        {
            List<OrganizationsModel> objList = new List<OrganizationsModel>();
            string query = "usp_Get_List_Organisation";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrganizationsModel obj = new OrganizationsModel
                    {
                        SNo = Convert.ToInt32(reader["row_num"]),
                        ORG_ID = Convert.ToInt32(reader["ORG_ID"]),
                        Ministry_Name = reader["Ministry_Name"].ToString(),
                        DeptName = reader["DeptName"].ToString(),
                        Org_Name = reader["ORGNAME"].ToString(),
                        org_address = reader["org_address"].ToString(),
                        pincode = reader["pincode"].ToString(),
                        phoneno = reader["phoneno"].ToString(),
                        org_category = reader["org_category"].ToString(),
                        org_status = reader["org_status"].ToString()
                    };
                    objList.Add(obj);
                }
                con.Close();
            }
            return objList;
        }
        public List<FullTimeCVOModel> Get_FullTimeCVO()
        {
            List<FullTimeCVOModel> objList = new List<FullTimeCVOModel>();
            string query = "usp_GetFullTimeCvoList";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FullTimeCVOModel obj = new FullTimeCVOModel
                    {
                        Sno = Convert.ToInt32(reader["Sno"]),
                        cvoName = reader["cvoName"].ToString(),
                        orgName = reader["orgName"].ToString(),
                        cvoDesignation = reader["cvoDesignation"].ToString(),
                        cvoServices = reader["cvoServices"].ToString(),
                        cvoBatch = reader["cvoBatch"].ToString(),
                        cvoLevel = reader["cvoLevel"].ToString(),
                        cvoTenureFrom = reader["cvoTenureFrom"].ToString(),
                        cvoTenureTo = reader["cvoTenureTo"].ToString(),
                        CHARGES = reader["CHARGES"].ToString(),
                        PHONE_NO = reader["PHONE_NO"].ToString(),
                        EMAIL_ID = reader["EMAIL_ID"].ToString(),
                    };
                    objList.Add(obj);
                }
                con.Close();
            }
            return objList;
        }
        public List<VacantFullTimeCVOModel> Get_VacantFullTimeCVO(string reportType)
        {
            List<VacantFullTimeCVOModel> objList = new List<VacantFullTimeCVOModel>();
            string query = "usp_GetFullTimeCvoList_Vacant";

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameter
                        cmd.Parameters.AddWithValue("@reportType", reportType);

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VacantFullTimeCVOModel obj = new VacantFullTimeCVOModel
                                {
                                    SNo = Convert.ToInt32(reader["Sno"]),
                                    cvoName = reader["cvoName"].ToString(),
                                    orgName = reader["orgName"].ToString(),
                                    VacantFrom = reader["VacantFrom"].ToString(),
                                };
                                objList.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Get_VacantFullTimeCVO: " + ex.Message);
            }

            return objList;
        }




        public List<AdminDashboardModel> GetAdminDashboardData(string reportType, string VER_APP_FLAG)
        {
            List<AdminDashboardModel> objList = new List<AdminDashboardModel>();
            string query = "usp_ReportPendingWithApprover";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Correctly adding parameters
                    cmd.Parameters.AddWithValue("@reportType", reportType);
                    cmd.Parameters.AddWithValue("@VER_APP_FLAG", VER_APP_FLAG);

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AdminDashboardModel obj = new AdminDashboardModel
                            {
                                TotalRequestReceived = reader["TotalRequestReceived"]?.ToString() ?? "0",
                                TotalRequestApprovedCount = reader["TotalRequestApprovedCount"]?.ToString() ?? "0",
                                TotalRequestRejected = reader["TotalRequestRejected"]?.ToString() ?? "0",
                                TotalRequestPendingCount = reader["TotalRequestPendingCount"]?.ToString() ?? "0",
                            };
                            objList.Add(obj);
                        }
                    }
                }
            }
            return objList;
        }

        public List<AdminViewRequestDetailsModel> AdminViewRequestDetails(string reportType, string VER_APP_FLAG)
        {
            List<AdminViewRequestDetailsModel> objList = new List<AdminViewRequestDetailsModel>();
            string query = "usp_ReportPendingWithApprover";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Correctly adding parameters
                    cmd.Parameters.AddWithValue("@reportType", reportType);
                    cmd.Parameters.AddWithValue("@VER_APP_FLAG", VER_APP_FLAG);

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AdminViewRequestDetailsModel obj = new AdminViewRequestDetailsModel
                            {
                                MinName = reader["minName"]?.ToString() ?? "0",
                                OrgName = reader["orgName"]?.ToString() ?? "0",
                                ReqId = reader["reqId"]?.ToString() ?? "0",
                                RequestIssue = reader["requestIssue"]?.ToString() ?? "0",
                                ReqComments = reader["reqComments"]?.ToString() ?? "0",
                                ApproverRemarks = reader["approverRemarks"]?.ToString() ?? "0",
                                ReqSendAdminFlag = reader["reqSendAdminFlag"]?.ToString() ?? "0",
                                ReqVerificationDate = reader.IsDBNull(reader.GetOrdinal("reqverificationDate"))
                                 ? (DateTime?)null // Set as null if the value is NULL in DB
                                : reader.GetDateTime(reader.GetOrdinal("reqverificationDate")),
                                RequestStatus = reader["requestStatus"]?.ToString() ?? "0",
                            };
                            objList.Add(obj);
                        }
                    }
                }
            }
            return objList;
        }


        #region Added as on date 13_03_2025 for Magane the full time CVO

        public List<FulltimeCVOListForManageModel> Get_FulltimeCVOlistForManage()
        {
            try
            {
                List<FulltimeCVOListForManageModel> objList = new List<FulltimeCVOListForManageModel>();
                string query = "usp_GetFullTimeCvoList_Manage";

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FulltimeCVOListForManageModel obj = new FulltimeCVOListForManageModel
                                {
                                    Sno = Convert.ToInt32(reader["sno"]?.ToString() ?? "0"),
                                    cvoName = reader["cvoName"]?.ToString() ?? "0",
                                    orgName = reader["orgName"]?.ToString() ?? "0",
                                    cvoDesignation = reader["cvoDesignation"]?.ToString() ?? "0",
                                    cvoServices = reader["cvoServices"]?.ToString() ?? "0",
                                    cvoBatch = reader["cvoBatch"]?.ToString() ?? "0",
                                    cvoLevel = reader["cvoLevel"]?.ToString() ?? "0",
                                    cvoTenureFrom = reader.IsDBNull(reader.GetOrdinal("cvoTenureFrom"))
                                     ? (DateTime?)null // Set as null if the value is NULL in DB
                                    : reader.GetDateTime(reader.GetOrdinal("cvoTenureFrom")),
                                    cvoTenureTo = reader.IsDBNull(reader.GetOrdinal("cvoTenureFrom"))
                                     ? (DateTime?)null // Set as null if the value is NULL in DB
                                    : reader.GetDateTime(reader.GetOrdinal("cvoTenureFrom")),
                                    charges = reader["charges"]?.ToString() ?? "0",
                                    phoneNo = reader["phoneNo"]?.ToString() ?? "0",
                                    emailid = reader["emailid"]?.ToString() ?? "0",
                                    Cvostatus = reader["Cvostatus"]?.ToString() ?? "0",
                                    cvoid = Convert.ToInt32(reader["cvoid"]?.ToString() ?? "0"),
                                    verifierflag = reader["verifierflag"]?.ToString() ?? "0",
                                };
                                objList.Add(obj);
                            }
                        }
                    }
                }
                return objList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public List<SelectListItem> Get_OrgforDropdown()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "usp_GetOrgForDropdown";

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        list.Add(new SelectListItem
                        {
                            Value = row["ORGCODE"].ToString(),
                            Text = row["ORGNAME"].ToString()
                        });
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

        public List<SelectListItem> Get_ServicesforDropdown()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "usp_Get_services_for_dropdown";

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        list.Add(new SelectListItem
                        {
                            Value = row["Services_Code"].ToString(),
                            Text = row["ServicesName"].ToString()
                        });
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

        #endregion

        #region Added code as on date 19_03_2025 
        public List<CheckOrgWithcvoModel> Check_OrgWithCvo(string OrgCode)
        {
            try
            {
                List<CheckOrgWithcvoModel> objList = new List<CheckOrgWithcvoModel>();
                string query = "usp_Check_OrgWithCvo";

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@orgcode", OrgCode);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CheckOrgWithcvoModel obj = new CheckOrgWithcvoModel
                                {
                                    CVO_NAME = reader["CVO_NAME"]?.ToString() ?? "0",
                                    ORGNAME = reader["ORGNAME"]?.ToString() ?? "0",
                                    CHARGES = reader["CHARGES"]?.ToString() ?? "0",
                                    TENURE_FROM = reader["TENURE_FROM"]?.ToString() ?? "0",
                                    TENURE_TO = reader["TENURE_TO"]?.ToString() ?? "0",
                                    PHONE_NO = reader["PHONE_NO"]?.ToString() ?? "0",
                                    EMAIL_ID = reader["EMAIL_ID"]?.ToString() ?? "0",
                                };
                                objList.Add(obj);
                            }
                        }
                    }
                }
                return objList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Added as on date 02_04_2025 

        public int InsertFullTimeCVO(AddFullTimeCVOModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    const string query = "usp_AddFullTimeCVO";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ORGCODE", model.Organisation);
                        cmd.Parameters.AddWithValue("@CHARGES", model._charges_Dropdown.SelectedCharges);
                        cmd.Parameters.AddWithValue("@TITLE", model._title_Dropdown.SelectedTitle);
                        cmd.Parameters.AddWithValue("@CVO_NAME", model.EnterCVOName);
                        cmd.Parameters.AddWithValue("@DESIGNATION", "Null");
                        cmd.Parameters.AddWithValue("@GENDER", model._gender.SelectedGender);
                        cmd.Parameters.AddWithValue("@PHONE_NO", model.MobileNo);
                        cmd.Parameters.AddWithValue("@SERVICES", model.SelectedService);
                        cmd.Parameters.AddWithValue("@BATCH", model.SelectedBatch);
                        cmd.Parameters.AddWithValue("@TENURE_FROM", model.TenureFrom);
                        cmd.Parameters.AddWithValue("@TENURE_TO", model.TenureTo);
                        cmd.Parameters.AddWithValue("@EMAIL_ID", model.Email);
                        cmd.Parameters.AddWithValue("@CVO_STATUS", model._status.SelectedStatus);
                        con.Open();
                        return cmd.ExecuteNonQuery();
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


        #endregion


    }
}




//23/04/2024
//public List<Ministries> Get_Ministries()
//{
//    List<Ministries> objList = new List<Ministries>();
//    try
//    {
//        string query = "usp_Get_List_Ministry";
//        using (SqlConnection con = new SqlConnection(_connectionString))
//        {
//            SqlDataAdapter sda = new SqlDataAdapter(query, con);
//            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
//            DataSet ds = new DataSet();
//            sda.Fill(ds);
//            foreach (DataRow row in ds.Tables[0].Rows)
//            {
//                Ministries obj = new Ministries
//                {
//                    Min_id = Convert.ToInt32(row["Min_id"]),
//                    Mincode = row["MinCode"].ToString(),
//                    Ministry_Name = row["Ministry_Name"].ToString(),
//                    Min_Status = row["Min_Status"].ToString(),
//                    Ministry_Type = row["MinistryType"].ToString()
//                };
//                objList.Add(obj);
//            }
//        }
//    }
//    catch (SqlException ex)
//    {
//        Console.WriteLine("SQL Error: " + ex.Message);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("General Error: " + ex.Message);
//    }
//    return objList;
//}