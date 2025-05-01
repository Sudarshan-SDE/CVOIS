using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.Models.Admin;
using CVOIS.Models.SuperAdmin;
using CVOIS.Models.Viewers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CVOIS.Models.SuperAdmin.AuditTrail;

namespace CVOIS.Controllers
{
    public class SuperAdminController : Controller
    {
        private readonly IDatabaseDAL _databaseDAL;
        private readonly IMinistry _ministry;
        private readonly IDepartment _department;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IOrganization _organization;
        private readonly ILevel _level;
        private readonly IAppointingAuthority _appointingauthority;
        private readonly IMasterCvoServices _masterCvoServices;
        private readonly IState _state;

        public SuperAdminController(IMinistry ministry, IDepartment department, IOrganization organization, IDatabaseDAL databaseDAL, ILevel level, IAppointingAuthority appointingauthority, IMasterCvoServices masterCvoServices, IState state, IHttpContextAccessor contextAccessor)
        {
            this._ministry = ministry;
            this._department = department;
            this._organization = organization;
            this._databaseDAL = databaseDAL;
            this._level = level;
            _contextAccessor = contextAccessor;
            this._appointingauthority = appointingauthority;
            this._masterCvoServices = masterCvoServices;
            this._state = state;
        }

        public IActionResult SuperAdminDashboard()
        {
            try
            {
                ViewBag.title = "SuperAdmin Dashboard";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                int result = _databaseDAL.TestDatabaseConnection();
                if (result > 0)
                {
                    ViewBag.title = "SuperAdmin Dashboard";
                    return View();
                }
                else
                {
                    return Content("Database connection failed.");
                }
            }
            catch (Exception)
            {
                TempData["DbError"] = "An unexpected error occurred while checking the database connection.";
                return RedirectToAction("SuperAdminDashboard");
            }
        }


