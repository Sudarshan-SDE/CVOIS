using CVOIS.Interfaces.Admin;
using CVOIS.Models;
using CVOIS.Models.Admin;
using CVOIS.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Data;
using Newtonsoft.Json;
using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.Models.SuperAdmin;

namespace CVOIS.Controllers
{

    public class AdminController : Controller
    {
        private readonly IReport_Admin _Report_Admin;
        public AdminController(IReport_Admin report_admin)
        {
            this._Report_Admin = report_admin;
        }

        public IActionResult AdminDashboard()
        {
            try
            {
                ViewBag.title = "Admin Dashboard";
                var _DashboardData = _Report_Admin.GetAdminDashboardData("countList", "");
                return View(_DashboardData);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public IActionResult View_DashboardDetailedReport(string flag)
        {
            try
            {
                if (flag == "Y")
                {
                    TempData["Title"] = "Total Request Approved";
                }
                else if (flag == "N")
                {
                    TempData["Title"] = "Total Request Pending";
                }
                else if (flag == "R")
                {
                    TempData["Title"] = "Total Request Rejected";
                }
                else
                {
                    TempData["Title"] = "Total Requests Received";
                }

                var model = new DashboardReportViewModel
                {
                    Requests = _Report_Admin.AdminViewRequestDetails("DetailedList", flag), // Example data source
                    StatusFilter = new StaticDropdownModel
                    {
                        SelectedStatus = "",
                        StatusList = new List<SelectListItem>
                {
                   new SelectListItem { Value = "N", Text = "Pending" },
                   new SelectListItem { Value = "Y", Text = "Approved" },
                   new SelectListItem { Value = "R", Text = "Rejected" }
                }
                    }

                };
                model.StatusFilter.SelectedStatus = flag;

                return View(model);
            }
            catch (Exception ex)
            {

            }
            return View();
        }



        //23-04-2025
        public IActionResult Ministries(string minCode = "")
        {
            try
            {
                ViewBag.title = "Ministries";
                var manageFlag = string.IsNullOrEmpty(minCode) ? "manage" : "";
                MinistriesModel model = new MinistriesModel
                {
                    Ministry_List = _Report_Admin.Get_Ministries(minCode, manageFlag)
                };
                return View(model);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while loading the ministry data.";
                return View();
            }
        }
        public IActionResult Departments()
        {
            try
            {
                ViewBag.title = "Department";
                DepartmentsModel model = new DepartmentsModel
                {
                    Department_List = _Report_Admin.Get_Departments()
                };
                return View(model);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while loading the department data.";
                return View();
            }
        }
        public IActionResult Organizations()
        {
            try
            {
                ViewBag.title = "Organizations";
                OrganizationsModel model = new OrganizationsModel
                {
                    Organizations_List = _Report_Admin.Get_Organization()
                };
                return View(model);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while loading the organization data.";
                return View();
            }
        }
        public IActionResult FullTimeCVO()
        {
            try
            {
                ViewBag.title = "Full Time CVO";
                FullTimeCVOModel model = new FullTimeCVOModel
                {
                    FullTimeCVO_List = _Report_Admin.Get_FullTimeCVO()
                };
                return View(model);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while loading the department data.";
                return View();
            }
        }
        public IActionResult VacantFullTimeCVO()
        {
            try
            {
                ViewBag.title = "Vacant Full Time CVO";
                VacantFullTimeCVOModel model = new VacantFullTimeCVOModel
                {
                    VacantFullTimeCVO_List = _Report_Admin.Get_VacantFullTimeCVO("Detailed")
                };
                return View(model);
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while loading the vacant CVO data.";
                return View();
            }
        }



        public IActionResult PendingFullTimeOrganization()
        {
            return View();
        }
        //23-04-2025


        public IActionResult PendingFullTimeOrganization1()
        {
            return View();
        }


        #region Added as on date 17.03.2025
        public IActionResult Get_FullTimeCVOForManage()
        {
            try
            {
                var _CVOList = _Report_Admin.Get_FulltimeCVOlistForManage();
                TempData["Title"] = "Full Time CVO List";
                return View(_CVOList);
            }
            catch (Exception ex)
            {

            }
            return View();

        }

        public IActionResult Add_FullTime_CVO()
        {
            try
            {
                List<CheckOrgWithcvoModel> cvoData = new List<CheckOrgWithcvoModel>();

                if (TempData["CvoData"] != null)
                {
                    var jsonData = TempData["CvoData"].ToString();

                    if (!string.IsNullOrEmpty(jsonData) && jsonData != "[]")
                    {
                        cvoData = JsonConvert.DeserializeObject<List<CheckOrgWithcvoModel>>(jsonData);
                    }
                    //cvoData = JsonConvert.DeserializeObject<List<CheckOrgWithcvoModel>>(TempData["CvoData"].ToString());
                }

                //var checkorgwithcvo = TempData.Peek("CvoData");

                var currentYear = DateTime.Now.Year;
                var yearsList = Enumerable.Range(currentYear - 40, 41)
                              .Select(y => new SelectListItem
                              {
                                  Value = y.ToString(),
                                  Text = y.ToString()
                              }).ToList();

                var model = new AddFullTimeCVOModel
                {
                    /*_checkorgwithcvo = (List<CheckOrgWithcvoModel>)TempData.Peek("CvoData")*/
                    _checkorgwithcvo = cvoData,
                    _Orgfilter = _Report_Admin.Get_OrgforDropdown(),
                    _servicesfilter = _Report_Admin.Get_ServicesforDropdown(),
                    _batchfilter = yearsList,
                    _charges_Dropdown = new Charges_StaticDropdownModel
                    {
                        SelectedCharges = "",
                        ChargesList = new List<SelectListItem>
                {
                   new SelectListItem { Value = "Additional", Text = "Additional" },
                   new SelectListItem { Value = "Regular", Text = "Regular" },
                }
                    },

                    _title_Dropdown = new Title_StaticDropdownModel
                    {
                        SelectedTitle = "",
                        TilteList = new List<SelectListItem>
                {
                   new SelectListItem { Value = "Mr", Text = "Mr" },
                   new SelectListItem { Value = "Mrs", Text = "Mrs" },
                   new SelectListItem { Value = "Ms", Text = "Ms" },
                   new SelectListItem { Value = "Dr", Text = "Dr" },
                }
                    },

                    _gender = new Gender_StaticDropdownModel
                    {
                        SelectedGender = "",
                        GenderList = new List<SelectListItem>
                {
                   new SelectListItem { Value = "Male", Text = "Male" },
                   new SelectListItem { Value = "Female", Text = "Female" },

                }
                    },

                    _status = new Status_StaticDropdownModel
                    {
                        SelectedStatus = "",
                        StatusList = new List<SelectListItem>
                {
                   new SelectListItem { Value = "ACTIVE", Text = "ACTIVE" },
                   new SelectListItem { Value = "INACTIVE", Text = "INACTIVE" },

                }
                    },

                    SelectedOrg = (string)TempData["OrgCode"],


                };

                return View(model);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        public IActionResult Add_FullTime_CVO(AddFullTimeCVOModel model)
        {
            try
            {
                int isInserted = _Report_Admin.InsertFullTimeCVO(model);

                if (isInserted > 0)
                {
                    TempData["Successmsg"] = "Full Time CVO Added Successfully";
                }
                else
                {
                    TempData["Successmsg"] = "Error: CVO Not Added...!!";
                }


                //Insert_NewFullTimeCVO_Model addmodel = new Insert_NewFullTimeCVO_Model()
                //{
                //    addmodel.TITLE = model._title_Dropdown.SelectedTitle??"null",
                //    addmodel.ORGCODE = model.Organisation,
                //    addmodel.CHARGES = model._charges_Dropdown.SelectedCharges;
                //    addmodel.TITLE = model._title_Dropdown.SelectedTitle;
                //    addmodel.CVO_NAME = model.EnterCVOName;
                //    addmodel.GENDER = model._gender.SelectedGender;
                //    addmodel.PHONE_NO = model.MobileNo;
                //    addmodel.SERVICES = model.SelectedService; // selected 
                //    addmodel.BATCH = model.SelectedBatch;
                //    addmodel.TENURE_FROM = model.TenureFrom;
                //    addmodel.TENURE_TO = model.TenureTo;
                //    addmodel.EMAIL_ID = model.Email;
                //    addmodel.CVO_STATUS = model._status.SelectedStatus;
                //};


            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Get_FullTimeCVOForManage");

        }

        public IActionResult Update_CVODetails(string id, string flag)
        {
            try
            {
                //var _CVOList = _Report_Admin.Get_FulltimeCVOlistForManage();
                //return View(_CVOList);
            }
            catch (Exception ex)
            {

            }
            return View();

        }


        public IActionResult SearchOrganization_WithCvo(string OrgCode)
        {
            try
            {
                var data = _Report_Admin.Check_OrgWithCvo(OrgCode);
                TempData["CvoData"] = JsonConvert.SerializeObject(data);
                TempData.Keep("CvoData");

                TempData["OrgCode"] = OrgCode;
                TempData.Keep("OrgCode");


                return RedirectToAction("Add_FullTime_CVO");
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Add_FullTime_CVO");

        }


        #endregion Added as on date 17.03.2025
    }
}
