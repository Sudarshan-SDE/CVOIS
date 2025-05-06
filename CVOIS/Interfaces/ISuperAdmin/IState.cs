using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;
using CVOIS.Models.SuperAdmin.DeleteAuditTrail;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IState
    {
        Task<List<StateModel>> Get_StateListAsync();
        Task<int> InsertStateAsync(StateModel model);
        Task<int> UpdateStateAsync(StateModel model);
        Task<int> DeleteStateAsync(string id, string createdBy, string createdByIP, string sessionID, string actionCategory);
        Task<StateModel> Get_State_By_IdAsync(string id);
        Task<List<StateAuditTrailModel>> Get_StateAuditTrailAsync();
        Task<List<StateDeleteAuditTrailModel>> Get_StateDeleteAuditTrailAsync();
    }
}
