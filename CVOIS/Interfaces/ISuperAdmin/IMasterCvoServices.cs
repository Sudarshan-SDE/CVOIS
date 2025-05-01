using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IMasterCvoServices
    {
        List<MasterCvoServicesModel> Get_MasterCvoServicesModel();

        int InsertMasterCvoServices(MasterCvoServicesModel model);

        int UpdateMasterCvoServices(MasterCvoServicesModel model);

        int DeleteMasterCvoServices(int id, string createdBy, string createdByIP, string sessionID, string actionCategory);

        MasterCvoServicesModel Get_MasterCvoServices_By_Id(int id);

        List<MasterCVOServicesAuditTrailModel> Get_MasterCvoServicesAuditTrail();
    }
}
