using CVOIS.Interfaces.IViewer;
using CVOIS.Models.Admin;
using CVOIS.Models.Connection;
using CVOIS.Models.Viewers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CVOIS.DataAccessLayer.Viewer
{
    public class Report_Viewer_DAL : IReport_Viewer
    {
        private readonly string _connectionString;
        public Report_Viewer_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }

        public List<SelectListItem> GetAppointingAuthorities()
        {
            List<SelectListItem> list = new List<SelectListItem>();
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

        public List<SelectListItem> GetStates()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                //string query = "SELECT state_id, state_name FROM tbl_state";
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

        public List<SelectListItem> GetLevels()
        {
            List<SelectListItem> list = new List<SelectListItem>();
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

        public List<SelectListItem> GetServices()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "select*from tbl_MasterServices";

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
                            Value = row["Service_Id"].ToString(),
                            Text = row["Services_Name"].ToString()
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

        public List<SelectListItem> GetCVO()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "select*from tbl_cvo";

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
                            Value = row["cvo_id"].ToString(),
                            Text = row["CVO_Name"].ToString()
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

        public List<SelectListItem> GetOrganisationCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "usp_Get_List_OrganisationCategory";

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
                            Value = row["Code"].ToString(),
                            Text = row["codeDesc"].ToString()
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

        //public List<SelectListItem> GetMinistries()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
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
        //                list.Add(new SelectListItem
        //                {
        //                    Value = row["Mincode"].ToString(),
        //                    Text = row["Ministry_Name"].ToString()
        //                });
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
        //    return list;
        //}

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


        public List<SelectListItem> GetDepartments()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "usp_Get_List_Department";

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
                            Value = row["Ministry_Name"].ToString(),
                            Text = row["DeptName"].ToString()
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

        public List<SelectListItem> GetOrganisations()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "usp_Get_List_Organisation";

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
                            Value = row["DeptName"].ToString(),
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

        //public List<CVO_Details_Model> Get_CVO_Details_byID(Search_GetCvoDetails_Model objdata)
        //{
        //    {
        //        List<CVO_Details_Model> list = new();

        //        using (SqlConnection con = new SqlConnection(_connectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand("usp_Search_GetCvoDetails", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@appointingAuthority", objdata.appointingAuthority ?? string.Empty);
        //            cmd.Parameters.AddWithValue("@level", objdata.level ?? string.Empty);
        //            cmd.Parameters.AddWithValue("@org_category", objdata.org_category ?? string.Empty);
        //            cmd.Parameters.AddWithValue("@tenureFrom", objdata.tenureFrom ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@tenureTo", objdata.tenureTo ?? (object)DBNull.Value);
        //            cmd.Parameters.AddWithValue("@org_state", objdata.org_state ?? string.Empty);
        //            cmd.Parameters.AddWithValue("@SERVICES", objdata.SERVICES ?? string.Empty);
        //            cmd.Parameters.AddWithValue("@CVO_NAME", objdata.CVO_NAME ?? string.Empty);
        //            cmd.Parameters.AddWithValue("@minCode", objdata.minCode ?? string.Empty);
        //            cmd.Parameters.AddWithValue("@deptCode", objdata.deptCode ?? string.Empty);
        //            cmd.Parameters.AddWithValue("@orgCode", objdata.orgCode ?? string.Empty);

        //            con.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                CVO_Details_Model obj = new CVO_Details_Model
        //                {
        //                    cvoName = reader["cvoName"].ToString(),
        //                    Ministry_Name = reader["Ministry_Name"].ToString(),
        //                    DeptName = reader["DeptName"].ToString(),
        //                    ORGNAME = reader["ORGNAME"].ToString(),
        //                    CHARGES = reader["CHARGES"].ToString(),
        //                    org_level = reader["org_level"].ToString(),
        //                    TENURE_FROM = reader["TENURE_FROM"].ToString(),
        //                    TENURE_TO = reader["TENURE_TO"].ToString(),
        //                    PHONE_NO = reader["PHONE_NO"].ToString(),
        //                    EMAIL_ID = reader["EMAIL_ID"].ToString(),
        //                    BATCH = reader["BATCH"].ToString(),
        //                    org_address = reader["org_address"].ToString(),
        //                    CVO_STATUS = reader["CVO_STATUS"].ToString()
        //                };

        //                list.Add(obj);
        //            }
        //        }
        //        return list;
        //    }
        //}


        //public List<CVO_Details_Model> Get_CVO_Details_byID
        //   (string appointingAuthority, 
        //    string level,
        //    string org_category,
        //    DateTime? tenureFrom,
        //    DateTime? tenureTo,
        //    string org_state,
        //    string SERVICES,
        //    string CVO_NAME,
        //    string minCode,
        //    string deptCode,
        //    string orgCode)
        //{
        //    List<CVO_Details_Model> list = new();

        //    using (SqlConnection con = new SqlConnection(_connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("usp_Search_GetCvoDetails", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@appointingAuthority", appointingAuthority ?? string.Empty);
        //        cmd.Parameters.AddWithValue("@level", level ?? string.Empty);
        //        cmd.Parameters.AddWithValue("@org_category", org_category ?? string.Empty);
        //        cmd.Parameters.AddWithValue("@tenureFrom", tenureFrom ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@tenureTo", tenureTo ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@org_state", org_state ?? string.Empty);
        //        cmd.Parameters.AddWithValue("@SERVICES", SERVICES ?? string.Empty);
        //        cmd.Parameters.AddWithValue("@CVO_NAME", CVO_NAME ?? string.Empty);
        //        cmd.Parameters.AddWithValue("@minCode", minCode ?? string.Empty);
        //        cmd.Parameters.AddWithValue("@deptCode", deptCode ?? string.Empty);
        //        cmd.Parameters.AddWithValue("@orgCode", orgCode ?? string.Empty);

        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            CVO_Details_Model obj = new CVO_Details_Model
        //            {
        //                cvoName = reader["cvoName"].ToString(),
        //                Ministry_Name = reader["Ministry_Name"].ToString(),
        //                DeptName = reader["DeptName"].ToString(),
        //                ORGNAME = reader["ORGNAME"].ToString(),
        //                CHARGES = reader["CHARGES"].ToString(),
        //                org_level = reader["org_level"].ToString(),
        //                TENURE_FROM = reader["TENURE_FROM"].ToString(),
        //                TENURE_TO = reader["TENURE_TO"].ToString(),
        //                PHONE_NO = reader["PHONE_NO"].ToString(),
        //                EMAIL_ID = reader["EMAIL_ID"].ToString(),
        //                BATCH = reader["BATCH"].ToString(),
        //                org_address = reader["org_address"].ToString(),
        //                CVO_STATUS = reader["CVO_STATUS"].ToString()
        //            };

        //            list.Add(obj);
        //        }
        //    }
        //    return list;
        //}


      
        public List<CVO_Details_Model> Search_GetCvoDetails(ViewerModel _objmodeldata)
        {
            List<CVO_Details_Model> objList = new List<CVO_Details_Model>();
            string query = "usp_Search_GetCvoDetails";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@appointingAuthority", _objmodeldata.AppointingAuthority ?? "");
                    cmd.Parameters.AddWithValue("@level", _objmodeldata.Level ?? "");
                    cmd.Parameters.AddWithValue("@org_state", _objmodeldata.State ?? "");
                    cmd.Parameters.AddWithValue("@SERVICES", _objmodeldata.Service ?? "");
                    cmd.Parameters.AddWithValue("@CVO_NAME", _objmodeldata.CVO ?? "");
                    cmd.Parameters.AddWithValue("@minCode", _objmodeldata.Ministry ?? "");
                    cmd.Parameters.AddWithValue("@deptCode", _objmodeldata.Department ?? "");
                    cmd.Parameters.AddWithValue("@orgCode", _objmodeldata.OrganisationCategory ?? "");
                    cmd.Parameters.AddWithValue("@org_category", _objmodeldata.OrganisationCategory ?? "");
                    cmd.Parameters.AddWithValue("@tenureFrom", _objmodeldata.Tenurefrom ?? null);
                    cmd.Parameters.AddWithValue("@tenureTo", _objmodeldata.TenureTo ?? null);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CVO_Details_Model _CVOData = new CVO_Details_Model
                            {
                                cvoName = reader["cvoName"].ToString(),
                                Ministry_Name = reader["Ministry_Name"].ToString(),
                                DeptName = reader["DeptName"].ToString(),
                                ORGNAME = reader["ORGNAME"].ToString(),
                                CHARGES = reader["CHARGES"].ToString(),
                                org_level = reader["org_level"].ToString(),
                                TENURE_FROM = (DateTime)reader["TENURE_FROM"],
                                TENURE_TO = (DateTime)reader["TENURE_TO"],
                                PHONE_NO = reader["PHONE_NO"].ToString(),
                                EMAIL_ID = reader["EMAIL_ID"].ToString(),
                                BATCH = reader["BATCH"].ToString(),
                                org_address = reader["org_address"].ToString(),
                                CVO_STATUS = reader["CVO_STATUS"].ToString()
                            };
                            objList.Add(_CVOData);
                        }
                    }
                }
            }
            return objList;
        }

    }
}





