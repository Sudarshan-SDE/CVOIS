using CVOIS.Models.SuperAdmin;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface IState
    {
        Task<List<StateModel>> Get_StateListAsync();
        Task<int> InsertStateAsync(StateModel model);
        Task<int> UpdateStateAsync(StateModel model);
        Task<int> DeleteStateAsync(string id, string createdBy, string createdByIP, string sessionID);
        Task<StateModel> Get_State_By_IdAsync(string id);

    }
}
