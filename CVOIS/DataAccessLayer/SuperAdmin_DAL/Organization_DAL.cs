using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.Models.Admin;
using CVOIS.Models.Connection;
using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;

namespace CVOIS.DataAccessLayer.SuperAdmin_DAL
{
    public class Organization_DAL : IOrganization
    {
        private readonly string _connectionString;
        public Organization_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }

        //Start Main Crud Operation of Organization
        public List<OrganizationModel> Get_OrganizationList()
        {
            List<OrganizationModel> objList = new List<OrganizationModel>();
            try
            {
                string query = "usp_Get_List_Organisation";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrganizationModel obj = new OrganizationModel
                                {
                                    row_num = Convert.ToInt32(reader["row_num"]),
                                    ORG_ID = Convert.ToInt32(reader["ORG_ID"]),
                                    Ministry_Name = reader["Ministry_Name"].ToString(),
                                    DeptName = reader["DeptName"].ToString(),
                                    ORGNAME = reader["ORGNAME"].ToString(),
                                    org_address = reader["org_address"].ToString(),
                                    pincode = reader["pincode"].ToString(),
                                    phoneno = reader["phoneno"].ToString(),
                                    org_category = reader["org_category"].ToString(),
                                    org_status = reader["org_status"].ToString()
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

        public int InsertOrganization(OrganizationModel organizationModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    const string query = "usp_Organization_Insert";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MINCODE", organizationModel.MINCODE);
                        cmd.Parameters.AddWithValue("@DEPTCODE", organizationModel.DEPTCODE);
                        cmd.Parameters.AddWithValue("@ORGNAME", organizationModel.ORGNAME);
                        cmd.Parameters.AddWithValue("@file_no", organizationModel.file_no);
                        cmd.Parameters.AddWithValue("@APPOINTING_AUTHORITY", organizationModel.APPOINTING_AUTHORITY);
                        cmd.Parameters.AddWithValue("@org_level", organizationModel.org_level);
                        cmd.Parameters.AddWithValue("@section", organizationModel.section);
                        cmd.Parameters.AddWithValue("@org_status", organizationModel.org_status);
                        cmd.Parameters.AddWithValue("@org_country", organizationModel.org_country);
                        cmd.Parameters.AddWithValue("@org_address", organizationModel.org_address);
                        cmd.Parameters.AddWithValue("@org_state", organizationModel.org_state);
                        cmd.Parameters.AddWithValue("@org_city", organizationModel.org_district);
                        cmd.Parameters.AddWithValue("@pincode", organizationModel.pincode);
                        cmd.Parameters.AddWithValue("@phoneno", organizationModel.phoneno);
                        cmd.Parameters.AddWithValue("@fax", organizationModel.fax);
                        cmd.Parameters.AddWithValue("@org_category", organizationModel.org_category);
                        cmd.Parameters.AddWithValue("@EMAIL_ID", organizationModel.EMAIL_ID);

                        cmd.Parameters.AddWithValue("@createdBy", organizationModel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", organizationModel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", organizationModel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", organizationModel.actionCategory ?? string.Empty);
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

        public OrganizationModel Get_Organization_By_Id(int id)
        {
            OrganizationModel organization = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Get_Organization_By_Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ORG_ID", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        organization = new OrganizationModel
                        {
                            ORG_ID = Convert.ToInt32(reader["ORG_ID"]),
                            ORGCODE = reader["ORGCODE"].ToString(),
                            MINCODE = reader["MINCODE"].ToString(),
                            DEPTCODE = reader["DEPTCODE"].ToString(),
                            ORGNAME = reader["ORGNAME"].ToString(),
                            file_no = reader["file_no"].ToString(),
                            APPOINTING_AUTHORITY = reader["APPOINTING_AUTHORITY"].ToString(),
                            org_level = reader["org_level"].ToString(),
                            section = Convert.ToInt32(reader["section"]),
                            org_status = reader["org_status"].ToString(),
                            org_country = reader["org_country"].ToString(),
                            org_address = reader["org_address"].ToString(),
                            org_state = reader["org_state"].ToString(),
                            org_district = reader["org_city"].ToString(),
                            pincode = reader["pincode"].ToString(),
                            phoneno = reader["phoneno"].ToString(),
                            fax = reader["fax"].ToString(),
                            org_category = reader["org_category"].ToString(),
                            EMAIL_ID = reader["EMAIL_ID"].ToString(),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching organization by ID: " + ex.Message);
            }
            return organization;
        }

        public int UpdateOrganization(OrganizationModel organizationModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Organization_Update";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ORG_ID", organizationModel.ORG_ID);
                        cmd.Parameters.AddWithValue("@ORGCODE", organizationModel.ORGCODE);
                        cmd.Parameters.AddWithValue("@MINCODE", organizationModel.MINCODE);
                        cmd.Parameters.AddWithValue("@DEPTCODE", organizationModel.DEPTCODE);
                        cmd.Parameters.AddWithValue("@ORGNAME", organizationModel.ORGNAME);
                        cmd.Parameters.AddWithValue("@file_no", organizationModel.file_no);
                        cmd.Parameters.AddWithValue("@APPOINTING_AUTHORITY", organizationModel.APPOINTING_AUTHORITY);
                        cmd.Parameters.AddWithValue("@org_level", organizationModel.org_level);
                        cmd.Parameters.AddWithValue("@section", organizationModel.section);
                        cmd.Parameters.AddWithValue("@org_status", organizationModel.org_status);
                        cmd.Parameters.AddWithValue("@org_country", organizationModel.org_country);
                        cmd.Parameters.AddWithValue("@org_address", organizationModel.org_address);
                        cmd.Parameters.AddWithValue("@org_state", organizationModel.org_state);
                        cmd.Parameters.AddWithValue("@org_city", organizationModel.org_district);
                        cmd.Parameters.AddWithValue("@pincode", organizationModel.pincode);
                        cmd.Parameters.AddWithValue("@phoneno", organizationModel.phoneno);
                        cmd.Parameters.AddWithValue("@fax", organizationModel.fax);
                        cmd.Parameters.AddWithValue("@org_category", organizationModel.org_category);
                        cmd.Parameters.AddWithValue("@EMAIL_ID", organizationModel.EMAIL_ID);
                        cmd.Parameters.AddWithValue("@UPDATE_DATE", organizationModel.UPDATE_DATE);

                        cmd.Parameters.AddWithValue("@createdBy", organizationModel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", organizationModel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", organizationModel.SessionID ?? string.Empty);
                        cmd.Parameters.AddWithValue("@actionCategory", organizationModel.actionCategory ?? string.Empty);
                        con.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating Organization", ex);
            }
        }

        public int DeleteOrganization(int id, string createdBy, string createdByIP, string sessionID, string actionCategory)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Organization_Delete";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ORG_ID", id);

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

        public List<OrganizationAuditTrailModel> Get_OrganizationAuditTrail()
        {
            List<OrganizationAuditTrailModel> objList = new List<OrganizationAuditTrailModel>();
            try
            {
                string query = "usp_Get_AuditLog_Organization";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrganizationAuditTrailModel obj = new OrganizationAuditTrailModel
                                {
                                    auditID= Convert.ToInt32(reader["auditID"]),
                                    auditDetails= reader["auditDetails"].ToString(),
                                    createdBy = reader["createdBy"].ToString(),
                                    createdOn = reader["createdOn"].ToString(),
                                    createdByIP = reader["createdByIP"].ToString(),
                                    sessionID = reader["sessionID"].ToString(),
                                    actionCategory= reader["actionCategory"].ToString()
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
        //EndMain Crud Operation of Organization






        //Other DropDownList
        public List<SelectListItem> GetAppointingAuthority()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "select * from tbl_masterAppointingAuthority";

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.Text;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        list.Add(new SelectListItem
                        {
                            Value = row["AppointingAuthority_Code"].ToString(),
                            Text = row["APPOINTING_AUTHORITY"].ToString()
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
        public List<SelectListItem> GetOrgLevel()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "select * from tbl_masterLevel";

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.CommandType = CommandType.Text;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        list.Add(new SelectListItem
                        {
                            Value = row["Code"].ToString(),
                            Text = row["org_level"].ToString()
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
        public List<SelectListItem> GetStates()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "usp_Get_List_State";

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
                            Value = row["state_id"].ToString(),
                            Text = row["state_name"].ToString()
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

        public List<SelectListItem> GetDistrictsByState(string state_id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetDistrict_basis_of_state", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@state_id", state_id);

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new SelectListItem
                                {
                                    Value = reader["district_id"].ToString(),
                                    Text = reader["district_name"].ToString()
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
        public List<SelectListItem> GetMinistries(string minCode = "", string manage = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Get_List_Ministry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@minCode", minCode);
                        cmd.Parameters.AddWithValue("@manage", manage);

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new SelectListItem
                                {
                                    Value = reader["Mincode"].ToString(),
                                    Text = reader["Ministry_Name"].ToString()
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
        public List<SelectListItem> Get_DepartmentfordropdownbyMincode_New(string Mincode)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetDepartment_basis_of_ministry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@minicode", Mincode);

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new SelectListItem
                                {
                                    Value = reader["Deptcode"].ToString(),
                                    Text = reader["DeptName"].ToString()
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
    }
}