//code appotining authority
//public List<CVO_Details_Model> Get_Appointing_Authority_By_Id(string id)
//{
//    List<CVO_Details_Model> objlist = new List<CVO_Details_Model>();

//    using (SqlConnection con = new SqlConnection(_connectionString))
//    {
//        string query = "SELECT * FROM usp_Search_GetCvoDetails WHERE appointingAuthority = @appointingAuthority";

//        using (SqlCommand cmd = new SqlCommand(query, con))
//        {
//            cmd.Parameters.AddWithValue("@appointingAuthority", id);
//            con.Open();
//            SqlDataReader reader = cmd.ExecuteReader();

//            while (reader.Read())
//            {
//                CVO_Details_Model obje = new CVO_Details_Model
//                {
//                    cvoName = reader["cvoName"].ToString(),
//                    Ministry_Name = reader["Ministry_Name"].ToString(),
//                    DeptName = reader["DeptName"].ToString(),
//                    ORGNAME = reader["ORGNAME"].ToString(),
//                    CHARGES = reader["CHARGES"].ToString(),
//                    org_level = reader["org_level"].ToString(),
//                    TENURE_FROM = reader["TENURE_FROM"].ToString(),
//                    TENURE_TO = reader["TENURE_TO"].ToString(),
//                    PHONE_NO = reader["PHONE_NO"].ToString(),
//                    EMAIL_ID = reader["EMAIL_ID"].ToString(),
//                    BATCH = reader["BATCH"].ToString(),
//                    org_address = reader["org_address"].ToString(),
//                    CVO_STATUS = reader["CVO_STATUS"].ToString()
//                };

//                objlist.Add(obje);
//            }
//        }
//    }

//    return objlist;
//}


//one table data fetching 

