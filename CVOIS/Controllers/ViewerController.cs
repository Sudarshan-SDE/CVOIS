using CVOIS.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;
using CVOIS.Interfaces.IViewer;
using CVOIS.Models;
using CVOIS.Models.Viewers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Controllers
{
    public class ViewerController : Controller
    {
        private readonly cvois_context _context;
        private readonly IReport_Viewer _viewer;
        public ViewerController(cvois_context context, IReport_Viewer viewer)
        {
            this._context = context;
            this._viewer = viewer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var model = new ViewerModel
                {
                    AppointingAuthorityList = _viewer.GetAppointingAuthorities(),
                    StateList = _viewer.GetStates(),
                    LevelList = _viewer.GetLevels(),
                    ServiceList = _viewer.GetServices(),
                    CVOList = _viewer.GetCVO(),
                    OrganisationCategoryList = _viewer.GetOrganisationCategory(),
                    MinistryList = _viewer.GetMinistries("", "manage"),
                    DepartmentList = _viewer.GetDepartments(),
                    OrganisationList = _viewer.GetOrganisations(),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Index: " + ex.Message);
                ViewBag.ErrorMessage = "Something went wrong while loading the page.";
                return View("Index");

            }
        }

        public IActionResult ViewerDashboard()
        {
            try
            {
                //var _DashboardData = _Report_Admin.GetAdminDashboardData("countList", "");
                //return View(_DashboardData);
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        [HttpPost]
        public IActionResult Index([FromBody] ViewerModel _objmodeldata)

        {
            var _cvodata = _viewer.Search_GetCvoDetails(_objmodeldata);
            return Json(new { success = true, message = "Data received successfully", data = _cvodata });
        }

    }
}
