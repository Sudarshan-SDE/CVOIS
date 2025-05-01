using CVOIS.Models.Admin;
using CVOIS.Models.SuperAdmin.AuditTrail;
using CVOIS.Models.Viewers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Models.SuperAdmin
{
    public class SuperAdminViewModel
    {
        //Ministry
        public MinistryModel Ministry { get; set; }
        public List<MinistryModel> Ministry_List { get; set; }
        public List<SelectListItem> MinistryType_ddl_List { get; set; }


        public MinistryAuditTrailModel MinistryAuditTrail { get; set; }
        public List<MinistryAuditTrailModel> Ministry_AuditTrail_List { get; set; }



        //Department
        public DepartmentModel Department { get; set; }
        public List<DepartmentModel> Department_List { get; set; }
        public List<SelectListItem> Ministry_ddl_List { get; set; }


        public DepartmentAuditTrailModel DepartmentAuditTrail { get; set; }
        public List<DepartmentAuditTrailModel> Department_AuditTrail_List { get; set; }
    



        //Organization
        public OrganizationModel Organization { get; set; }
        public List<OrganizationModel> Organization_List { get; set; }
        public List<OrganizationAuditTrailModel> Organization_AuditTrail_List { get; set; }
        public List<SelectListItem> AppointingAuthority_ddl_List { get; set; }
        public List<SelectListItem> OrgLevel_ddl_List { get; set; }
       




        public string MinistryforOrg { get; set; }
        public List<SelectListItem> Ministry_ddl_ListforOrg { get; set; }

        public string ddlDeptCode { get; set; }
        public List<SelectListItem> Department_ddl_List_Basisofmincode { get; set; }


        public string StateforOrg { get; set; }
        public List<SelectListItem> States_ddl_List { get; set; }
        public string ddldistrictCode { get; set; }
        public List<SelectListItem> District_ddl_List_Basisofstateid { get; set; }





        //Level
        public OrgLevelModel Org_Level { get; set; }
        public List<OrgLevelModel> Org_Level_List { get; set; }

        public OrgLevelAuditTrailModel Org_LevelAuditTrail { get; set; }
        public List<OrgLevelAuditTrailModel> Org_Level_AuditTrail_List { get; set; }



        //Appointing AUthority
        public AppointingAuthorityModel AppointingAuthority { get; set; }
        public List<AppointingAuthorityModel> AppointingAuthority_List { get; set; }

        public AppointingAuthorityAuditTrailModel AppointingAuthorityAuditTrail { get; set; }
        public List<AppointingAuthorityAuditTrailModel> AppointingAuthority_AuditTrail_List { get; set; }




        //Master CVO Service
        public MasterCvoServicesModel MasterCVOServices { get; set; }
        public List<MasterCvoServicesModel> MasterCVOServices_List { get; set; }


        public MasterCVOServicesAuditTrailModel MasterCVOServicesAuditTrail { get; set; }
        public List<MasterCVOServicesAuditTrailModel> MasterCVOServices_AuditTrail_List { get; set; }




        //State
        public StateModel State { get; set; }
        public List<StateModel> State_List { get; set; }


        public StateAuditTrailModel StateAuditTrail { get; set; }
        public List<StateAuditTrailModel> State_AuditTrail_List { get; set; }

    }


}
