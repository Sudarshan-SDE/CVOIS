using CVOIS.Models.Admin;
using CVOIS.Models.SuperAdmin;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IMinistry
    {
        //List<MinistryModel> Get_Ministries(string minCode, string manage);
        Task<List<MinistryModel>> Get_MinistriesAsync(string minCode, string manage);


        //List<SelectListItem> GetMinistryType();
        Task<List<SelectListItem>> GetMinistryTypeAsync();


        //int InsertMinistry(MinistryModel model);
        Task<int> InsertMinistryAsync(MinistryModel model);

        //MinistryModel Get_Ministry_By_Id(int id);
        Task<MinistryModel> Get_Ministry_By_IdAsync(int id);


        //int UpdateMinistry(MinistryModel model);
        Task<int> UpdateMinistryAsync(MinistryModel model);


        //int DeleteMinistry(int id);
        Task<int> DeleteMinistryAsync(int id, string createdBy, string createdByIP, string sessionID);






    }
}
