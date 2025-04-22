using CVOIS.Models.SuperAdmin;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IMasterCvoServices
    {
        List<MasterCvoServicesModel> Get_MasterCvoServicesModel();

        int InsertMasterCvoServices(MasterCvoServicesModel model);

        int UpdateMasterCvoServices(MasterCvoServicesModel model);

        int DeleteMasterCvoServices(int id, string createdBy, string createdByIP, string sessionID);

        MasterCvoServicesModel Get_MasterCvoServices_By_Id(int id);
    }
}
