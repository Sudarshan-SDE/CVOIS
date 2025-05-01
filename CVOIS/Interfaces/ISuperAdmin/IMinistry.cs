using CVOIS.Models.Admin;
using CVOIS.Models.SuperAdmin;
using CVOIS.Models.SuperAdmin.AuditTrail;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IMinistry
    {
        Task<List<MinistryModel>> Get_MinistriesAsync(string minCode, string manage);
        Task<List<SelectListItem>> GetMinistryTypeAsync();
        Task<int> InsertMinistryAsync(MinistryModel model);
        Task<MinistryModel> Get_Ministry_By_IdAsync(int id);
        Task<int> UpdateMinistryAsync(MinistryModel model);
        Task<int> DeleteMinistryAsync(int id, string createdBy, string createdByIP, string sessionID, string actionCategory);
        Task<List<MinistryAuditTrailModel>> Get_MinistryAuditTrailAsync();
    }
}