        //-----------------------------------------Ministry Section
        [HttpGet]
        public async Task<IActionResult> Ministry(string minCode = "")
        {
            try
            {
                ViewBag.title = "Ministry";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var manageFlag = string.IsNullOrEmpty(minCode) ? "manage" : "";

                var model = new SuperAdminViewModel
                {
                    Ministry_List = await _ministry.Get_MinistriesAsync(minCode, manageFlag),
                    MinistryType_ddl_List = await _ministry.GetMinistryTypeAsync()
                };

                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the page.";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ministry(SuperAdminViewModel objModel)
        {
            try
            {
                ViewBag.title = "Ministry";
                objModel.Ministry.CreatedBy = HttpContext.Session.GetString("Fullname");
                objModel.Ministry.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objModel.Ministry.SessionID = HttpContext.Session.Id;
                objModel.Ministry.actionCategory = "Ministry";
                int isInserted = await _ministry.InsertMinistryAsync(objModel.Ministry);
                if (isInserted > 0)
                {
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Ministry_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Ministry_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditMinistry(int id)
        {
            try
            {
                ViewBag.title = "Ministry";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var ministry = await _ministry.Get_Ministry_By_IdAsync(id);
                if (ministry == null)
                {
                    return NotFound();
                }

                var model = new SuperAdminViewModel
                {
                    Ministry = ministry,
                    MinistryType_ddl_List = await _ministry.GetMinistryTypeAsync()
                };

                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the ministry for editing.";
                return View("Ministry");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditMinistry(SuperAdminViewModel objmodel)
        {
            try
            {
                ViewBag.title = "Ministry";
                objmodel.Ministry.CreatedBy = HttpContext.Session.GetString("Fullname");
                objmodel.Ministry.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objmodel.Ministry.SessionID = HttpContext.Session.Id;
                objmodel.Ministry.actionCategory = "Ministry";

                int isInserted = await _ministry.UpdateMinistryAsync(objmodel.Ministry);
                if (isInserted > 0)
                {
                    var model = new SuperAdminViewModel
                    {
                        Ministry = new MinistryModel(),
                    };
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Update_Ministry_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Update_Ministry_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMinistry(int id)
        {
            try
            {
                string createdBy = HttpContext.Session.GetString("Fullname");
                string createdByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                string sessionID = HttpContext.Session.Id;
                string actionCategory = "Ministry";

                int result = await _ministry.DeleteMinistryAsync(id, createdBy, createdByIP, sessionID, actionCategory);
                if (result > 0)
                {
                    TempData["Delete_Ministry_Message"] = "Ministry deleted successfully";
                }
                else
                {
                    TempData["Failed_Ministry_Message"] = "Failed to delete Ministry";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the ministry.";
            }

            return RedirectToAction("Ministry");
        }

        [HttpGet]
        public async Task<IActionResult> MinistryAuditTrail()
        {
            try
            {
                ViewBag.title = "Ministry Audit";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    Ministry_AuditTrail_List = await _ministry.Get_MinistryAuditTrailAsync()
                };
                return View(model);
            }
            catch (Exception)
            {
                return View();
            }
        }



        //-----------------------------------------Department Section
        [HttpGet]
        public IActionResult Department()
        {
            try
            {
                ViewBag.title = "Department";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    Department = new DepartmentModel(),
                    Department_List = _department.Get_Department(),
                    Ministry_ddl_List = _department.GetMinistries()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the form.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Department(SuperAdminViewModel objModel)
        {
            try
            {
                ViewBag.title = "Department";
                objModel.Department.CreatedBy = HttpContext.Session.GetString("Fullname");
                objModel.Department.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objModel.Department.SessionID = HttpContext.Session.Id;
                objModel.Department.actionCategory = "Department";
                int isInserted = _department.InsertDepartment(objModel.Department);
                if (isInserted > 0)
                {
                    var model = new SuperAdminViewModel
                    {
                        Department = new DepartmentModel(),
                    };
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Department_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Department_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult EditDepartment(int id)
        {
            try
            {
                ViewBag.title = "Department";

                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var department = _department.Get_Department_By_Id(id);
                if (department == null)
                {
                    return NotFound();
                }
                var model = new SuperAdminViewModel
                {
                    Department = department,
                    Ministry_ddl_List = _department.GetMinistries()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the ministry for editing.";
                return View("Department");
            }
        }

        [HttpPost]
        public IActionResult EditDepartment(SuperAdminViewModel objModel)
        {
            try
            {
                ViewBag.title = "Department";
                objModel.Department.CreatedBy = HttpContext.Session.GetString("Fullname");
                objModel.Department.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objModel.Department.SessionID = HttpContext.Session.Id;
                objModel.Department.actionCategory = "Department";
                int isInserted = _department.UpdateDepartment(objModel.Department);
                if (isInserted > 0)
                {
                    var model = new SuperAdminViewModel
                    {
                        Department = new DepartmentModel(),
                    };
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Update_Department_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Update_Department_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                string createdBy = HttpContext.Session.GetString("Fullname");
                string createdByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                string sessionID = HttpContext.Session.Id;
                string actionCategory = "Department";
                int result = _department.DeleteDepartment(id, createdBy, createdByIP, sessionID, actionCategory);
                if (result > 0)
                {
                    TempData["Delete_Department_Message"] = "Department deleted successfully";
                }
                else
                {
                    TempData["Failed_Department_Message"] = "Failed to delete Department";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Department.";
            }

            return RedirectToAction("Department");
        }

        [HttpGet]
        public IActionResult DepartmentAuditTrail()
        {
            try
            {
                ViewBag.title = "Department Audit Trail";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    Department_AuditTrail_List = _department.Get_DepartmentAuditTrail()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the form.";
                return View();
            }
        }


        //-----------------------------------------Organization Section
        [HttpGet]
        public IActionResult Organization()
        {
            try
            {
                ViewBag.title = "Organization";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    Organization_List = _organization.Get_OrganizationList(),

                    Ministry_ddl_ListforOrg = _organization.GetMinistries("", "manage"),
                    AppointingAuthority_ddl_List = _organization.GetAppointingAuthority(),
                    OrgLevel_ddl_List = _organization.GetOrgLevel(),
                    States_ddl_List = _organization.GetStates()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the form.";
                return View();
            }
        }

        [HttpGet]
        public JsonResult BindDepartmentdropdown(string Mincode)
        {
            var departments = _organization.Get_DepartmentfordropdownbyMincode_New(Mincode);

            if (departments == null || !departments.Any())
            {
                return Json(new { success = false, message = "No departments found." });
            }
            return Json(departments);
        }

        [HttpGet]
        public JsonResult GetDistrictsByState(string state_id)
        {
            try
            {
                var districts = _organization.GetDistrictsByState(state_id);
                if (districts == null || !districts.Any())
                {
                    return Json(new { success = false, message = "No districts found." });
                }
                return Json(districts);
            }
            catch (Exception)
            {
                return Json(new List<SelectListItem>());
            }
        }

        [HttpPost]
        public IActionResult Organization(SuperAdminViewModel objModel)
        {
            try
            {
                ViewBag.title = "Organization";

                objModel.Organization.DEPTCODE = objModel.ddlDeptCode;
                objModel.Organization.org_district = objModel.ddldistrictCode;

                int isInserted = _organization.InsertOrganization(objModel.Organization);
                if (isInserted > 0)
                {
                    var model = new SuperAdminViewModel
                    {
                        Organization = new OrganizationModel()
                    };
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }

            catch (Exception)
            {
                return Json(new { success = false, message = "An unexpected error occurred." });
            }
        }

        [HttpGet]
        public IActionResult EditOrganization(int id)
        {
            try
            {
                ViewBag.title = "Organization";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var organization = _organization.Get_Organization_By_Id(id);
                if (organization == null)
                {
                    return NotFound();
                }
                var model = new SuperAdminViewModel
                {
                    Organization = organization,

                    Ministry_ddl_ListforOrg = _organization.GetMinistries("", "manage"),
                    AppointingAuthority_ddl_List = _organization.GetAppointingAuthority(),
                    OrgLevel_ddl_List = _organization.GetOrgLevel(),
                    States_ddl_List = _organization.GetStates(),
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the Organization for editing.";
                return View("Organization");
            }
        }

        [HttpPost]
        public IActionResult EditOrganization(SuperAdminViewModel objmodel)
        {
            try
            {
                ViewBag.title = "Organization";
                objmodel.Organization.UPDATE_DATE = DateTime.Now;
                int isInserted = _organization.UpdateOrganization(objmodel.Organization);
                if (isInserted > 0)
                {
                    var model = new SuperAdminViewModel
                    {
                        Organization = new OrganizationModel(),
                    };
                    return Json(new { success = true });

                }
                else
                {
                    return Json(new { success = false, error_message = "Something went wrong..." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, error_message = "Something went wrong while updating the organization." });
            }
        }

        [HttpGet]
        public IActionResult DeleteOrganization(int id)
        {
            try
            {
                int result = _organization.DeleteOrganization(id);
                if (result > 0)
                {
                    TempData["Delete_Organization_Message"] = "Organization deleted successfully";
                }
                else
                {
                    TempData["Failed_Organization_Message"] = "Failed to delete Organization";
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Organization.";
            }

            return RedirectToAction("Organization");
        }

        [HttpGet]
        public IActionResult OrganizationAuditTrail()
        {
            try
            {
                ViewBag.title = "Organization Audit Trail";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    Organization_AuditTrail_List = _organization.Get_OrganizationAuditTrail()
                };
                return View(model);
            }
            catch (Exception)
            {
                return View();
            }
        }



        //-----------------------------------------Level Section
        [HttpGet]
        public IActionResult Level()
        {
            try
            {
                ViewBag.title = "Org Level";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    Org_Level_List = _level.Get_Level()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the page.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Level(SuperAdminViewModel objModel)
        {
            try
            {
                ViewBag.title = "Org Level";

                objModel.Org_Level.CreatedBy = HttpContext.Session.GetString("Fullname");
                objModel.Org_Level.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objModel.Org_Level.SessionID = HttpContext.Session.Id;
                objModel.Org_Level.actionCategory = "Org Level";
                int isInserted = _level.InsertLevel(objModel.Org_Level);
                if (isInserted > 0)
                {
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Level_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Level_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult EditLevel(int id)
        {
            try
            {
                ViewBag.title = "Org Level";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var level = _level.Get_Level_By_Id(id);
                if (level == null)
                {
                    return NotFound();
                }
                var model = new SuperAdminViewModel
                {
                    Org_Level = level
                };

                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the level for editing.";
                return View("Level");
            }
        }

        [HttpPost]
        public IActionResult EditLevel(SuperAdminViewModel objmodel)
        {
            try
            {
                ViewBag.title = "Org Level";
                objmodel.Org_Level.CreatedBy = HttpContext.Session.GetString("Fullname");
                objmodel.Org_Level.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objmodel.Org_Level.SessionID = HttpContext.Session.Id;
                objmodel.Org_Level.actionCategory = "Org Level";
                int isInserted = _level.UpdateLevel(objmodel.Org_Level);
                if (isInserted > 0)
                {
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Update_Level_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Update_Level_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult DeleteLevel(int id)
        {
            try
            {
                string createdBy = HttpContext.Session.GetString("Fullname");
                string createdByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                string sessionID = HttpContext.Session.Id;
                string actionCategory = "Org Level";
                int result = _level.DeleteLevel(id, createdBy, createdByIP, sessionID, actionCategory);
                if (result > 0)
                {
                    TempData["Delete_Level_Message"] = "Level deleted successfully.";
                }
                else
                {
                    TempData["Failed_Level_Message"] = "Failed to delete Level.";
                }
            }
            catch (Exception)
            {
                TempData["Error_Level_Message"] = "An error occurred while deleting the Level.";
            }

            return RedirectToAction("Level"); // Go back to the list page
        }

        [HttpGet]
        public IActionResult LevelAuditTrail()
        {
            try
            {
                ViewBag.title = "Org Level Audit Trail";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    Org_Level_AuditTrail_List = _level.Get_LevelAuditTrail()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the page.";
                return View();
            }
        }



        //-----------------------------------------Appointing Authority Section
        [HttpGet]
        public IActionResult AppointingAuthority()
        {
            try
            {
                ViewBag.title = "Appointing Authority";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    AppointingAuthority_List = _appointingauthority.GetAppointingAuthorityList()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the page.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult AppointingAuthority(SuperAdminViewModel objModel)
        {
            try
            {
                ViewBag.title = "Appointing Authority";

                objModel.AppointingAuthority.CreatedBy = HttpContext.Session.GetString("Fullname");
                objModel.AppointingAuthority.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objModel.AppointingAuthority.SessionID = HttpContext.Session.Id;
                objModel.AppointingAuthority.actionCategory = "Appointing Authority";
                int isInserted = _appointingauthority.InsertAppointingAuthority(objModel.AppointingAuthority);
                if (isInserted > 0)
                {
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_AppointingAuthority_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_AppointingAuthority_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult EditAppointingAuthority(int id)
        {
            try
            {
                ViewBag.title = "Appointing Authority";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var app_auth = _appointingauthority.Get_AppointingAuthority_By_Id(id);
                if (app_auth == null)
                {
                    return NotFound();
                }

                var model = new SuperAdminViewModel
                {
                    AppointingAuthority = app_auth
                };

                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the Appointing Authority for editing.";
                return View("Level");
            }
        }

        [HttpPost]
        public IActionResult EditAppointingAuthority(SuperAdminViewModel objmodel)
        {
            try
            {
                ViewBag.title = "Appointing Authority";
                objmodel.AppointingAuthority.CreatedBy = HttpContext.Session.GetString("Fullname");
                objmodel.AppointingAuthority.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objmodel.AppointingAuthority.SessionID = HttpContext.Session.Id;
                objmodel.AppointingAuthority.actionCategory = "Appointing Authority";
                int isInserted = _appointingauthority.UpdateAppointingAuthority(objmodel.AppointingAuthority);
                if (isInserted > 0)
                {
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Update_Appointing_Authority_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Update_Appointing_Authority_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult DeleteAppointingAuthority(int id)
        {
            try
            {
                string createdBy = HttpContext.Session.GetString("Fullname");
                string createdByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                string sessionID = HttpContext.Session.Id;
                string actionCategory = "Appointing Authority";
                int result = _appointingauthority.DeleteAppointingAuthority(id, createdBy, createdByIP, sessionID, actionCategory);
                if (result > 0)
                {
                    TempData["Delete_Appointing_Authority_Message"] = "Appointing Authority deleted successfully.";
                }
                else
                {
                    TempData["Failed_Appointing_Authority_Message"] = "Failed to delete Appointing Authority.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Appointing Authority.";
            }

            return RedirectToAction("AppointingAuthority");
        }

        [HttpGet]
        public IActionResult AppointingAuthorityAuditTrail()
        {
            try
            {
                ViewBag.title = "Appointing Authority Audit Trail";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    AppointingAuthority_AuditTrail_List = _appointingauthority.Get_AppointingAuthorityAuditTrail()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the page.";
                return View();
            }
        }



        //-----------------------------------------Master Service Section
        [HttpGet]
        public IActionResult MasterCvoServices()
        {
            try
            {
                ViewBag.title = "Master Cvo Services";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    MasterCVOServices_List = _masterCvoServices.Get_MasterCvoServicesModel()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the form.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult MasterCvoServices(SuperAdminViewModel objModel)
        {
            try
            {
                ViewBag.title = "Master Cvo Services";
                objModel.MasterCVOServices.CreatedBy = HttpContext.Session.GetString("Fullname");
                objModel.MasterCVOServices.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objModel.MasterCVOServices.SessionID = HttpContext.Session.Id;
                objModel.MasterCVOServices.actionCategory = "Master Cvo Services";
                int isInserted = _masterCvoServices.InsertMasterCvoServices(objModel.MasterCVOServices);
                if (isInserted > 0)
                {
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Master_Services_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Master_Services_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult EditMasterCvoServices(int id)
        {
            try
            {
                ViewBag.title = "Master Cvo Services";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var masterservice = _masterCvoServices.Get_MasterCvoServices_By_Id(id);
                if (masterservice == null)
                {
                    return NotFound();
                }

                var model = new SuperAdminViewModel
                {
                    MasterCVOServices = masterservice
                };

                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the MasterCvoServices for editing.";
                return View("Level");
            }
        }

        [HttpPost]
        public IActionResult EditMasterCvoServices(SuperAdminViewModel objModel)
        {
            try
            {
                ViewBag.title = "Master Cvo Services";
                objModel.MasterCVOServices.CreatedBy = HttpContext.Session.GetString("Fullname");
                objModel.MasterCVOServices.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objModel.MasterCVOServices.SessionID = HttpContext.Session.Id;
                objModel.MasterCVOServices.actionCategory = "Master Cvo Services";
                int isInserted = _masterCvoServices.UpdateMasterCvoServices(objModel.MasterCVOServices);
                if (isInserted > 0)
                {
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Update_MasterCvoServices_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Update_MasterCvoServices_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult DeleteMasterCvoServices(int id)
        {
            try
            {
                string createdBy = HttpContext.Session.GetString("Fullname");
                string createdByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                string sessionID = HttpContext.Session.Id;
                string actionCategory = "Master Cvo Services";
                int result = _masterCvoServices.DeleteMasterCvoServices(id, createdBy, createdByIP, sessionID, actionCategory);
                if (result > 0)
                {
                    TempData["Delete_MasterCvoServices_Message"] = "Master Cvo Services deleted successfully.";
                }
                else
                {
                    TempData["Failed_MasterCvoServices_Message"] = "Failed to delete Master Cvo Services.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the Master Cvo Services.";
            }

            return RedirectToAction("MasterCvoServices");
        }

        [HttpGet]
        public IActionResult MasterCvoServicesAuditTrail()
        {
            try
            {
                ViewBag.title = "Master Cvo Services AuditTrail";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }
                var model = new SuperAdminViewModel
                {
                    MasterCVOServices_AuditTrail_List = _masterCvoServices.Get_MasterCvoServicesAuditTrail()
                };
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the form.";
                return View();
            }
        }



        //-----------------------------------------State Section
        [HttpGet]
        public async Task<IActionResult> State()
        {
            try
            {
                ViewBag.title = "State";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }

                var model = new SuperAdminViewModel
                {
                    State_List = await _state.Get_StateListAsync()
                };

                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the form.";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> State(SuperAdminViewModel objModel)
        {
            try
            {
                ViewBag.title = "State";

                objModel.State.CreatedBy = HttpContext.Session.GetString("Fullname");
                objModel.State.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objModel.State.SessionID = HttpContext.Session.Id;
                objModel.State.actionCategory = "State";
                int isInserted = await _state.InsertStateAsync(objModel.State);

                if (isInserted > 0)
                {
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_state_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_state_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditState(string id)
        {
            try
            {
                ViewBag.title = "State";

                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }

                var state = await _state.Get_State_By_IdAsync(id);

                if (state == null)
                {
                    return NotFound();
                }

                var model = new SuperAdminViewModel
                {
                    State = state
                };

                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the State for editing.";
                return View("Level"); // Consider changing "Level" to "State" if it's a typo
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditState(SuperAdminViewModel objmodel)
        {
            try
            {
                ViewBag.title = "State";

                objmodel.State.CreatedBy = HttpContext.Session.GetString("Fullname");
                objmodel.State.CreatedByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                objmodel.State.SessionID = HttpContext.Session.Id;
                objmodel.State.actionCategory = "State";
                int isInserted = await _state.UpdateStateAsync(objmodel.State);

                if (isInserted > 0)
                {
                    return Json(new { success = true });
                }
                else if (isInserted == -1)
                {
                    return Json(new { success = false, duplicate_Update_State_message = "This record already exists." });
                }
                else
                {
                    return Json(new { success = false, error_Update_State_message = "This record is not saved." });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteState(string id)
        {
            try
            {
                string createdBy = HttpContext.Session.GetString("Fullname");
                string createdByIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                string sessionID = HttpContext.Session.Id;
                string actionCategory = "State";
                int result = await _state.DeleteStateAsync(id, createdBy, createdByIP, sessionID, actionCategory);

                if (result > 0)
                {
                    TempData["StatesuccessMessage"] = "State deleted successfully.";
                }
                else
                {
                    TempData["StateerrorMessage"] = "Failed to delete State.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the State.";
            }

            return RedirectToAction("State");
        }

        [HttpGet]
        public async Task<IActionResult> StateAuditTrail()
        {
            try
            {
                ViewBag.title = "State Audit Trail";
                string username = HttpContext.Session.GetString("Username");
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("Login", "Home");
                }

                var model = new SuperAdminViewModel
                {
                    State_AuditTrail_List = await _state.Get_StateAuditTrailAsync()
                };

                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = "Something went wrong while loading the form.";
                return View();
            }
        }
    }
}
