using CVOIS.Models.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace CVOIS.Interfaces.Admin
{
    public interface IReport_Admin
    {
        List<MinistriesModel> Get_Ministries(string minCode, string manage);
        List<DepartmentsModel> Get_Departments();
        List<OrganizationsModel> Get_Organization();
        List<FullTimeCVOModel> Get_FullTimeCVO();
        List<VacantFullTimeCVOModel> Get_VacantFullTimeCVO(string reportType);


        List<AdminDashboardModel> GetAdminDashboardData(string reportType, string VER_APP_FLAG);
        List<AdminViewRequestDetailsModel> AdminViewRequestDetails(string reportType, string VER_APP_FLAG);

        #region Added as on date 18_03_2025 for Add Full time CVO
        List<FulltimeCVOListForManageModel> Get_FulltimeCVOlistForManage();

       // List<OrgdropdownModel> Get_OrgforDropdown();

        List<SelectListItem> Get_OrgforDropdown();
        List<SelectListItem> Get_ServicesforDropdown();
        List<CheckOrgWithcvoModel> Check_OrgWithCvo(string OrgCode);

        #endregion

        #region Added as on date 02_04_2025

        int InsertFullTimeCVO(AddFullTimeCVOModel model);

        #endregion
    }
}
