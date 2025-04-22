using CVOIS.Models.SuperAdmin;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IAppointingAuthority
    {
        
        List<AppointingAuthorityModel> GetAppointingAuthorityList();

        int InsertAppointingAuthority(AppointingAuthorityModel model);

        int UpdateAppointingAuthority(AppointingAuthorityModel model);

        int DeleteAppointingAuthority(int id,string createdBy, string createdByIP, string sessionID);

        AppointingAuthorityModel Get_AppointingAuthority_By_Id(int id);

    }
}
