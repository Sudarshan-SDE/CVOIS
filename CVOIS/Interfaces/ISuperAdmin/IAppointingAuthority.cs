using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IAppointingAuthority
    {
        
        List<AppointingAuthorityModel> GetAppointingAuthorityList();

        int InsertAppointingAuthority(AppointingAuthorityModel model);

        int UpdateAppointingAuthority(AppointingAuthorityModel model);

        int DeleteAppointingAuthority(int id,string createdBy, string createdByIP, string sessionID, string actionCategory);

        AppointingAuthorityModel Get_AppointingAuthority_By_Id(int id);

        List<AppointingAuthorityAuditTrailModel> Get_AppointingAuthorityAuditTrail();

    }
}
