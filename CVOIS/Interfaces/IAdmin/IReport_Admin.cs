using CVOIS.Models.Admin;
using CVOIS.Models.SuperAdmin;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace CVOIS.Interfaces.Admin
{
    public interface IReport_Admin
    {
        List<Ministries> Get_Ministries();
        List<FullTimeCVO> Get_Full_Time_CVO();
        List<Organizations> Get_Organization();
        List<Departments> Get_Departments();
        List<Vacant> Get_Vacant();        
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
