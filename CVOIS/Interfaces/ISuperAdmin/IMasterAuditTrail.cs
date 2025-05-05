using CVOIS.Models.SuperAdmin.AuditTrail;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IMasterAuditTrail
    {
        List<MasterAuditTrailModel> Get_MasterAuditTrail();
    }
}
