using CVOIS.Models.Admin;
using CVOIS.Models.Viewers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Interfaces.IViewer
{
    public interface IReport_Viewer
    {
        List<SelectListItem> GetAppointingAuthorities();
        List<SelectListItem> GetStates();
        List<SelectListItem> GetLevels();
        List<SelectListItem> GetServices();
        List<SelectListItem> GetCVO();
        List<SelectListItem> GetOrganisationCategory();
        List<SelectListItem> GetMinistries(string minCode = "", string manage = "");
        List<SelectListItem> GetDepartments();
        List<SelectListItem> GetOrganisations();
        List<CVO_Details_Model> Search_GetCvoDetails(ViewerModel _objmodeldata);

    }

}
