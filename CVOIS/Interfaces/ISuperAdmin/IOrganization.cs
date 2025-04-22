using CVOIS.Models.SuperAdmin;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IOrganization
    {
        List<OrganizationModel> Get_OrganizationList();
        int InsertOrganization(OrganizationModel model);
        OrganizationModel Get_Organization_By_Id(int id);
        int UpdateOrganization(OrganizationModel model);
        int DeleteOrganization(int id);


        //other dropdown lists 
        List<SelectListItem> GetAppointingAuthority();
        List<SelectListItem> GetOrgLevel();

        List<SelectListItem> GetStates();
        List<SelectListItem> GetDistrictsByState(string state_id);

        List<SelectListItem> GetMinistries(string minCode = "", string manage = "");
        List<SelectListItem> Get_DepartmentfordropdownbyMincode_New(string Mincode);
    }
}
