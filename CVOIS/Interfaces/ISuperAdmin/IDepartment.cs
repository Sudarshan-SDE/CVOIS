using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;
using CVOIS.Models.SuperAdmin.DeleteAuditTrail;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IDepartment
    {
        int InsertDepartment(DepartmentModel model);
        int UpdateDepartment(DepartmentModel model);
        int DeleteDepartment(int id, string createdBy, string createdByIP, string sessionID, string actionCategory);
        List<DepartmentModel> Get_Department();
        DepartmentModel Get_Department_By_Id(int id);
        List<SelectListItem> GetMinistries(string minCode = "", string manage = "");
        List<DepartmentAuditTrailModel> Get_DepartmentAuditTrail();
        List<DepartmentDeleteAuditTrailModel> Get_DepartmentDeleteAuditTrail();
    }
}
