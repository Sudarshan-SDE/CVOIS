using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.Models.Connection;
using CVOIS.Models.SuperAdmin;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CVOIS.DataAccessLayer.SuperAdmin_DAL
{
    public class Department_DAL : IDepartment
    {
        private readonly string _connectionString;

        public Department_DAL(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value.DefaultConnection;
        }
        public List<DepartmentModel> Get_Department()
        {
            List<DepartmentModel> objList = new List<DepartmentModel>();
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
                        DepartmentModel obj = new DepartmentModel
                        {
                            Dept_Id = Convert.ToInt32(row["Dept_Id"]),
                            row_num = Convert.ToInt32(row["row_num"]),
                            Ministry_Name = row["Ministry_Name"].ToString(),
                            DeptName = row["DeptName"].ToString(),
                            Dept_status = row["Dept_status"].ToString()
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

        public List<SelectListItem> GetMinistries(string minCode = "", string manage = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                string query = "usp_Get_Ministry_List";
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
                            Value = row["Mincode"].ToString(),
                            Text = row["Ministry_Name"].ToString()
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

        public int InsertDepartment(DepartmentModel departmentModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    const string query = "usp_Department_Insert";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Mincode", departmentModel.Mincode);
                        cmd.Parameters.AddWithValue("@DeptName", departmentModel.DeptName);
                        cmd.Parameters.AddWithValue("@Dept_status", departmentModel.Dept_status);

                        cmd.Parameters.AddWithValue("@createdBy", departmentModel.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", departmentModel.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", departmentModel.SessionID ?? string.Empty);
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

        public DepartmentModel Get_Department_By_Id(int id)
        {
            DepartmentModel department = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Get_Department_By_Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Dept_Id", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        department = new DepartmentModel
                        {
                            Dept_Id = Convert.ToInt32(reader["Dept_Id"]),
                            Deptcode = reader["Deptcode"].ToString(),
                            Mincode = reader["Mincode"].ToString(),
                            DeptName = reader["DeptName"].ToString(),
                            Dept_status = reader["Dept_status"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching ministry by ID: " + ex.Message);
            }
            return department;
        }

        public int UpdateDepartment(DepartmentModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Department_Update";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Dept_Id", model.Dept_Id);
                        cmd.Parameters.AddWithValue("@Deptcode", model.Deptcode);
                        cmd.Parameters.AddWithValue("@Mincode", model.Mincode);
                        cmd.Parameters.AddWithValue("@DeptName", model.DeptName);
                        cmd.Parameters.AddWithValue("@Dept_status", model.Dept_status);

                        cmd.Parameters.AddWithValue("@createdBy", model.CreatedBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", model.CreatedByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", model.SessionID ?? string.Empty);
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
                throw new Exception("Error while updating ministry", ex);
            }
        }

        public int DeleteDepartment(int id, string createdBy, string createdByIP, string sessionID)
        {
            int result = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "usp_Department_Delete";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Dept_Id", id);

                        cmd.Parameters.AddWithValue("@createdBy", createdBy ?? string.Empty);
                        cmd.Parameters.AddWithValue("@createdByIP", createdByIP ?? string.Empty);
                        cmd.Parameters.AddWithValue("@sessionID", sessionID ?? string.Empty);

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
    }
}
