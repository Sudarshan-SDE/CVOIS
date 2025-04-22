using CVOIS.Models.SuperAdmin;
using CVOIS.Models.Viewers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Interfaces.ISuperAdmin
{
    public interface ILevel
    {
        int InsertLevel(OrgLevelModel model);
        int UpdateLevel(OrgLevelModel model);
        int DeleteLevel(int id, string createdBy, string createdByIP, string sessionID);
        List<OrgLevelModel> Get_Level();
        OrgLevelModel Get_Level_By_Id(int id);
    }
}
